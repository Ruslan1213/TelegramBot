using System;

namespace Introspekt_bot.MessagePagination
{
    public class MessageInfo
    {
        public MessageInfo(int page, int pageSize, int totalPages)
        {
            CurrentPage = page;
            ItemsPerPage = pageSize;
            TotalItems = totalPages;
        }

        public int TotalItems { get; set; }

        public int ItemsPerPage { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
}
