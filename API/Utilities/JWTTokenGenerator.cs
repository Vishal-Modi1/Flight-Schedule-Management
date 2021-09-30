using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Configuration;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace API.Utilities
{
    public class JWTTokenGenerator
    {
        private readonly ConfigurationSettings _configurationSettings;

        public JWTTokenGenerator()
        {
            _configurationSettings = ConfigurationSettings.Instance;
        }

        public string Generate(string username, List<string> roles)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, username)
            };

            roles.ForEach(role =>
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            });

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationSettings.JWTKey));
            DateTime expires = DateTime.Now.AddDays(_configurationSettings.JWTExpireDays);

            //  DateTime expires = DateTime.Now.AddSeconds(15);

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configurationSettings.JWTIssuer,
                _configurationSettings.JWTIssuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
