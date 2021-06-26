using System;
using System.ComponentModel.DataAnnotations;
using SocialNetworkConsoleApp.BLL.Exceptions;
using SocialNetworkConsoleApp.BLL.Models;
using SocialNetworkConsoleApp.DAL.Entities;
using SocialNetworkConsoleApp.DAL.Repositories;

namespace SocialNetworkConsoleApp.BLL.Services
{
    public class UserService
    {
        MessageService messageService;
        IUserRepository userRepository;
        public UserService()
        {
            userRepository = new UserRepository();
            messageService = new MessageService();
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

        public User Authentificate(UserAuthenticationData userAuthenticationData)
        {
            var findUserEntity = userRepository.FindByEmail(userAuthenticationData.Email);
            if (findUserEntity is null) 
                throw new UserNotFoundException();

            if (findUserEntity.Password != userAuthenticationData.Password)
                throw new WrongPasswordException();

            return ConstructUserModel(findUserEntity);
        }

        public User FindByEmail(string email)
        {
            var findUserEntity = userRepository.FindByEmail(email);
            if (findUserEntity is null)
                throw new UserNotFoundException();
            
            return ConstructUserModel(findUserEntity);
        }
        public User FindById(int id)
        {
            var findUserEntity = userRepository.FindById(id);
            if (findUserEntity is null) throw new UserNotFoundException();

            return ConstructUserModel(findUserEntity);
        }

        public void Update(User user)
        {
            var updatatableUserEntity = new UserEntity()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                EMail = user.Email,
                Photo = user.Photo,
                FavoriteMovie = user.FavoriteMovie,
                FavoriteBook = user.FavoriteBook

            };
            if (this.userRepository.Update(updatatableUserEntity) == 0)
                throw new Exception();
        }
        
        private User ConstructUserModel(UserEntity userEntity)
        {
            return new User
                (userEntity.Id,
                userEntity.FirstName,
                userEntity.LastName,
                userEntity.Password,
                userEntity.EMail,
                userEntity.Photo,
                userEntity.FavoriteMovie,
                userEntity.FavoriteBook);
        }
    }
}