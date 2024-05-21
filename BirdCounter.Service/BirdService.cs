using BirdCounter.Core;
using BirdCounter.Model;

namespace BirdCounter.Service
{
	public class BirdService
	{
		private readonly BirdDbContext _dbContext;
		public BirdService(BirdDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IList<Bird> Find()
		{
			return _dbContext.Birds.ToList();
		}

		public Bird Find(int id)
		{
			return _dbContext.Birds.FirstOrDefault(b => b.Id == id);
		}

		public async void Add(Bird bird)
		{
            if (bird.ImageFile != null && bird.ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(bird.ImageFile.FileName);
                var fileExtension = Path.GetExtension(fileName).ToLower();
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await bird.ImageFile.CopyToAsync(stream);
                }

                bird.ImagePath = "/Images/" + fileName;
            }
            _dbContext.Birds.Add(bird);
			_dbContext.SaveChanges();
		}

		public async void Update(Bird bird)
		{
            if (bird.ImageFile != null && bird.ImageFile.Length > 0)
            {
                var oldBird = Find(bird.Id);
                if (oldBird != null)
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", oldBird.ImagePath);
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                }
                var fileName = Path.GetFileName(bird.ImageFile.FileName);
                var fileExtension = Path.GetExtension(fileName).ToLower();
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await bird.ImageFile.CopyToAsync(stream);
                }

                bird.ImagePath = "/Images/" + fileName;
            }
            _dbContext.Birds.Update(bird);
			_dbContext.SaveChanges();
		}

		public void Delete(int id)
		{
			var bird = Find(id);
			if (bird != null)
			{
				_dbContext.Birds.Remove(bird);
				_dbContext.SaveChanges();
			}
		}
	}
}
