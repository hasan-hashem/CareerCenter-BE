namespace Application.Common.Models
{

    public class Pagination
    {
        public int TotalItemCount { get; set; }
        public int TotalPageCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public Pagination(int totalItemCount, int pageNumber, int pageSize)
        {
            TotalItemCount = totalItemCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPageCount = (int)Math.Ceiling((decimal)(TotalItemCount / PageSize));
        }


    }
}
