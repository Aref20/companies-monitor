namespace MSGCompaniesMonitor.Models
{
    public class Pagination<T>
    {
        public List<T> Data { get; set; }
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
    }
}
