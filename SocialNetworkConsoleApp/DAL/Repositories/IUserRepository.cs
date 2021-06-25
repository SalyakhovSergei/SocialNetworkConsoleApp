using System.Collections;
using System.Collections.Generic;
using SocialNetworkConsoleApp.DAL.Entities;

namespace SocialNetworkConsoleApp.DAL.Repositories
{
    public interface IUserRepository
    {
         int Create (UserEntity userEntity);
         UserEntity FindByEmail(string email);
         IEnumerable<UserEntity> FindAll();
         UserEntity FindById(int id);
         int Update(UserEntity userEntity);
         int DeleteById(int id);
    }
}