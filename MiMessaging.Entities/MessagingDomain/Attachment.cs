using MiMessaging.Entities.IdentityDomain;
using System;

namespace MiMessaging.Entities.MessagingDomain
{
  public class Attachment
  {
		public Guid Id { get; set; }
		public string Description { get; set; }
		public DateTime AddedOn { get; set; }

		public Guid AddedById { get; set; }
		public User AddedBy { get; set; }
	}
}