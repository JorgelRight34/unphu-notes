using System;
using api.Data;
using api.Models;

namespace api.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
