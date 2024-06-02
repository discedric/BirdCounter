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
    public class CountingSessionService
    {
        private readonly BirdDbContext _birdDbContext;
        public CountingSessionService(BirdDbContext birdDbContext) 
        { 
            _birdDbContext = birdDbContext;
        }

        public IList<CountingSession> Find()
        {
            var session = _birdDbContext.CountingSessions.Include(cs => cs.User).ToList();
            return session;
        }

        public CountingSession Find(int id)
        {
            var session = _birdDbContext.CountingSessions.Include(cs => cs.User).Include(cs => cs.BirdCounts).FirstOrDefault(cs=>cs.Id==id);
            return session;
        }

        public CountingSession Add(CountingSession countingSession)
        {
            _birdDbContext.CountingSessions.Add(countingSession);
            _birdDbContext.SaveChanges();
            return countingSession;
        }

        public CountingSession Update(CountingSession countingSession)
        {
            _birdDbContext.CountingSessions.Update(countingSession);
            _birdDbContext.SaveChanges();
            return countingSession;
        }

        public void Delete(int id)
        {
            var countingSession = _birdDbContext.CountingSessions.Find(id);
            if (countingSession != null)
            {
                _birdDbContext.CountingSessions.Remove(countingSession);
                _birdDbContext.SaveChanges();
            }
        }
    }
}
