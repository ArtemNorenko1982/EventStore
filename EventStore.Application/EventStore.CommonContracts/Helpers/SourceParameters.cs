namespace BookService.WebApi.Helpers
{
    /// <summary>
    /// Provides functionality for paging
    /// </summary>
    public class SourceParameters
    {
        private const int MaxPageSize = 20;
        private int _itemPerPage = 20;
        public int PersonId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => _itemPerPage;
            set => _itemPerPage = value > MaxPageSize ? MaxPageSize : value;
        }

        //search params
        public string FirstName { get; set; } = null;
        public string LastName { get; set; } = null;
        public string CompanyName { get; set; } = null;
    }
}