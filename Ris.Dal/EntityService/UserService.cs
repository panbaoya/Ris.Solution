using Ris.Dal.Entitys;
using Ris.Models.User;
using Ris.Tools;
using System;
using System.Linq.Expressions;

namespace Ris.Dal.EntityService
{
    public class UserService: SugarRepository<tb_User>
    {
        public tb_User Login(UserModel model)
        {
            Expression<Func<tb_User, bool>> expression = x => 1 == 1;
            if (!string.IsNullOrEmpty(model.UserName))
            {
                expression = expression.And(x => x.UserName == model.UserName);
            }
            if (!string.IsNullOrEmpty(model.Password))
            {
                expression = expression.And(x => x.Password == model.Password);
            }
            if (!string.IsNullOrEmpty(model.Phone))
            {
                expression = expression.And(x => x.Phone == model.Phone);
            }
            if (model.Status.HasValue)
            {
                expression = expression.And(x => x.Status == model.Status);
            }
            return GetSingle(expression);
        }

        public tb_User IsExists(UserModel model)
        {
            Expression<Func<tb_User, bool>> expression=x=>x.Phone==model.Phone || x.UserName==model.UserName;
            return GetSingle(expression);
        }
    }
}
