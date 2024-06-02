using BirdCounter.Core;
using BirdCounter.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCounter.Service
{
    public class BirdService
    {
        private readonly BirdDbContext _birdDbContext;
        public BirdService(BirdDbContext birdDbContext)
        {
            _birdDbContext = birdDbContext;
        }

        public IList<Bird> Find()
        {
            var birds = _birdDbContext.Birds.ToList();
            return birds;
        }

        public Bird Find(int id)
        {
            var bird = _birdDbContext.Birds.Find(id);
            return bird;
        }

        public Bird Add(Bird bird)
        {
            _birdDbContext.Birds.Add(bird);
            _birdDbContext.SaveChanges();
            return bird;
        }

        public Bird Update(Bird bird)
        {
            _birdDbContext.Birds.Update(bird);
            _birdDbContext.SaveChanges();
            return bird;
        }

        public void Delete(int id)
        {
            var bird = _birdDbContext.Birds.Find(id);
            if (bird != null)
            {
                _birdDbContext.Birds.Remove(bird);
                _birdDbContext.SaveChanges();
            }
        }
    }
}
