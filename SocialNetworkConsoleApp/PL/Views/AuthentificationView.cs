﻿using System;
using SocialNetworkConsoleApp.BLL.Exceptions;
using SocialNetworkConsoleApp.BLL.Models;
using SocialNetworkConsoleApp.BLL.Services;

namespace SocialNetworkConsoleApp.PL.Views
{
    public class AuthenticationView
    {
        private readonly UserService _userService;
        public AuthenticationView(UserService userService)
        {
            this._userService = userService;
        }

        public void Show()
        {
            var authenticationData = new UserAuthenticationData();

            Console.WriteLine("Введите почтовый адрес:");
            authenticationData.Email = Console.ReadLine();

            Console.WriteLine("Введите пароль:");
            authenticationData.Password = Console.ReadLine();

            try
            {
                var user = this._userService.Authentificate(authenticationData);

                SuccessMessage.Show("Вы успешно вошли в социальную сеть!");
                SuccessMessage.Show("Добро пожаловать " + user.FirstName);

                UserMenuView.Show(user);
            }

            catch (WrongPasswordException)
            {
                AlertMessage.Show("Пароль не корректный!");
            }

            catch (UserNotFoundException)
            {
                AlertMessage.Show("Пользователь не найден!");
            }

        }
    } 
}