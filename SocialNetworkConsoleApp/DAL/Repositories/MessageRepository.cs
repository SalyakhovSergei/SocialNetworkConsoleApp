using System.Collections.Generic;
using SocialNetworkConsoleApp.DAL.Entities;

namespace SocialNetworkConsoleApp.DAL.Repositories
{
    public class MessageRepository: BaseRepository, IMessageRepository
    {
        public int Create(MessageEntity messageEntity)
        {
            return Execute(@"insert into messages(content, sender_id, recipient_id) 
                             values(:content,:sender_id,:recipient_id)", messageEntity);
        }

        public IEnumerable<MessageEntity> FindBySenderId(int senderid)
        {
            return Query<MessageEntity>("select * from Messages where serder_id = :sender_id",
                new {sender_id = senderid});
        }

        public IEnumerable<MessageEntity> FindByRecipientId(int recipientid)
        {
            return Query<MessageEntity>("select * from Messages where recepient_id = :recipientid",
                new {recepient_id = recipientid});
        }

        public int DeleteById(int messageid)
        {
            return Execute("delete from Messages where id=:id", new {id = messageid});
        }
    }
}