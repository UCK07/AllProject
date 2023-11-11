using AllProject.Server.Helper;
using AllProject.Server.Services.Interfaces;
using AllProject.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AllProject.Server.Controllers
{
    public class MovieController : ControllerBase
    {

        protected readonly IMongoRepository<Movie> _movieService;
        protected readonly IImageUploadService _imageUploadService;
        protected readonly AppStateManager _appStateManager;

        public MovieController(IMongoRepository<Movie> movieService, AppStateManager appStateManager, IImageUploadService imageUploadService)
        {
            _movieService = movieService;
            _appStateManager = appStateManager;
            _imageUploadService = imageUploadService;
        }

        [HttpGet("MovieList")]



        public async Task<IEnumerable<Movie>> MovieList()
        {
            var response = _movieService.AsQueryable();
            return response;
        }

        [HttpPost("MovieAdd")]
        public async Task MovieAdd([FromBody] Movie movie)
        {
            var cloudUrl = "";
            if (movie.ImageUrl != null)
            {
                var newImageName = await _appStateManager.PrepareUniqueImageName(movie.ImageUrl);
                movie.ImageUrl = newImageName;
                string rootpath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Images/" + newImageName);

                var buf = Convert.FromBase64String(movie.ImageBase64);
                await System.IO.File.WriteAllBytesAsync(rootpath, buf);

                cloudUrl = await _imageUploadService.Upload(movie.Id, rootpath);
                movie.CloudUrl = cloudUrl;

                if (cloudUrl != null)
                {
                    if (System.IO.File.Exists(rootpath))
                    {
                        System.IO.File.Delete(rootpath);
                    }
                }
            }

            if (cloudUrl != null)
            {
                movie.ImageBase64 = string.Empty;
                await _movieService.InsertOneAsync(movie);
            }

        }
        [HttpPut("MovieUpdate")]
        public async Task BlogUpdate([FromBody] Movie movie)
        {
            await _movieService.ReplaceOneAsync(movie);

        }
        [HttpDelete("MovieListById/{id}")]
        public Task<IActionResult> Delete(string id)
        {
            var repository = _movieService.DeleteByIdAsync(id);

            return Task.FromResult<IActionResult>(NoContent());
        }
    }
}