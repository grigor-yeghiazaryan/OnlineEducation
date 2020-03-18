using System.Linq;
using System.Collections.Generic;
using OnlineEducation.DAL.Entities;

namespace OnlineEducation.Common
{
    public static class ExtensionMethods
    {
        public static IEnumerable<Student> WithoutPasswords(this IEnumerable<Student> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static Student WithoutPassword(this Student user)
        {
            user.Password = null;
            return user;
        }
    }
}
