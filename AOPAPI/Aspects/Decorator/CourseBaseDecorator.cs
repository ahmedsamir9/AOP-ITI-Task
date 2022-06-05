using AOPAPI.BLL;
using AOPAPI.DAL;
using AOPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AOPAPI.Aspects.Decorator
{
    public class CourseBaseDecorator : ICourseService
    {
        private readonly ICourseService _courseService;

        public CourseBaseDecorator(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public virtual bool Delete(DeleteCourseInput input)
        {
            return _courseService.Delete(input);
        }

        public virtual IEnumerable<Course> GetAll()
        {
           return _courseService.GetAll();
        }

        public virtual Course GetById(int id)
        {
           return _courseService.GetById(id);
        }
    }
}