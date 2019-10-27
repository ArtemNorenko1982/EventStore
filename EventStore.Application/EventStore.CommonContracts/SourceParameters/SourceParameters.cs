using System.Collections.Generic;

namespace EventStore.CommonContracts.SourceParameters
{
    public class SourceParameters
    {
        private const int MaxPageSize = 20;

        private int _itemPerPage = 20;

        public List<int> PersonIds { get; set; } = new List<int>();

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _itemPerPage;
            set => _itemPerPage = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}
