using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SocialNetworkConsoleApp.BLL.Exceptions;
using SocialNetworkConsoleApp.BLL.Models;
using SocialNetworkConsoleApp.DAL.Entities;
using SocialNetworkConsoleApp.DAL.Repositories;

namespace SocialNetworkConsoleApp.BLL.Services
{
    public class FriendServices
    {
        IFriendRepository friendRepository;
        IUserRepository userRepository;
       
        public FriendServices()
        {
            friendRepository = new FriendRepository();
            userRepository = new UserRepository();
        }
       
       public void AddFriend(Friend friend)
        {
            var findFriend = userRepository.FindByEmail(friend.FriendEmail);
            if (findFriend is null) throw new UserNotFoundException();

            var friendEntity = new FriendEntity()
            {
                user_id = friend.UserId,
                friend_id = findFriend.id
            };

            if (this.friendRepository.Create(friendEntity) == 0)
                throw new Exception();
        }

    }
}