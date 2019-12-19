using Introspekt.DAL.Models;
using Introspekt_bot.ViewModels;

namespace Introspekt_bot.Resourse
{
    public static class GetterCourseInfo
    {
        static string Name = "Имя курса: ";

        static string Description = "\nОписание курса: ";

        static string Price = "\nЦена: ";

        public static string GetCourseInfo(Course course)
        {
            return Name + course.Name + "\n" + Description + "\n" + course.Description + "\n" + Price + course.Price;
        }

        public static string GetCourseInfo(CourseInfo course)
        {
            return Name + course.Name + Description + course.Description;
        }
    }
}
