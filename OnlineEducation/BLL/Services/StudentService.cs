using System;
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
    public class StudentService : ServiceBase<Student>, IStudentService
    {
        private readonly AppSettings _appSettings;

        public StudentService(OnlineEducationDbContext dbContext,
            IOptions<AppSettings> appSettings) : base(dbContext)
        {
            this._appSettings = appSettings.Value;
        }

        public override async Task<Student> Add(Student newModel)
        {
            newModel.Password = Encryptor.Encrypt(newModel.Password);

            return await base.Add(newModel);
        }

        public override async Task<Student> Get(int id)
        {
            var data = await base.Get(id);
            return data.WithoutPassword();
        }

        public override async Task<FilterResult<Student>> Get(Expression<Func<Student, bool>> expression, int start, int count)
        {
            var data = await base.Get(expression, start, count);

            foreach (var item in data.Data)
            {
                item.WithoutPassword();
            }

            return data;
        }

        public override async Task<List<Student>> GetAll()
        {
            var data = await base.GetAll();
            data.ForEach(x => x.WithoutPassword());
            return data;
        }

        public override async Task<List<Student>> Get(Expression<Func<Student, bool>> expression)
        {
            var data = await base.Get(expression);
            data.ForEach(x => x.WithoutPassword());
            return data;
        }

        public override async Task<Student> Update(Student newModel)
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

        public async Task<Student> Authenticate(string email, string password)
        {
            var pass = Encryptor.Encrypt(password);

            var student = await _db.Students.FirstOrDefaultAsync(x => x.Email == email && x.Password == pass);

            if (student == null)
                return null;

            var role = student.GroupId == 1 ? ClaimType.Professor : ClaimType.Student;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, student.Email),
                    new Claim("StudentId", student.Id.ToString()),
                    new Claim(ClaimTypes.Role, role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            student.Token = tokenHandler.WriteToken(token);

            return student.WithoutPassword();
        }

        public async Task<List<Item>> GetLessans(int id)
        {
            var group = await _dbSet
                .Where(x => x.Id == id)
                .Select(v => v.Group)
                .FirstOrDefaultAsync();

            if (group == null)
                return null;

            var items = await _db.ItemGroups
                .Where(x => x.GroupId == group.Id)
                .Include(x => x.Item).ThenInclude(x => x.ItemsLessons)
                .Select(x => x.Item)
                .ToListAsync();

            return items;
        }
    }
}