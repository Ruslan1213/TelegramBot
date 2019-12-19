using System.Collections.Generic;
using Introspect.DAL.Repositories;
using Introspekt.DAL.Models;
using Introspekt_bot.Abstraction;

namespace Introspekt_bot.Service
{
    public class CourseService: ICourseService
    {
        private readonly IRepository<Course> _courseRepository;

        public CourseService(IRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public void Create(Course course)
        {
            if (course != null)
            {
                _courseRepository.Create(course);
            }
        }

        public void Update(Course course)
        {
            if (course != null)
            {
                _courseRepository.Update(course);
            }
        }

        public void Delete(Course course)
        {
            if (course != null)
            {
                _courseRepository.Delete(course);
            }
        }

        public IEnumerable<Course> GetAll()
        {
            return _courseRepository.GetAll();
        }

        public Course Get(int id)
        {
            return _courseRepository.Get(id);
        }
    }
}
