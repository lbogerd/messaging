using Microsoft.EntityFrameworkCore;
using MiMessaging.Entities.IdentityDomain;
using MiMessaging.Entities.MessagingDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiMessaging.Api.Models
{
	public class MiMessagingContext
		: DbContext
	{
		public MiMessagingContext(DbContextOptions<MiMessagingContext> options)
			: base(options)
		{ }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ConversationParticipant>()
				.HasKey(c => new { c.ConversationId, c.ParticipantId });
			modelBuilder.Entity<MessageReadStatus>()
				.HasKey(c => new { c.ParticipantId, c.MessageId });
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Attachment> Attachments { get; set; }
		public DbSet<Conversation> Conversations { get; set; }
		public DbSet<ConversationParticipant> ConversationParticipants { get; set; }
		public DbSet<ConversationStatus> ConversationStatuses { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<MessageReadStatus> MessageReadStatuses { get; set; }
	}
}
