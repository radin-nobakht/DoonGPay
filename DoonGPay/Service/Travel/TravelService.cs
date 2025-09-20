using AutoMapper;
using DoonGPay.Adapter;
using DoonGPay.Dto;
using DoonGPay.Dto.Travel;
using DoonGPay.Entity;
using DoonGPay.Entity.Travel;
using DoonGPay.Inteface;
using DoonGPay.Inteface.Travel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DoonGPay.Service.Travel
{
    public class TravelService(MyContext db, IMapper mapper, IMySession mySession) : ITravelService
    {

        #region UsuallyFriends
        public List<UsuallyFriendDto> UsuallyFriends()
        {
            var model = db.UsuallyFrineds.Where(x => x.UserId == mySession.UserId).ToList();
            return mapper.Map<List<UsuallyFriendDto>>(model);
        }
        public UsuallyFriendDto UsuallyFriend(int usuallyFriendId)
        {
            var model = db.UsuallyFrineds.FirstOrDefault(x => x.Id == usuallyFriendId);
            return mapper.Map<UsuallyFriendDto>(model);
        }
        public void SaveUsualyFriend(UsuallyFriendDto usuallyFriend)
        {
            var model = mapper.Map<UsuallyFrinedEntity>(usuallyFriend);
            model.UserId = (int)mySession.UserId;

            if (model.Id > 0)
                db.UsuallyFrineds.Update(model);
            else
                db.UsuallyFrineds.Add(model);
            db.SaveChanges();

        }

        public void DeleteUsuallyFriend(int id)
        {
            var model = db.UsuallyFrineds.FirstOrDefault(x => x.Id == id);
            db.UsuallyFrineds.Remove(model);
            db.SaveChanges();

        }

        #endregion

        #region Travel


        public List<TravelDto> AllTravel(int travelId)
        {
            var data = db.Travels.Where(x => x.Id == travelId)
                .Include(x => x.TravelCosts.Where(x => x.TravelId == travelId))
                .Include(x => x.TravelFriends.Where(x => x.TravelId == travelId))
                .ToList();

            return mapper.Map<List<TravelDto>>(data);
        }


        public void SaveTravel(TravelDto travel)
        {

            var model = mapper.Map<TravelEntity>(travel);
            model.UserId = (int)mySession.UserId;
            if (model.Id > 0)
                db.Travels.Update(model);
            else
                db.Travels.Add(model);
            db.SaveChanges();
        }
        public List<TravelCostFriendDto> TravelCostFriend(int travelId,int friendId)
        {
            var travel = db.Travels
                         .Include(x => x.TravelFriends)
                         .Include(x => x.TravelCosts).FirstOrDefault(x => x.Id == travelId);
            foreach (var i in travel.TravelFriends)
                i.TravelCostFriends = db.TravelCostFriends.Where(x => x.TravelFriendId == i.Id).ToList();

            var travelDto = mapper.Map<TravelDto>(travel);
            foreach (var i in travelDto.TravelFriends)
                foreach (var e in i.TravelCostFriends)
                    foreach (var t in travelDto.TravelCosts)
                        if (t.Id == e.TravelCostId)
                            e.CostName = t.Title;

            foreach(var i in travelDto.TravelFriends)
            {
                if(friendId == i.Id)
                {
                    var travelCostFriend = i.TravelCostFriends.ToList();
                    return travelCostFriend;
                }
            }
            return new List<TravelCostFriendDto>();
        }
        public TravelDto Travel(int? id)
        {
            TravelEntity? travel;
            var query = db.Travels
                           .Include(x => x.TravelFriends)
                           .Include(x => x.TravelCosts);
            
            if (id > 0)
                travel = query.FirstOrDefault(x => x.Id == id);

            else
                travel = query.FirstOrDefault(x => x.UserId == mySession.UserId);


                
            
            if (travel == null)
                return new TravelDto();

            var costTypes = TravelCostTypes();
            var travelDto = mapper.Map<TravelDto>(travel);
            foreach (var tc in travelDto.TravelCosts)
                tc.TypeStr = costTypes.FirstOrDefault(x => x.Value == tc.Type.ToString())?.Text ?? "نامشخص";


            foreach (var tf in travelDto.TravelFriends)
                tf.Share = db.TravelCostFriends.Where(x => x.TravelFriendId == tf.Id).Sum(x => x.Value);

            return travelDto;
        }
        public List<TravelDto> Travels()
        {
            var data = db.Travels.Where(x => x.UserId == mySession.UserId).ToList();
            //var first = db.Travels.First();
            //var dtoFirst = mapper.Map<TravelDto>(first);
            //var costs = db.TravelCosts.Where(x => x.TravelId == dtoFirst.Id).Select(x => x.Value).ToList();
            //var persons = db.TravelFriends.Where(x => x.TravelId == dtoFirst.Id).Select(x => x.Person).ToList();



            return mapper.Map<List<TravelDto>>(data);


        }
        public void DeleteTravel(int id)
        {
            var model = db.Travels.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                db.Travels.Remove(model);
                db.SaveChanges();
            }
        }

        #endregion

        #region Friend
        public List<TravelFriendDto> TravelFriends(int travelId)
        {
            var data = db.TravelFriends.Where(x => x.TravelId == travelId).ToList();
            return mapper.Map<List<TravelFriendDto>>(data);
        }

        public TravelFriendDto TravelFriend(int FriendId)
        {

            var data = db.TravelFriends.FirstOrDefault(x => x.Id == FriendId);

            return mapper.Map<TravelFriendDto>(data);
        }

        public void SaveTravelFriend(TravelFriendDto travelFriends)
        {
            var model = mapper.Map<TravelFriendEntity>(travelFriends);

            if (model.Id > 0)
                db.TravelFriends.Update(model);
            else
                db.TravelFriends.Add(model);
            db.SaveChanges();
            RecalculateShares(travelFriends.TravelId);

        }

        // داخل TravelService (یا هر جایی که _db/context هست)
        public void RecalculateShares(int travelId)
        {
            var travel = db.Travels
                .Include(t => t.TravelFriends)
                .Include(t => t.TravelCosts)
                    .ThenInclude(c => c.TravelCostFriends)
                .FirstOrDefault(t => t.Id == travelId);

            if (travel == null) return;

            int friendCount = travel.TravelFriends?.Count ?? 0;
            if (friendCount == 0)
            {
                // اگر دوست نیست، می‌توانیم تمام TravelCostFriendها را پاک کنیم یا کاری نکنیم
                return;
            }

            foreach (var cost in travel.TravelCosts)
            {
                // فرض: cost.Amount از جنس decimal
                decimal Value = Convert.ToDecimal(cost.Value);

                // پاک کردن سهم‌های قبلی (اگر لود شده باشند)
                if (cost.TravelCostFriends != null && cost.TravelCostFriends.Any())
                    db.TravelCostFriends.RemoveRange(cost.TravelCostFriends);

                decimal rawShare = Value / friendCount;
                // گرد کردن به 2 رقم اعشار (یا هر قاعده‌ای که می‌پسندی)
                decimal share = Math.Round(rawShare, 2, MidpointRounding.AwayFromZero);

                foreach (var friend in travel.TravelFriends)
                {
                    var tcf = new TravelCostFriendEntity
                    {
                        TravelCostId = cost.Id,
                        TravelFriendId = friend.Id,
                        Value = share // فرض: Share هم decimal است
                    };
                    db.TravelCostFriends.Add(tcf);
                }
            }

            db.SaveChanges();
        }

        #endregion

        #region Cost
        //public void SaveTravelCost(TravelCostDto travelCost)
        //{

        //    var model = mapper.Map<TravelCostEntity>(travelCost);
        //    bool add = true;
        //    if (model.Id > 0)
        //    {
        //        db.TravelCosts.Update(model);
        //         add = false;
        //    }
        //    else
        //    {
        //        db.TravelCosts.Add(model);
        //         add = true;
        //    }

        //    db.SaveChanges();

        //    var travelCostCalc = TravelCostCalcFactory.Create(travelCost.Type);
        //    travelCostCalc.Calc(model.Id,model.TravelId,add);


        //}
        public TravelCostDto TravelCost(int? travelCostId, int travelId)
        {
            TravelCostDto travelCost;

            if (travelCostId > 0)
            {
                var query =
                    from tc in db.TravelCosts
                    where tc.Id == travelCostId
                    select new TravelCostDto
                    {
                        Id = tc.Id,
                        Title = tc.Title,
                        Value = tc.Value,
                        Type = tc.Type,
                        TravelId = travelId,
                        TravelCostFriends = (
                            from tcf in db.TravelCostFriends
                            where tcf.TravelCostId == tc.Id
                            join tf in db.TravelFriends on tcf.TravelFriendId equals tf.Id
                            select new TravelCostFriendDto
                            {
                                Id = tcf.Id,
                                TravelCostId = tcf.TravelCostId,
                                TravelFriendId = tcf.TravelFriendId,
                                Value = tcf.Value,
                                Rate = tcf.Rate,
                                FriendName = tf.FristName + " " + tf.LastName
                            }
                        ).ToList()
                    };

                travelCost = query.FirstOrDefault();
            }
            else
            {
                travelCost = new TravelCostDto
                {
                    TravelId = travelId,
                    TravelCostFriends = (
                        from tf in db.TravelFriends
                        where tf.TravelId == travelId
                        select new TravelCostFriendDto
                        {
                            TravelFriendId = tf.Id,
                            FriendName = tf.FristName + " " + tf.LastName,
                        }
                    ).ToList()
                };
            }

            travelCost.CostTypes = TravelCostTypes();

            return travelCost;
        }
        public IBaseResult SaveTravelCost(TravelCostDto travelCost)
        {
            var travelCostCalc = TravelCostCalcFactory.Create(travelCost.Type, db);
            var validate = travelCostCalc.Validate(travelCost);
            if (!validate.Result)
                return validate;

            var modelCalc = travelCostCalc.Calc(travelCost);

            var model = mapper.Map<TravelCostEntity>(modelCalc);


            if (model.Id <= 0)
                db.TravelCosts.Add(model);

            else
                db.TravelCosts.Update(model);
            db.SaveChanges();
            return new BaseResult(true);
        }


        public List<TravelCostDto> TravelCosts(int travelId)
        {

            var data = db.TravelCosts.Where(x => x.TravelId == travelId).ToList();
            var list = mapper.Map<List<TravelCostDto>>(data);
            var costTypes = TravelCostTypes();

            foreach (var tc in list)
            {
                var type = costTypes.FirstOrDefault(x => x.Value == tc.Type.ToString());
                tc.TypeStr = type != null ? type.Text : "نامشخص"; // یا مقدار پیش‌فرض دلخواه
            }




            return list;
        }
        private List<SelectListItem> TravelCostTypes() => TravelCostCalcFactory.SelectTypes();
        //public TravelCostDto TravelCost(int? travelCostId, int travelId)
        //{

        //    TravelCostDto travelCost;

        //    if (travelCostId > 0)
        //    {
        //        //var cost = db.TravelCosts
        //        //    .Include(x => x.TravelCostFriends.Where(z => z.TravelCostId == travelCostId))
        //        //    .FirstOrDefault(x => x.Id == travelCostId);

        //        var query = (from tc in db.TravelCosts
        //                     where tc.Id == travelCostId
        //                     join tcf in db.TravelCostFriends on tc.Id equals tcf.TravelCostId into jtcf
        //                     join tf in db.TravelFriends on tc.TravelId equals tf.TravelId into jtf
        //                     select new { tc, jtcf, jtf });

        //        var list = query.ToList();
        //        travelCost = list.Select(x => new TravelCostDto
        //        {
        //            Id = x.tc.Id,
        //            Title = x.tc.Title,
        //            Value = x.tc.Value,
        //            Type = x.tc.Type,
        //            TravelCostFriends = x.jtcf.Select(f => new TravelCostFriendDto
        //            {
        //                TravelFriendId = f.TravelFriendId,
        //                TravelCostId = x.tc.Id,
        //                Id = f.Id,
        //                Rate = f.Rate,
        //                Value = f.Value,
        //                FriendName = x.jtf.Select(z => $"{z.FristName} {z.LastName}").First()
        //            }).ToList(),
        //        }).First();
        //    }
        //    else
        //    {
        //        travelCost = new TravelCostDto
        //        {
        //            TravelId = travelId,
        //            TravelCostFriends = db.TravelFriends.Where(x => x.TravelId == travelId)
        //                .Select(x => new TravelCostFriendDto
        //                {
        //                    FriendName = $"{x.FristName} {x.LastName}",
        //                    TravelFriendId = x.Id
        //                }).ToList()
        //        };
        //    }
        //    travelCost.CostTypes = TravelCostTypes();

        //    return travelCost;
        //}


        public void DeleteTravelCost(int id)
        {
            var model = db.TravelCosts.FirstOrDefault(x => x.Id == id);

            db.TravelCosts.Remove(model);
            var travelCostFriend = db.TravelCostFriends.Where(x => x.TravelFriendId == id);
            foreach (var tcf in travelCostFriend)
                db.TravelCostFriends.Remove(tcf);
            db.SaveChanges();
        }


        #endregion


    }
}
