namespace TaskManagement.Models
{
    public class GetRecordsResult<T>
    {
        /// <summary>
        /// All records existing count
        /// </summary>
        public int TotalRecords { get; set; }
        /// <summary>
        /// All existing pages count
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// Number of current page in pagination
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// Number of records in page
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Records found in current search
        /// </summary>
        public List<T> Records { get; set; }

        public GetRecordsResult(int totalItems, int totalPages, int currentPage, int pageSize, List<T> items)
        {
            TotalRecords = totalItems;
            TotalPages = totalPages;
            CurrentPage = currentPage;
            PageSize = pageSize;
            Records = items;
        }
        public GetRecordsResult() { }
    }
}
