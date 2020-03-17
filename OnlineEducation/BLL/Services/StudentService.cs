using System;
using System.Linq;
using OnlineEducation.DAL;
using OnlineEducation.Common;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using OnlineEducation.DAL.Entities;
using OnlineEducation.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace OnlineEducation.BLL.Services
{
    public class StudentService : ServiceBase<StudentModel>, IStudentService
    {
        private readonly IUserService _userService;

        public StudentService(OnlineEducationDbContext dbContext, IUserService userService) : base(dbContext)
        {
            this._userService = userService;
        }

        public override async Task<StudentModel> Add(StudentModel newModel)
        {
            var user = newModel.GetUser();
            var newUser = await _userService.Add(user);

            newModel.UserId = newUser.Id;
            var newStudent = await base.Add(newModel);

            var studentModel = new StudentModel(newUser.WithoutPassword(), newStudent);
            return studentModel;
        }

        public override async Task<StudentModel> Get(int id)
        {
            var student = await _dbSet
               .Where(x=>x.Id == id)
               .Include(x => x.User)
               .SingleOrDefaultAsync();

            return new StudentModel(student.User.WithoutPassword(), student);
        }

        public override async Task<FilterResult<StudentModel>> Get(Expression<Func<StudentModel, bool>> expression, int start, int count)
        {
            var allCount = await _dbSet.CountAsync(expression);

            var query = _dbSet
                .Where(expression)
                .Include(x=>x.User)
                .OrderByDescending(x => x.Id)
                .Skip(start).Take(count);

            var entityList = await query.ToListAsync();

            var studentModelList = entityList.Select(x=> new StudentModel(x.User.WithoutPassword(), x));

            var filterResult = new FilterResult<StudentModel> { Data = studentModelList, ItemsCount = allCount };

            return filterResult;
        }

        public override async Task<List<StudentModel>> GetAll()
        {
            var query = _dbSet.Include(x => x.User);

            var entityList = await query.ToListAsync();

            var studentModelList = entityList.Select(x => new StudentModel(x.User.WithoutPassword(), x)).ToList();

            return studentModelList;
        }

        public override async Task<List<StudentModel>> Get(Expression<Func<StudentModel, bool>> expression)
        {
            var query = _dbSet
                .Where(expression)
                .Include(x => x.User);

            var entityList = await query.ToListAsync();

            var studentModelList = entityList.Select(x => new StudentModel(x.User.WithoutPassword(), x)).ToList();

            return studentModelList;
        }

        public override async Task<StudentModel> Update(StudentModel newModel)
        {
            var user = newModel.GetUser();
            var newUser = await _userService.Update(user);

            newModel.UserId = newUser.Id;
            var newStudent = await base.Update(newModel);

            var studentModel = new StudentModel(newUser, newStudent);
            return studentModel;
        }
    }
}