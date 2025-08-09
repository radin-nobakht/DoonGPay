using AutoMapper;
using DoonGPay.Adapter;
using DoonGPay.Dto.Travel;
using DoonGPay.Entity.Travel;
using DoonGPay.Inteface;
using DoonGPay.Inteface.Travel;
using DoonGPay.ViewModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;



namespace DoonGPay.Service.Travel
{
    public class TravelService(MyContext db, IMapper mapper, IMySession mySession) : ITravelService
    {
        #region TravelCostFriend

        public void SaveTravelCostFriend(TravelCostFriendDto travelCostFriend)
        {
            var model = mapper.Map<TravelCostFriendEntity>(travelCostFriend);
            if (model.Id > 0)
                db.TravelCostFriends.Update(model);
            else
                db.TravelCostFriends.Add(model);
            db.SaveChanges();
        }
        //public List<TravelCostFriendDto> TravelCostFriends(int travelCostId)
        //{
        //    var data = db.TravelCostFriends.Where(x => x.TravelCostId == travelCostId).ToList();

        //    return mapper.Map<List<TravelCostFriendDto>>(data);
        //}
        public void DeleteTravelCostFriend(int id)
        {
            var model = db.TravelCostFriends.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                db.TravelCostFriends.Remove(model);
                db.SaveChanges();
            }
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
            model.InsertDate = DateTime.Now;
            model.UserId = (int)mySession.UserId;
            if (model.Id > 0)
                db.Travels.Update(model);
            else
                db.Travels.Add(model);
            db.SaveChanges();
        }
        public TravelDto Travel(int? id) {
            TravelEntity data = new TravelEntity();
            if (id == 0)
            {
                 data = db.Travels
                              .Include(x => x.TravelFriends)
                              .Include(x => x.TravelCosts)
                              .FirstOrDefault(x => x.UserId == mySession.UserId);
               
            }
            else
            {
                 data = db.Travels
                             .Include(x => x.TravelFriends)
                             .Include(x => x.TravelCosts)
                             .FirstOrDefault(x => x.Id ==id);
            }
          

            return mapper.Map<TravelDto>(data);
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
        public void SaveTravelFriend(TravelFriendDto travelFriends)
        {
            var model = mapper.Map<TravelFriendEntity>(travelFriends);

            if (model.Id > 0)
                db.TravelFriends.Update(model);
            else
                db.TravelFriends.Add(model);
            db.SaveChanges();
        }
        //public List<TravelFriendDto> Friends_ShareByRow(int travelId)
        //{

        //    var data = db.TravelFriends.Where(x => x.TravelId == travelId).ToList();
        //    var model = mapper.Map<List<TravelFriendDto>>(data);
        //    if (model.Count > 0)
        //    {
        //        var costs = db.TravelCosts.Where(x => x.TravelId == travelId).ToList();
        //        var costmodel = mapper.Map<List<TravelCostDto>>(costs);

        //        // جمع تمام مقادیر هزینه‌ها
        //        var totalValue = costmodel.Sum(x => x.Value);

        //        // محاسبه سهم هر فرد
        //        var share = totalValue / model.Count;

        //        foreach (var friend in model)
        //        {
        //            friend.Share = share;
        //        }
        //    }


        //    return model;
        //}
        //public List<TravelFriendDto> Friends_ShareByPerson(int travelId)
        //{

        //    var data = db.TravelFriends.Where(x => x.TravelId == travelId).ToList();
        //    var model = mapper.Map<List<TravelFriendDto>>(data);
        //    var countPerson = model.Sum(x => x.Person);

        //    if (model.Count > 0)
        //    {
        //        var totalValue = db.TravelCosts.Where(x => x.TravelId == travelId).Sum(x => x.Value);


        //        // محاسبه سهم هر فرد
        //        decimal onePersonShare = (decimal)totalValue / (decimal)countPerson;

        //        foreach (var friend in model)
        //        {
        //            decimal share = onePersonShare * friend.Person;
        //            friend.Share = share;
        //        }
        //    }


        //    return model;
        //}
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

        public void DeleteTravelFriend(int id)
        {
            var model = db.TravelFriends.FirstOrDefault(x => x.Id == id);
            if (model.FristName != null && model.LastName != null && model.TravelId != 0 && model.Id != 0 && model.PhoneNumber != null && model.Person != 0 && model.Share != null)
            {
                db.TravelFriends.Remove(model);
                db.SaveChanges();
            }
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
        public void SaveTravelCost(TravelCostDto travelCost)
        {
            var model = mapper.Map<TravelCostEntity>(travelCost);
            bool isAdd = model.Id <= 0;

            if (isAdd)
            {
                db.TravelCosts.Add(model);
                db.SaveChanges();  // تا مدل Id بگیره
            }
            else
            {
                db.TravelCosts.Update(model);
                db.SaveChanges();
            }

            var friendsDto = travelCost.TravelCostFriends ?? new List<TravelCostFriendDto>();

            var friendIdsInDto = friendsDto.Select(f => f.Id).ToList();
            var friendsToDelete = db.TravelCostFriends
                .Where(f => f.TravelCostId == model.Id && !friendIdsInDto.Contains(f.Id))
                .ToList();

            db.TravelCostFriends.RemoveRange(friendsToDelete);

            foreach (var friendDto in friendsDto)
            {
                var friendEntity = mapper.Map<TravelCostFriendEntity>(friendDto);
                friendEntity.TravelCostId = model.Id;

                if (friendDto.Id <= 0)
                    db.TravelCostFriends.Add(friendEntity);
                else
                    db.TravelCostFriends.Update(friendEntity);
            }

            db.SaveChanges();

         var travelCostCalc = TravelCostCalcFactory.Create(travelCost.Type ,db,mapper);
         travelCostCalc.Calc(model.Id,model.TravelId,isAdd,friendsDto);

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
                            FriendName = tf.FristName + " " + tf.LastName
                        }
                    ).ToList()
                };
            }

            travelCost.CostTypes = TravelCostTypes();

            return travelCost;
        }

        public void DeleteTravelCost(int id)
        {
            var model = db.TravelCosts.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                db.TravelCosts.Remove(model);
                db.SaveChanges();
            }
        }
        #endregion


    }
}
