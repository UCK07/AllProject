using AllProject.Client.Helper;
using AllProject.Shared.Dto;
using AllProject.Shared.Entities;
using RestSharp;

namespace AllProject.Client.Services
{
    public class UserService
    {
        private readonly RestClient _restClient;
        public UserService(RestClient restClient) { _restClient = restClient; }
        public async Task<User> UserLogin(User model)
        {
            var rs = await _restClient.PostJsonAsync<User, User>(EndPoints.UserLogin, model);
            return rs;
        }

        public async Task<bool> UserCodeCheck(UserCodeCheck model)
        {
            var rs = await _restClient.PostJsonAsync<UserCodeCheck, bool>(EndPoints.UserCodeCheck, model);
            return rs;
        }
        public async Task<bool> PasswordRemember(UserCodeCheck model)
        {
            var rs = await _restClient.PostJsonAsync<UserCodeCheck, bool>(EndPoints.PasswordRemember, model);
            return rs;
        }
    }
}
