using api.DTOs.User;
using api.Interfaces;
using AutoMapper;
using Google.Apis.Auth;
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
        public async Task<ActionResult> Register([FromBody] GoogleLoginRequestDto request)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(request.Token);
            if (payload == null) return BadRequest("Invalid token");

            var user = await authRepository.SignInFromGoogleTokenAsync(payload);
            if (user == null) return BadRequest();

            user = await subjectGroupRepository.AssignStudentToSubjectGroups(user);
            var userDto = mapper.Map<UserDto>(user);
            userDto.Token = tokenService.CreateToken(user);

            return Ok(user);
        }
    }
}
