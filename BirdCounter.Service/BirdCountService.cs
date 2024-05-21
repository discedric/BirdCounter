using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdCounter.Model;

namespace BirdCounter.Service
{
    public class BirdCountService
    {
        private readonly CountingSessionService _countingSession;

        public BirdCountService(CountingSessionService countingSession)
        {
            _countingSession = countingSession;
        }

        public BirdCount GetBirdCountForSession(int birdId, CountingSession Csession)
        {
            foreach (var birdcount in Csession.BirdCounts)
            {
                if (birdcount.Id == birdId)
                {
                    return birdcount;
                }
            }
            return null;
        }

        public void Update(BirdCount birdCount, CountingSession CSession)
        {
            var session = _countingSession.Find(CSession);
            if (session == null)
            {
                throw new InvalidOperationException("Counting session not found.");
            }
            if (session.BirdCounts == null)
            {
                session.BirdCounts = new List<BirdCount>();
            }

            if (session.BirdCounts.Contains(birdCount))
            {
                session.BirdCounts.Remove(birdCount);
                session.BirdCounts.Add(birdCount);
            }
            throw new NotImplementedException();
        }
    }
}
