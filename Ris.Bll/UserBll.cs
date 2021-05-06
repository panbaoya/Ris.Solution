using Nursing.Tools;
using Ris.Dal.Entitys;
using Ris.Dal.EntityService;
using Ris.IBll;
using Ris.Models.User;
using Ris.Tools;
using System.Collections.Generic;

namespace Ris.Bll
{
    public class UserBll : IUserBll
    {
        private UserService _userService;
        public UserBll()
        {
            _userService = new UserService();
        }

        public bool AddUser(UserModel model)
        {
            model.Password = AesUnit.AESEncrypt(model.Password, AppConfSetting.AesKey);
            model.Phone = AesUnit.AESEncrypt(model.Phone, AppConfSetting.AesKey);
            var user = _userService.IsExists(model);
            if (user!=null)
            {
                //已存在.
                return false;
            }
            var entity = model.MapTo<tb_User>();
            entity.CreateDate = _userService.GetDateTime();
            return _userService.Insert(entity) > 0;
        }

        public List<UserModel> GetUsers()
        {
            var users = _userService.GetList();
            var models= users.MapListTo<tb_User,UserModel>();
            models.ForEach(x =>
            {
                x.Phone = AesUnit.AESDecrypt(x.Phone, AppConfSetting.AesKey);
                x.Password = AesUnit.AESDecrypt(x.Password, AppConfSetting.AesKey);
            });
            return models;
        }

        //[Log]
        public UserModel Login(UserModel model)
        {
            if (!string.IsNullOrEmpty(model.Phone))
            {
                model.Phone = AesUnit.AESEncrypt(model.Phone, AppConfSetting.AesKey);
            }
            if (!string.IsNullOrEmpty(model.Password))
            {
                model.Password = AesUnit.AESEncrypt(model.Password, AppConfSetting.AesKey);
            }
            var entity= _userService.Login(model);
            return entity.MapTo<UserModel>();
        }
    }
}
