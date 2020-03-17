using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineEducation.BLL.Interfaces;
using OnlineEducation.Common;
using OnlineEducation.DAL.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace OnlineEducation.BLL.Services
{
    public class UserService : IUserService
    {
        //users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User { Id = 1, Name = "Test", Surname= "User", Email= "test", Password = "test" }
        };

        private readonly AppSettings _appSettings;
        private readonly IStudentService _studentService;

        public UserService(IOptions<AppSettings> appSettings, IStudentService studentService)
        {
            _appSettings = appSettings.Value;
            this._studentService = studentService;
        }

        public User Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Email == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }
    }
}
