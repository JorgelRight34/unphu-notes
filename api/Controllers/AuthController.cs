using api.DTOs.User;
using api.Interfaces;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class AuthController(IAuthRepository authRepository) : ApiBaseController
    {
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] GoogleLoginRequestDto request)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(request.Token);
            if (payload == null) return BadRequest("Invalid token");

            var user = await authRepository.SignInFromGoogleTokenAsync(payload);
            if (user == null) return BadRequest();
            
            return Ok(user);
        }
    }
}
