using System;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using SocialNetworkConsoleApp.BLL.Models;
using SocialNetworkConsoleApp.BLL.Services;

namespace SocialNetworkConsoleApp
{
    class Program
    {
        public static UserService userService = new UserService();
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to our social network");
            while (true)
            {
                Console.WriteLine("Введите Имя пользователя для регистрации");

                string firstName = Console.ReadLine();

                Console.WriteLine("фамилия:");
                string lastName = Console.ReadLine();
            
                Console.WriteLine("пароль:");
                string password = Console.ReadLine();
            
                Console.WriteLine("почта:");
                string email = Console.ReadLine();

                UserRegistrationData userRegistrationData = new UserRegistrationData()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Password = password,
                    Email = email
                };
                try
                {
                    userService.Register(userRegistrationData);
                    Console.WriteLine("Регситрация прошла успешно");
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("ввести корректное значение");

                }
                catch (Exception)
                {
                    Console.WriteLine("Произошла ошибка");
                }
                Console.ReadLine();
            }
            
        }
    }
}
