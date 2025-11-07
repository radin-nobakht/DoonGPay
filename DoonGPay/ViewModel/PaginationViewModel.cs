namespace DoonGPay.ViewModel
{
    public class PagationViewModel<T>
    {
        public List<T> List { get; set; }

        public PagationModel PagationModel { get; set; }


    }
    public class PagationModel
    {
        public int TotalItem { get; set; }
        public int ItemInpage { get; set; } = 5;
        public int TotalPage => (int)Math.Ceiling((decimal)TotalItem / ItemInpage);
        public int CurrentPage { get; set; } = 1;
        public int PageShow { get; set; } = 7;
        public int ShowFirstPage
        {
            get
            {
                int value = CurrentPage - PageShow / 2;

                value = value + PageShow > TotalPage ? TotalPage - PageShow + 1 : value;

                value = value < 1 ? 1 : value;
                return value;
            }
        }
        public int ShowLastPage
        {
            get
            {
                int value = ShowFirstPage + PageShow - 1;
                value = value > TotalPage ? TotalPage : value;

                return value;
            }
        }
        public string Url { get; set; }
    }
}
