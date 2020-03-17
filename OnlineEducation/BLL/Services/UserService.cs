﻿using System;
using System.Text;
using System.Linq;
using OnlineEducation.DAL;
using OnlineEducation.Common;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq.Expressions;
using System.Collections.Generic;
using OnlineEducation.DAL.Entities;
using Microsoft.Extensions.Options;
using OnlineEducation.BLL.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;

namespace OnlineEducation.BLL.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        private readonly AppSettings _appSettings;

        public UserService(OnlineEducationDbContext dbContext,
            IOptions<AppSettings> appSettings) : base(dbContext)
        {
            this._appSettings = appSettings.Value;
        }

        public override async Task<User> Add(User newModel)
        {
            newModel.Password = Encryptor.Encrypt(newModel.Password);

            return await base.Add(newModel);
        }

        public override async Task<User> Get(int id)
        {
            var data = await base.Get(id);
            return data.WithoutPassword();
        }

        public override async Task<FilterResult<User>> Get(Expression<Func<User, bool>> expression, int start, int count)
        {
            var data = await base.Get(expression, start, count);

            foreach (var item in data.Data)
            {
                item.WithoutPassword();
            }

            return data;
        }

        public override async Task<List<User>> GetAll()
        {
            var data = await base.GetAll();
            data.ForEach(x => x.WithoutPassword());
            return data;
        }

        public override async Task<List<User>> Get(Expression<Func<User, bool>> expression)
        {
            var data = await base.Get(expression);
            data.ForEach(x => x.WithoutPassword());
            return data;
        }

        public override async Task<User> Update(User newModel)
        {
            if (string.IsNullOrWhiteSpace(newModel.Password))
            {
                var student = await _dbSet.FindAsync(newModel.Id);
                if (student == null)
                    return null;
                newModel.Password = student.Password;
            }
            else
            {
                newModel.Password = Encryptor.Encrypt(newModel.Password);
            }

            var data = await base.Update(newModel);
            return data;
        }

        public async Task<User> Authenticate(string email, string password)
        {
            var pass = Encryptor.Encrypt(password);
            var user = await _dbSet.Where(x => x.Email == email && x.Password == pass)
                    .Include(x => x.Student).FirstOrDefaultAsync();

            var student = await _db.Students.FirstOrDefaultAsync(x => x.UserId == user.Id);

            if (user == null)
                return null;

            var role = student == null ? ClaimType.Professor : ClaimType.Student;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim("StudentId", student.Id.ToString()),
                    new Claim(ClaimTypes.Role, role.ToString())
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