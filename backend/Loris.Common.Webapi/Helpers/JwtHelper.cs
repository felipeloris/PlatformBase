using Loris.Common.Domain.Interfaces;
using Loris.Common.Helpers;
using Loris.Common.Webapi.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Loris.Common.Webapi.Helpers
{
    public static class JwtHelper
    {
        private static AppSettings Settings { get; set; } = new AppSettings();
        private static RandomNumberGenerator Rng = RandomNumberGenerator.Create();

        public static void AddJwtBearer(this IServiceCollection services, AppSettings settings)
        {
            if (settings != null)
                Settings = settings;

            var key = Loadkey();

            services
                .AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Settings.Jwt.Issuer,
                        ValidAudience = Settings.Jwt.Audience,
                        IssuerSigningKey = key
                    };
                });
        }

        public static JwtResult GenerateJsonWebToken(ILogin login)
        {
            var key = Loadkey();

            var claims = new List<Claim> {
                new Claim(LorisClaimTypes.Id, login.ExtenalId),
                new Claim(LorisClaimTypes.Login, LoginHelper.EncriptLogin(login))
            };

            /*
                var roles = await _userManager.GetRolesAsync(appUser);
                if (roles != null)
                {
                    claims.AddRange(from role in roles
                                    select new Claim(ClaimTypes.Role, role));
                }
             */

            var token = new JwtSecurityToken(
                Settings.Jwt.Issuer,
                Settings.Jwt.Audience,
                claims,
                expires: login.LoginAt.AddMinutes(Settings.Jwt.ExpireMinutes),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                );

            var result = new JwtResult();
            result.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
            result.LoginAt = login.LoginAt;
            result.TokenExpiresIn = (int)result.LoginAt.AddHours(Settings.Jwt.ExpireMinutes * 60).Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMinutes;
            return result;
        }

        private static SecurityKey Loadkey()
        {
            var dirSystem = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var pathFile = Path.Combine(dirSystem, "jwt.key");
            if (File.Exists(pathFile))
                return JsonSerializer.Deserialize<JsonWebKey>(File.ReadAllText(pathFile));

            var newKey = CreateJWK();
            File.WriteAllText(pathFile, JsonSerializer.Serialize(newKey));
            return newKey;
        }

        private static JsonWebKey CreateJWK()
        {
            var symetricKey = new HMACSHA256(GenerateKey(64));
            var jwk = JsonWebKeyConverter.ConvertFromSymmetricSecurityKey(new SymmetricSecurityKey(symetricKey.Key));
            jwk.KeyId = Base64UrlEncoder.Encode(GenerateKey(16));
            return jwk;
        }

        private static byte[] GenerateKey(int bytes)
        {
            var data = new byte[bytes];
            Rng.GetBytes(data);
            return data;
        }

        public static ILogin GetLogin(ClaimsPrincipal currentUser)
        {
            try
            {
                if (currentUser == null) return null;

                if (currentUser.HasClaim(c => c.Type.Equals(LorisClaimTypes.Login)))
                {
                    var encriptedLogin = currentUser.Claims.FirstOrDefault(c => c.Type.Equals(LorisClaimTypes.Login)).Value;
                    var userLogin = LoginHelper.DecriptLogin(encriptedLogin);
                    return userLogin;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
    }
}
