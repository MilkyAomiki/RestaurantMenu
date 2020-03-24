using System;

namespace Restaurant_menu.Pagination
{
    /// <summary>
    /// Index view's pages model
    /// </summary>
    public class PageViewModel
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }

        public PageViewModel(int itemsCount, int pageNumber, int pageSize)
        {
            TotalPages = (int)Math.Ceiling((Double)itemsCount / pageSize);

            PageNumber = TotalPages >= pageNumber? pageNumber: 1;

        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }
        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }

    }
}
