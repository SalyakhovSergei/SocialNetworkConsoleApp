using System.Collections;
using System.Collections.Generic;
using SocialNetworkConsoleApp.DAL.Entities;

namespace SocialNetworkConsoleApp.DAL.Repositories
{
    public interface IMessageRepository
    {
        int Create(MessageEntity messageEntity);
        IEnumerable<MessageEntity> FindBySenderId(int senderid);
        IEnumerable<MessageEntity> FindByRecipientId(int recipientid);
        int DeleteById(int messageid);
    }
}