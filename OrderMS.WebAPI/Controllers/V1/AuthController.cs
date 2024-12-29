using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderMS.Application.DTOs;
using OrderMS.Application.Interfaces;
using OrderMS.Domain.Entities;
using OrderMS.WebAPI.Controllers.V1.Base;

namespace OrderMS.WebAPI.Controllers.V1
{
    public class AuthController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICompanyRepository _companyService;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<ApplicationUser> userManager, ICompanyRepository companyService, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _companyService = companyService;
            _tokenRepository = tokenRepository;
        }


        [HttpPost]
        [Route("Signup")]
        public async Task<IActionResult> Signup([FromBody] SignupReqDTO signupReqDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var companyId = await _companyService.CreateCompanyAsync(signupReqDTO);

            ApplicationUser newUser = new ApplicationUser()
            {
                UserName = signupReqDTO.EmailAddress,
                Email = signupReqDTO.EmailAddress,
                CompanyId = companyId,
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, signupReqDTO.Password);

            if (result.Succeeded) 
            {
                await _userManager.AddToRoleAsync(newUser, "Admin");

                if (result.Succeeded)
                {
                    return Ok("Account Created Successfully! Please Login.");
                }
            }

            return BadRequest("Something went wrong. Please try again.");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginReqDTO loginReqDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginReqDTO.EmailAddress);

            if (user != null)
            {
                var isPassCorrect = await _userManager.CheckPasswordAsync(user, loginReqDTO.Password);

                if (isPassCorrect)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        var jwtToken = _tokenRepository.CreateJWTToken(user, roles.ToList());

                        var loginResDTO = new LoginResDTO()
                        {
                            JwtToken = jwtToken,
                        };

                        return Ok(loginResDTO);
                    }

                }
            }

            return BadRequest("Email/Passowrd incorrect.");
        }
    }
}
