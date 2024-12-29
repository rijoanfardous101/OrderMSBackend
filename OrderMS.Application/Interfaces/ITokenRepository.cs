using System;
using OrderMS.Domain.Entities;

namespace OrderMS.Application.Interfaces
{
    public interface ITokenRepository
    {
        /// <summary>
        /// Generates JWT Token and returns it.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        string CreateJWTToken(ApplicationUser user, List<string> roles);
    }
}
