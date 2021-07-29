using System;
using SocialNetworkConsoleApp.BLL.Exceptions;
using SocialNetworkConsoleApp.BLL.Models;
using SocialNetworkConsoleApp.BLL.Services;

namespace SocialNetworkConsoleApp.PL.Views
{
    public class FriendsInfoView
    {
        FriendServices friendServices;
       public FriendsInfoView(FriendServices friendServices)
        {
            this.friendServices = friendServices;
        }

        public void ShowFriends(User user)
        { 
            try
            {
            var friend = new Friend();
            Console.WriteLine("Введите почтовый адрес друга для добавления его в друзья: ");
            friend.FriendEmail = Console.ReadLine();
            friend.UserId = user.Id;

            friendServices.AddFriend(friend);
            SuccessMessage.Show("Поздравляем, у Вас новый друг");
               
            }
            catch (UserNotFoundException)
            {
                AlertMessage.Show("Пользователь не найден");
            }
            catch (ArgumentNullException)
            {
                AlertMessage.Show("Введите корректное значение!");
            }

            catch (Exception)
            {
                AlertMessage.Show("Произошла ошибка при проведении операции!");
            }

        }
    }
}