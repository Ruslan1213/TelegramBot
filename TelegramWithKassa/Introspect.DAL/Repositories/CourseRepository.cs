using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Introspekt.DAL;
using Introspekt.DAL.Models;

namespace Introspect.DAL.Repositories
{
    public class CourseRepository : IRepository<Course>
    {
        private readonly CourseStoreContext _context;

        public CourseRepository(CourseStoreContext context)
        {
            _context = context;
        }

        public void Create(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
        }

        public void Update(Course course)
        {
            _context.Courses.Update(course);
        }

        public int SelectMaxId()
        {
            return _context.Courses.Select(x => x.Id).Max();
        }

        public void Delete(Course course)
        {
            _context.Courses.Remove(course);
        }

        public IEnumerable<Course> GetAll()
        {
            return _context.Courses.ToList();
        }

        public Course Get(int id)
        {
            return _context.Courses.Find(id);
        }

        public bool IsExist(Expression<Func<Course, bool>> expression)
        {
            return _context.Courses.Any(expression);
        }
    }
}
