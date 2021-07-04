using System;
using SocialNetworkConsoleApp.BLL.Models;
using SocialNetworkConsoleApp.BLL.Services;

namespace SocialNetworkConsoleApp.PL.Views
{
    public class UserDataUpdateView
    {
        private readonly UserService _userService;
        public UserDataUpdateView(UserService userService)
        {
            this._userService = userService;
        }

        public void Show(User user)
        {
            Console.Write("Меня зовут:");
            user.FirstName = Console.ReadLine();

            Console.Write("Моя фамилия:");
            user.LastName = Console.ReadLine();

            Console.Write("Ссылка на моё фото:");
            user.Photo = Console.ReadLine();

            Console.Write("Мой любимый фильм:");
            user.FavoriteMovie = Console.ReadLine();

            Console.Write("Моя любимая книга:");
            user.FavoriteBook = Console.ReadLine();

            this._userService.Update(user);

            SuccessMessage.Show("Ваш профиль успешно обновлён!");
        }
    }
}