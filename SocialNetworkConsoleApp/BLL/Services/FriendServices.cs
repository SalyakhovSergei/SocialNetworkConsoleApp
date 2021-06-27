using System;
using SocialNetworkConsoleApp.BLL.Exceptions;
using SocialNetworkConsoleApp.BLL.Models;
using SocialNetworkConsoleApp.DAL.Entities;
using SocialNetworkConsoleApp.DAL.Repositories;

namespace SocialNetworkConsoleApp.BLL.Services
{
    public class FriendServices
    {
        IFriendRepository friendRepository;
        private IUserRepository userRepository;

        public FriendServices()
        {
            friendRepository = new FriendRepository();
            userRepository = new UserRepository();
        }

        public void AddFriend(Friend friend)
        {
            if (String.IsNullOrEmpty(friend.Email))
                throw new UserNotFoundException();

            var findFriend = this.userRepository.FindByEmail(friend.Email);
            if (findFriend is null) throw new UserNotFoundException();

            var friendEntity = new FriendEntity()
            {
                FriendId = friend.FriendId,
                UserId = friend.UserId
            };

            if (this.friendRepository.Create(friendEntity) == 0)
                throw new Exception();
        }
    }
}