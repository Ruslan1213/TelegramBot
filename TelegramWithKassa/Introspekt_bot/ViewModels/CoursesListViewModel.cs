using System.Collections.Generic;
using Introspekt_bot.MessagePagination;

namespace Introspekt_bot.ViewModels
{
    public class CoursesListViewModel
    {
        public IEnumerable<CourseInfo> CoursesInfo { get; set; }

        public MessageInfo PagingInfo { get; set; }
    }
}
