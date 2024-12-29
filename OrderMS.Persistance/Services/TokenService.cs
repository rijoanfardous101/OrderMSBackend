using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OrderMS.Application.Interfaces;
using OrderMS.Domain.Entities;

namespace OrderMS.Persistence.Services
{
    public class TokenService : ITokenRepository
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string CreateJWTToken(ApplicationUser user, List<string> roles)
        {
            if(user == null || string.IsNullOrWhiteSpace(user.Email))
                throw new ArgumentException("User information is invalid.");

            if (roles == null || roles.Count == 0)
                throw new ArgumentException("Roles cannot be null or empty.");


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Validate Configuration Keys
            var jwtKey = _configuration[ "Jwt:Key" ]
                 ?? throw new InvalidOperationException("JWT Key is not configured.");
            var issuer = _configuration[ "Jwt:Issuer" ]
                         ?? throw new InvalidOperationException("JWT Issuer is not configured.");
            var audience = _configuration[ "Jwt:Audience" ]
                           ?? throw new InvalidOperationException("JWT Audience is not configured.");

            if (jwtKey.Length < 32) 
                throw new InvalidOperationException("JWT Key must be at least 32 characters long.");



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
