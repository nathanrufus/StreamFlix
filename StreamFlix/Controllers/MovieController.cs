using Microsoft.AspNetCore.Mvc;
using StreamFlix.Data;
using StreamFlix.Models;
using StreamFlix.Services;
using System.Threading.Tasks;

namespace StreamFlix.Controllers
{
    public class MovieController : Controller
    {
        private readonly StreamFlixDbContext _context;
        private readonly S3Service _s3Service;
        private readonly DynamoDBService _dynamoDbService;

        public MovieController(StreamFlixDbContext context, S3Service s3Service, DynamoDBService dynamoDbService)
        {
            _context = context;
            _s3Service = s3Service;
            _dynamoDbService = dynamoDbService;
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie(Movie movie, IFormFile videoFile, IFormFile imageFile)
        {
            // Upload the video file to S3
            if (videoFile != null)
            {
                var videoUrl = await _s3Service.UploadFileAsync(videoFile.FileName, videoFile);
                movie.S3Url = videoUrl;
            }

            // Upload the image file to S3
            if (imageFile != null)
            {
                var imageUrl = await _s3Service.UploadFileAsync(imageFile.FileName, imageFile);
                movie.ImageUrl = imageUrl;
            }

            // Save movie metadata in DynamoDB and relational database
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            await _dynamoDbService.SaveMovieAsync(movie);

            return RedirectToAction("MovieList");
        }

        public async Task<IActionResult> DeleteMovie(string movieId)
        {
            var movie = _context.Movies.Find(movieId);
            if (movie != null)
            {
                // Delete files from S3
                if (!string.IsNullOrEmpty(movie.S3Url))
                    await _s3Service.DeleteFileAsync(movie.S3Url);

                if (!string.IsNullOrEmpty(movie.ImageUrl))
                    await _s3Service.DeleteFileAsync(movie.ImageUrl);

                // Delete metadata from DynamoDB and relational database
                await _dynamoDbService.DeleteMovieAsync(movieId);
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("MovieList");
        }
    }
}
