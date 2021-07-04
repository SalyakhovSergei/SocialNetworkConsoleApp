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
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        public MessageService()
        {
            _userRepository = new UserRepository();
            _messageRepository = new MessageRepository();
        }

        public IEnumerable<Message> GetIncomingMessagesByUserId(int recipientId)
        {
            var messages = new List<Message>();

            _messageRepository.FindByRecipientId(recipientId).ToList().ForEach(m =>
            {
                var senderUserEntity = _userRepository.FindById(m.sender_id);
                var recipientUserEntity = _userRepository.FindById(m.recipient_id);

                messages.Add(new Message(m.id, m.content, senderUserEntity.email, recipientUserEntity.email));
            });

            return messages;
        }

        public IEnumerable<Message> GetOutcomingMessagesByUserId(int senderId)
        {
            var messages = new List<Message>();

            _messageRepository.FindBySenderId(senderId).ToList().ForEach(m =>
            {
                var senderUserEntity = _userRepository.FindById(m.sender_id);
                var recipientUserEntity = _userRepository.FindById(m.recipient_id);

                messages.Add(new Message(m.id, m.content, senderUserEntity.email, recipientUserEntity.email));
            });

            return messages;
        }

        public void SendMessage(MessageSendingData messageSendingData)
        {
            if (String.IsNullOrEmpty(messageSendingData.Content))
                throw new ArgumentNullException();

            if (messageSendingData.Content.Length > 5000)
                throw new ArgumentOutOfRangeException();

            var findUserEntity = this._userRepository.FindByEmail(messageSendingData.RecipientEmail);
            if (findUserEntity is null) throw new UserNotFoundException();

            var messageEntity = new MessageEntity()
            {
                content = messageSendingData.Content,
                sender_id = messageSendingData.SenderId,
                recipient_id = findUserEntity.id
            };

            if (this._messageRepository.Create(messageEntity) == 0)
                throw new Exception();
        }
    }
}