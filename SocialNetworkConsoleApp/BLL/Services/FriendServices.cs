using System;
using SocialNetworkConsoleApp.BLL.Exceptions;
using SocialNetworkConsoleApp.BLL.Models;
using SocialNetworkConsoleApp.DAL.Entities;
using SocialNetworkConsoleApp.DAL.Repositories;

namespace SocialNetworkConsoleApp.BLL.Services
{
    public class FriendServices
    {
        private readonly IFriendRepository _friendRepository;
        private readonly IUserRepository _userRepository;
       
        public FriendServices()
        {
            _friendRepository = new FriendRepository();
            _userRepository = new UserRepository();
        }
       
       public void AddFriend(Friend friend)
        {
            var findFriend = _userRepository.FindByEmail(friend.FriendEmail);
            if (findFriend is null) throw new UserNotFoundException();

            var friendEntity = new FriendEntity()
            {
                user_id = friend.UserId,
                friend_id = findFriend.id
            };

            if (this._friendRepository.Create(friendEntity) == 0)
                throw new Exception();
        }

    }
}