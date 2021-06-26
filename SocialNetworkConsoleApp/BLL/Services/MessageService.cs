using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetworkConsoleApp.BLL.Exceptions;
using SocialNetworkConsoleApp.BLL.Models;
using SocialNetworkConsoleApp.DAL.Entities;
using SocialNetworkConsoleApp.DAL.Repositories;

namespace SocialNetworkConsoleApp.BLL.Services
{
    public class MessageService
    {
        IMessageRepository messageRepository;
        IUserRepository userRepository;
        public MessageService()
        {
            userRepository = new UserRepository();
            messageRepository = new MessageRepository();
        }

        public IEnumerable<Message> GetIncomingMessagesByUserId(int recipientId)
        {
            var messages = new List<Message>();

            messageRepository.FindByRecipientId(recipientId).ToList().ForEach(m =>
            {
                var senderUserEntity = userRepository.FindById(m.SenderId);
                var recipientUserEntity = userRepository.FindById(m.RecipientId);

                messages.Add(new Message(m.Id, m.Content, senderUserEntity.EMail, recipientUserEntity.EMail));
            });

            return messages;
        }

        public IEnumerable<Message> GetOutcomingMessagesByUserId(int senderId)
        {
            var messages = new List<Message>();

            messageRepository.FindBySenderId(senderId).ToList().ForEach(m =>
            {
                var senderUserEntity = userRepository.FindById(m.SenderId);
                var recipientUserEntity = userRepository.FindById(m.RecipientId);

                messages.Add(new Message(m.Id, m.Content, senderUserEntity.EMail, recipientUserEntity.EMail));
            });

            return messages;
        }

        public void SendMessage(MessageSendingData messageSendingData)
        {
            if (String.IsNullOrEmpty(messageSendingData.Content))
                throw new ArgumentNullException();

            if (messageSendingData.Content.Length > 5000)
                throw new ArgumentOutOfRangeException();

            var findUserEntity = this.userRepository.FindByEmail(messageSendingData.RecipientEmail);
            if (findUserEntity is null) throw new UserNotFoundException();

            var messageEntity = new MessageEntity()
            {
                Content = messageSendingData.Content,
                SenderId = messageSendingData.SenderId,
                RecipientId = findUserEntity.Id
            };

            if (this.messageRepository.Create(messageEntity) == 0)
                throw new Exception();
        }
    }
}