using AllProject.Server.Helper;
using AllProject.Server.Services.Interfaces;
using AllProject.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AllProject.Server.Controllers
{
    public class UserController : Controller
    {
        private readonly IMongoRepository<User> _repository;
        private readonly IEmailService _emailService;
        private readonly AppStateManager _appStateManager;
        public UserController(IMongoRepository<User> repository, IEmailService emailService, AppStateManager appStateManager)
        {
            _repository = repository;
            _emailService = emailService;
            _appStateManager = appStateManager;
        }
        [HttpGet("UserList")]
        public async Task<IEnumerable<User>> UserList()
        {
            var response = _repository.AsQueryable();
            return await Task.FromResult(response);
        }
        [HttpGet("UserCodeCheck")]
        public async Task<bool> UserCodeCheck(string email, string code)
        {
            bool rsp = false;
            var response = _repository.AsQueryable().Where(x => x.EmailAddress == email && x.Code == code).ToList();
            if (response.Count == 0)
            {
                rsp = false;
            }
            else
            {
                rsp = true;
            }
            return rsp;
        }
        [HttpPost("UserLogin")]
        public async Task<User> UserList([FromBody] User model)
        {
            var response = _repository.AsQueryable();
            var users = response.FirstOrDefault(x => x.EmailAddress == model.EmailAddress.Trim() && x.Password == model.Password.Trim());
            if (users == null) return new User();
            var code = _appStateManager.GenerateRandomNo();
            users.Code = code.ToString();
            _repository.ReplaceOne(users);
            await _emailService.SendEmail(users.EmailAddress, "Kullanici giris Yapti", ("<h1>Sayin :" + users.FirstName + " " + users.LastName + "</h1></br>" + "Giris Icin Kodunuz:" + code));
            return users;
        }

        // POST api/values
        [HttpPost("UserAdd")] 
        public async Task UserAdd([FromBody] User user)
        {
            await _repository.InsertOneAsync(user);
        }
    }
}
