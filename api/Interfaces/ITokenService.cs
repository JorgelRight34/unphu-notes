using System;
using api.Data;

namespace api.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
