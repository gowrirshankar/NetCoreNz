using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest loginRequest)
        {
            //check if the user is authenticated(Check username and password)            
            //We are using fluent validator to check whether the loginRequest is empty or not. No need to validate the same here.

            var user = await userRepository.AuthenticateUserAsync(loginRequest.Username, loginRequest.Password);

            if(user != null)
            {
                //Generate JWT token                
                var token = await tokenHandler.CreateTokenAsync(user);
                return Ok(token);
            }

            return BadRequest("Username or password is incorrect.");
        }
    }
}
