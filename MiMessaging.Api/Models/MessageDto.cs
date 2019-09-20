using System;

namespace MiMessaging.Api.Models
{
	public class MessageDto
	{
		public Guid? Id { get; set; }
		public string Body { get; set; }
		public DateTime? SentOn { get; set; }
		public Guid SentBy { get; set; }
	}
}