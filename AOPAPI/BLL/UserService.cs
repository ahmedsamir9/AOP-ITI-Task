using AOPAPI.DAL;
using AOPAPI.DAL.Repositories;
using AOPAPI.Models;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOPAPI.BLL
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;

        public UserService(
            IUserRepository userRepository,
            ICourseRepository courseRepository
            )
        {
            _userRepository = userRepository;
            _courseRepository = courseRepository;
        }
        public IEnumerable<User> GetAll()
        {
            var users = _userRepository.GetAll();
            return users;
        }

        public User GetById(int id)
        {
            var user = _userRepository.GetById(id);
            return user;
        }

        public bool AssignCourse(AssignCourseInput input)
        {
            var user = _userRepository.GetById(input.UserId);
            var course = _courseRepository.GetById(input.CourseId);
            return _userRepository.AssignCourse(user, course);
        }
    }
}
