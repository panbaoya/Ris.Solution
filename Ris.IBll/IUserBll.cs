using Ris.Dal;
using Ris.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ris.IBll
{
    public interface IUserBll
    {
        UserModel Login(UserModel model);
        bool AddUser(UserModel model);
        List<UserModel> GetUsers();
    }
}
