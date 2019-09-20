using System;
using System.Collections.Generic;
using System.Text;

namespace MiMessaging.Entities.MessagingDomain
{
	public class MessageReadStatus
	{
		public Guid ParticipantId { get; set; }
		public ConversationParticipant Participant { get; set; }
		public Guid MessageId { get; set; }
		public Message Message { get; set; }
		public DateTime ReceivedOn { get; set; }
		public DateTime ReadOn { get; set; }
	}
}
