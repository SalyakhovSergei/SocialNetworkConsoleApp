using System;
using System.ComponentModel.DataAnnotations;
using SocialNetworkConsoleApp.BLL.Models;
using SocialNetworkConsoleApp.DAL.Entities;
using SocialNetworkConsoleApp.DAL.Repositories;

namespace SocialNetworkConsoleApp.BLL.Services
{
    public class UserService
    {
        IUserRepository userRepository;
        public UserService()
        {
            userRepository = new UserRepository();
        }
        public void Register(UserRegistrationData userRegistrationData)
        {
            if (String.IsNullOrEmpty(userRegistrationData.FirstName))
                throw new ArgumentNullException();
            if (String.IsNullOrEmpty(userRegistrationData.LastName))
                throw new ArgumentNullException();
            if (String.IsNullOrEmpty(userRegistrationData.Email))
                throw new ArgumentNullException();
            if (String.IsNullOrEmpty(userRegistrationData.Password))
                throw new ArgumentNullException();
            if (userRegistrationData.Password.Length < 8)
                throw new ArgumentNullException();
            if (!new EmailAddressAttribute().IsValid(userRegistrationData.Email))
                throw new ArgumentNullException();
            if(userRepository.FindByEmail(userRegistrationData.Email) != null)
                throw new ArgumentNullException();

            var userEntity = new UserEntity()
            {
                FirstName = userRegistrationData.FirstName,
                LastName = userRegistrationData.LastName,
                Password = userRegistrationData.Password,
                EMail = userRegistrationData.Email
            };

            if (this.userRepository.Create(userEntity) == 0)
                throw new Exception();

        }
    }
}