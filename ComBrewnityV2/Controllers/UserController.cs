using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using ComBrewnityV2.Auth;
using ComBrewnityV2.DTOs.ViewModels;
using ComBrewnityV2.Exceptions;
using ComBrewnityV2.Helpers;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ComBrewnityV2.Controllers
{
    [EnableCors]
    [Route("api/ver1/[controller]/[action]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public UserController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
             IJwtFactory jwtFactory, 
             IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }


        // POST api/ver1/User/Login
        //public async Task<IActionResult> AdminLogin(LoginViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
        //    if (result.Succeeded)
        //        return Ok("Toppen! Grattis!");
        //    else
        //        return BadRequest("Försök igen");

        //}

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                //FirstName = model.FirstName,
                //LastName = model.LastName
                //Created = DateTime.UtcNow
            };

            IdentityResult result;
            ApplicationUser existingUser = await _userManager.FindByEmailAsync(model.Email);

            if (existingUser != null)
            {
                throw new ApiDataException(110, "User already exists", HttpStatusCode.BadRequest);  //User already  registred
            }

            //new user
            try
            {
                result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return new BadRequestObjectResult((result, ModelState));
            }
            catch (DbUpdateException due)
            {
                throw due;
            }

            return new OkObjectResult("Account created");
        }

        //resource: https://fullstackmark.com/post/13/jwt-authentication-with-aspnet-core-2-web-api-angular-5-net-core-identity-and-facebook-login
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await GetClaimsIdentity(model.Email, model.Password);
            if (identity == null)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
            }

            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, model.Email, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByEmailAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}