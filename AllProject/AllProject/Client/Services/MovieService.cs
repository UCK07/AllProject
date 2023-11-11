using AllProject.Client.Helper;
using AllProject.Shared.Entities;
using RestSharp;

namespace AllProject.Client.Services
{
    public class MovieService
    {
        private readonly RestClient _restClient;
        public MovieService(RestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<IEnumerable<Movie>> GetMovieList()
        {
            var rs = await _restClient.GetJsonAsync<IEnumerable<Movie>>(EndPoints.MovieList);
            return rs;
        }

        public async Task<Movie> GetMovieById(int Id)
        {
            var rs = await _restClient.GetJsonAsync<Movie>(EndPoints.MovieById, new { Id });
            return rs;
        }


        public async Task<bool> MovieAdd(Movie model)
        {
            var rs = await _restClient.PostJsonAsync<Movie, bool>(EndPoints.MovieAdd, model);
            return rs;
        }

        public async Task<bool> MovieDelete(int Id)
        {
            var request = new RestRequest(EndPoints.MovieDelete)
                     .AddUrlSegment("Id", Id);

            var rs = await _restClient.DeleteAsync<bool>(request);

            return rs!;
        }
        public async Task<bool> MovieUpdate(Movie model)
        {
            var rs = await _restClient.PutJsonAsync<Movie, bool>(EndPoints.MovieUpdate, model);
            return rs;
        }
    }
}
