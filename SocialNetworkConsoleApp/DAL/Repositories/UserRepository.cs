using System.Collections.Generic;
using SocialNetworkConsoleApp.DAL.Entities;

namespace SocialNetworkConsoleApp.DAL.Repositories
{
    public class UserRepository: BaseRepository, IUserRepository
    {
        public int Create(UserEntity userEntity)
        {
            return Execute(@"insert into users (FirstName,LastName,Password,EMail) 
                             values (:firstname,:lastname,:password,:email)", userEntity);
        }

        public UserEntity FindByEmail(string email)
        {
            return QueryFirstOrDefault<UserEntity>("select * from Users where email = :email_p", new {email_p = email});
        }

        public IEnumerable<UserEntity> FindAll()
        {
            return Query<UserEntity>(@"select * from Users");
        }

        public UserEntity FindById(int id)
        {
            return QueryFirstOrDefault<UserEntity>("select * from Users where Id = :id_p", new {id_p = id});
        }

        public int Update(UserEntity userEntity)
        {
            return Execute(@"update users set firstname = :firstname, lastname = :lastname, password = :password, email = :email,
                             photo = :photo, favorite_movie = :favorite_movie, favorite_book = :favorite_book", userEntity);
        }

        public int DeleteById(int id)
        {
            return Execute("delete from Users where id =:id_p", new {id_p = id});
        }
    }
}