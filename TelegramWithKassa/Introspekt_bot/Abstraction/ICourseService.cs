using System.Collections.Generic;
using Introspekt.DAL.Models;

namespace Introspekt_bot.Abstraction
{
    public interface ICourseService
    {
        void Create(Course course);

        void Update(Course course);

        void Delete(Course course);

        IEnumerable<Course> GetAll();

        Course Get(int id);
    }
}
