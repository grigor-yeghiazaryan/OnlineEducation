using System;
using OnlineEducation.DAL;
using OnlineEducation.Common;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using OnlineEducation.DAL.Entities;
using OnlineEducation.BLL.Interfaces;

namespace OnlineEducation.BLL.Services
{
    public class StudentService : ServiceBase<Student>, IStudentService
    {
        public StudentService(OnlineEducationDbContext dbContext) : base(dbContext) { }

        public override async Task<Student> Add(Student newModel)
        {
            newModel.Password = Encrypt(newModel.Password);

            return await base.Add(newModel);
        }

        public override async Task<Student> Get(int id)
        {
            var data = await base.Get(id);
            data.Password = null;
            return data;
        }

        public override async Task<FilterResult<Student>> Get(Expression<Func<Student, bool>> expression, int start, int count)
        {
            var data = await base.Get(expression, start, count);

            foreach (var item in data.Data)
            {
                item.Password = null;
            }

            return data;
        }

        public override async Task<List<Student>> GetAll()
        {
            var data = await base.GetAll();
            data.ForEach(x => x.Password = null);
            return data;
        }

        public virtual async Task<List<Student>> Get(Expression<Func<Student, bool>> expression)
        {
            var data = await base.Get(expression);
            data.ForEach(x => x.Password = null);
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
                newModel.Password = Encrypt(newModel.Password);
            }

            var data = await base.Update(newModel);
            return data;
        }

        private string Encrypt(string inputString)
        {
            if (string.IsNullOrWhiteSpace(inputString))
                return null;

            byte[] data = System.Text.Encoding.ASCII.GetBytes(inputString);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            var hash = System.Text.Encoding.ASCII.GetString(data);
            return hash;
        }
    }
}