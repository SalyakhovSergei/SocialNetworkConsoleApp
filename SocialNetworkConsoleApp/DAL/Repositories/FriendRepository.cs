using System.Collections.Generic;
using SocialNetworkConsoleApp.DAL.Entities;

namespace SocialNetworkConsoleApp.DAL.Repositories
{
    public class FriendRepository: BaseRepository, IFriendRepository
    {
        public int Create(FriendEntity friendEntity)
        {
            return Execute(@"insert into Friends (user_id, friend_id) values (:user_id, friend_id)", friendEntity);
        }

        public IEnumerable<FriendEntity> FindAllByUseId(int userid)
        {
            return Query<FriendEntity>(@"select * from Friends where user_id = :usr_id", new {user_id = userid});
        }

        public int Delete(int id)
        {
            return Execute(@"delete from Friends where id =:id_p", new {id_p = id});
        }
    }
}