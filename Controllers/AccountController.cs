using AutoMapper;
using Backend_Task.Models.User;
using Backend_Task.Models.Users;
using Backend_Task.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        //POST : api/Account/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] CreateUser userDetails)
        {
            var errors = await _unitOfWork.AuthManager.RegisterUser(userDetails);

            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);

                }
                return BadRequest(ModelState);
            }
            return Ok();

        }
        //POST : api/Account/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] GetUser userDetails)
        {
            var authResponse = await _unitOfWork.AuthManager.LoginUser(userDetails);
            
            if (authResponse == null)
                return Unauthorized();


            
            return Ok(authResponse);

        }

        //POST : api/Account/RefreshToken
        [HttpPost]
        [Route("RefreshToken")]
        [Authorize]
        public async Task<IActionResult> RefreshToken([FromBody] ApiUserAuthenticationResponse userDetails)
        {
            var authResponse = await _unitOfWork.AuthManager.VerifyRefreshToken(userDetails);

            if (authResponse == null)
                return Unauthorized();

            return Ok(authResponse);

        }
    }
}

