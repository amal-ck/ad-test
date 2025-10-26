using backend.Data;
using backend.dto;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Processors
{
    public class LoginProcess
    {
        public MdlResponse FnLogin(DtoLogin dtoLogin, AppDbContext appDbContext, IConfiguration conf)
        {
            MdlResponse mdlResponse = new MdlResponse();
            var user = appDbContext.Users.FirstOrDefault(user => user.Username == dtoLogin.UserName);
            if (user == null)
            {
                throw new UnauthorizedAccessException("User Not Exist");
            }
            var passwordHasher = new PasswordHasher<User>();

            var passwordMatch = passwordHasher.VerifyHashedPassword(user, user.Password, dtoLogin.Password);
            if (passwordMatch == PasswordVerificationResult.Success)
            {
                mdlResponse.Data = FnGenerateJWTToken(user, conf);
                mdlResponse.Success = true;
                mdlResponse.Message = "Login Success";
                return mdlResponse;
            }
            else
            {
                throw new UnauthorizedAccessException("Username or Password Missmatch");
            }
        }

        public MdlResponse FnRegister(DtoRegister dtoRegister, AppDbContext appDbContext, IConfiguration conf)
        {
            MdlResponse mdlResponse = new MdlResponse();
            var existingUser = appDbContext.Users.FirstOrDefault(user => user.Username == dtoRegister.UserName);
            if (existingUser != null)
            {
                throw new BadHttpRequestException("Username already taken");
            }

            var passwordHasher = new PasswordHasher<User>();
            var user = new User
            {
                Name = dtoRegister.Name,
                Username = dtoRegister.UserName,
            };
            user.Password = passwordHasher.HashPassword(user, dtoRegister.Password);
            appDbContext.Users.Add(user);
            appDbContext.SaveChanges();

            mdlResponse.Data = FnGenerateJWTToken(user, conf);
            mdlResponse.Success = true;
            mdlResponse.Message = "Registration Succesfull";
            return mdlResponse;
        }

        public string FnGenerateJWTToken(User user, IConfiguration conf)
        {
            //JWT token gen
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(conf["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("UserId", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(conf["Jwt:ExpiryMinutes"])),
                Issuer = conf["Jwt:Issuer"],
                Audience = conf["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }
    }
}
