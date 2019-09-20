using System;
using System.Collections.Generic;

namespace MiMessaging.Api.Models
{
	public class FullConversationDto
	{
		public Guid? Id { get; set; }
		public string Title { get; set; }
		public string SalesforceId { get; set; }
		public DateTime? StartedOn { get; set; }
		public string CurrentStatus { get; set; }
		public IEnumerable<Guid> Participants { get; set; }
		public IEnumerable<MessageDto> Messages { get; set; }
	}
}
