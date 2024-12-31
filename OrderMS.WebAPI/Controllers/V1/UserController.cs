using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderMS.Application.DTOs;
using OrderMS.Application.DTOs.CompanyDTOs;
using OrderMS.Application.DTOs.UserDTOs;
using OrderMS.Application.Interfaces;
using OrderMS.Domain.Entities;
using OrderMS.Domain.Enums;
using OrderMS.Persistence.Services;
using OrderMS.WebAPI.Controllers.V1.Base;

namespace OrderMS.WebAPI.Controllers.V1
{
    [Authorize(Roles = "Admin")]
    public class UserController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICompanyRepository _companyRepository;

        public UserController(UserManager<ApplicationUser> userManager, ICompanyRepository companyRepository) 
        { 
            _userManager = userManager;
            _companyRepository = companyRepository;
        }

        [Route("CreateUserAccount")]
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateUserReqDTO createUserReqDTO)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            if(!createUserReqDTO.Role.Equals("Manager") && !createUserReqDTO.Role.Equals("Employee"))
            {
                ModelState.AddModelError("Role", "Please provide a valid role.");
                return ValidationProblem(ModelState);
            }

            var companyId = User.FindFirst("CompanyId")?.Value;

            if (companyId != null) {
                ApplicationUser newUser = new ApplicationUser()
                {
                    UserName = createUserReqDTO.UserName,
                    Email = createUserReqDTO.EmailAddress,
                    CompanyId = Guid.Parse(companyId),
                };

                IdentityResult result = await _userManager.CreateAsync(newUser, createUserReqDTO.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, createUserReqDTO.Role);

                    if (result.Succeeded)
                    {
                        return Ok("Account Created Successfully!");
                    }
                }
            }

            ModelState.AddModelError("Error", "Something went wrong!");
            return ValidationProblem(ModelState);
        }


        [Route("UpdateUserRole")]
        [HttpPut]
        public async Task<IActionResult> UpdateUserRole([FromBody] UpdateRoleReqDTO updateRoleReqDTO)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            if (!updateRoleReqDTO.Role.Equals("Manager") && !updateRoleReqDTO.Role.Equals("Employee"))
            {
                ModelState.AddModelError("Role", "Please provide a valid role.");
                return ValidationProblem(ModelState);
            }

            var user = await _userManager.FindByIdAsync(updateRoleReqDTO.UserId.ToString());

            if (user == null)
                return NotFound();


            // Checking Admin's CompanyId and User's CompanyId
            var adminCompanyId = User.FindFirst("CompanyId")?.Value;

            if (!user.CompanyId.ToString().Equals(adminCompanyId))
                return Unauthorized();


            // Removing current role of user.
            var currentRoles = await _userManager.GetRolesAsync(user);

            var removeResult = await _userManager.RemoveFromRoleAsync(user, currentRoles[0]);

            if (!removeResult.Succeeded)
                return BadRequest("Failed to remove current roles.");


            // Assigning new role
            var addResult = await _userManager.AddToRoleAsync(user, updateRoleReqDTO.Role);

            if (!addResult.Succeeded)
                return BadRequest("Failed to update user role.");


            return Ok("User role updated successfully.");
        }


        [Route("RemoveUserAccount")]
        [HttpDelete]
        public async Task<IActionResult> RemoveUserAccount([FromBody] Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return NotFound();


            // Checking Admin's CompanyId and User's CompanyId
            var adminCompanyId = User.FindFirst("CompanyId")?.Value;

            if (!user.CompanyId.ToString().Equals(adminCompanyId))
                return Unauthorized();


            // Removing user
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return BadRequest("Failed to remove the user.");

            return Ok("User account removed successfully.");

        }

        [Route("UpdateCompanyInfo")]
        [HttpPut]
        public async Task<IActionResult> UpdateCompanyInfo(ChangeCompanyInfoReqDTO reqDTO)
        {
            var companyId = User.FindFirst("CompanyId")?.Value;
            if (companyId == null)
                return BadRequest();

            var company = await _companyRepository.UpdateCompanyInfoAsync(reqDTO, Guid.Parse(companyId));

            if(company == null) 
                return NotFound();

            var companyResDTO = new CompanyResDTO()
            {
                CompanyId = company.CompanyId,
                CompanyName = company.CompanyName,
                CompanyAddress = company.CompanyAddress,
                CompanyDescription = company.CompanyDescription,
                Email = company.Email,
                CreatedAt = company.CreatedAt,
                PicturePath = company.PicturePath,
            };

            return Ok(companyResDTO);
        }
    }
}
