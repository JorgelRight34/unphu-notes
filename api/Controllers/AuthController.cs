using api.DTOs.SubjectGroup;
using api.DTOs.User;
using api.Extensions;
using api.Interfaces;
using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class AuthController(
        IAuthRepository authRepository,
        ISubjectGroupRepository subjectGroupRepository,
        ITokenService tokenService,
        IMapper mapper
    ) : ApiBaseController
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Register([FromBody] GoogleLoginRequestDto request)
        {
            // Validate token and get the data (payload) from the token
            var payload = await GoogleJsonWebSignature.ValidateAsync(request.Token);
            if (payload == null) return BadRequest("Invalid token");    // Ensure token is valid

            // Create user with the information available on the token
            var user = await authRepository.SignInFromGoogleTokenAsync(payload);
            if (user == null) return BadRequest();  // If user is null something went wrong

            // Create dto
            var userDto = mapper.Map<UserDto>(user);
            userDto.Token = tokenService.CreateToken(user);

            return Ok(userDto);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("subjects")]
        public async Task<ActionResult<List<SubjectGroupDto>>> GetSubjectGroups()
        {
            var username = User.GetUsername();

            var subjectGroups = await subjectGroupRepository.GetUserSubjectGroups(username);
            Console.WriteLine(subjectGroups);
            return Ok(subjectGroups);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<UserDto>> GetUserInfo()
        {
            var username = User.GetUsername();
            if (username == null) return BadRequest("Username is missing on the token.");

            var user = await authRepository.GetByUsernameAsync(username);
            return mapper.Map<UserDto>(user);
        }
    }
}
