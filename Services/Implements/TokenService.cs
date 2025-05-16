using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApiRest_Ev4.Services.Interfaces;
using DotNetEnv;
using Microsoft.IdentityModel.Tokens;

namespace ApiRest_Ev4.Services.Implements
{
    public class TokenService : ITokenService
    {
        public Task<string> GenerateToken()
        {
            var Claims = new List<Claim>(){
                new Claim ("Id", GenerateRandomId())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Env.GetString("JWT_SECRET")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: Claims,
                expires: DateTime.UtcNow.AddMonths(2),
                signingCredentials: creds
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return Task.FromResult(jwt);
        }

        private string GenerateRandomId(){

            var random = new Random();
            var id = random.Next(100000, 999999).ToString();
            return id;
        }
    }
}