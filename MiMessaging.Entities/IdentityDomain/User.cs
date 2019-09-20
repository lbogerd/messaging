using MiMessaging.Entities.MessagingDomain;
using System;
using System.Collections.Generic;

namespace MiMessaging.Entities.IdentityDomain
{
  public class User
  {
		public Guid Id { get; set; }
		public UserTypeEnum UserType { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public List<ConversationParticipant> Conversations { get; set; }
		public List<Message> SentMessages { get; set; }
	}
}