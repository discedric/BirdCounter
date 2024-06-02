using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdCounter.Core;
using BirdCounter.Model;

namespace BirdCounter.Service
{
    public class UserService
    {
        private readonly BirdDbContext _birdDbContext;
        public UserService(BirdDbContext birdDbContext)
        {
            _birdDbContext = birdDbContext;
        }

        public IList<User> Find()
        {
            var users = _birdDbContext.Users.ToList();
            return users;
        }

        public User Find(int id)
        {
            var user = _birdDbContext.Users.Find(id);
            return user;
        }
    }
}
