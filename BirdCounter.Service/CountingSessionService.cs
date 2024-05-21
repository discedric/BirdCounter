using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdCounter.Core;
using BirdCounter.Model;

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
            return _birdDbContext.CountingSessions.ToList();
        }

        public CountingSession Find(int id)
        {
            return _birdDbContext.CountingSessions.FirstOrDefault(cs => cs.Id == id);
        }

        public CountingSession Find(CountingSession cose)
        {
            return _birdDbContext.CountingSessions.FirstOrDefault(cs => cs.Start == cose.Start && cs.End == cose.End && cs.Location == cose.Location);
        }

        public void Add(CountingSession countingSession)
        {
            _birdDbContext.CountingSessions.Add(countingSession);
            _birdDbContext.SaveChanges();
        }

        public void Update(CountingSession countingSession)
        {
            _birdDbContext.CountingSessions.Update(countingSession);
            _birdDbContext.SaveChanges();
        }

        public void Stop(int id)
        {
            var session = Find(id);
            session.End = DateTime.Now;
            Update(session);
        }
    }
}
