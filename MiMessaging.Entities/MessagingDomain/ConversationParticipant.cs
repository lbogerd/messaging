using MiMessaging.Entities.IdentityDomain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiMessaging.Entities.MessagingDomain
{
	public class ConversationParticipant
	{
		public Guid ParticipantId { get; set; }
		public User Participant { get; set; }
		public Guid ConversationId { get; set; }
		public Conversation Conversation { get; set; }
	}
}
