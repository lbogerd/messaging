using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiMessaging.Api.Mappers;
using MiMessaging.Api.Models;
using MiMessaging.Entities.IdentityDomain;
using MiMessaging.Entities.MessagingDomain;

namespace MiMessaging.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ConversationsController 
		: ControllerBase
  {
    private readonly MiMessagingContext _context;
		private readonly IdentityDomainMapper _identityDomainMapper;
		private readonly MessagingDomainMapper _messagingDomainMapper;

		public ConversationsController(MiMessagingContext context)
    {
      _context = context;
			_identityDomainMapper = new IdentityDomainMapper();
			_messagingDomainMapper = new MessagingDomainMapper();
    }

		private void InitData()
		{
			if (_context.Users.Count() < 1)
			{
				var users = new List<User>()
				{
					new User
					{
						FirstName = "Handler",
						LastName = "User",
						UserType = UserTypeEnum.Handler
					},
					new User
					{
						FirstName = "Client",
						LastName = "User",
						UserType = UserTypeEnum.Customer
					}
				};
				_context.Users.AddRange(users);
				_context.SaveChanges();
			}

			//if (_context.Conversations.Count() > 0)
			//{
			//	var conversationToRemove = _context.Conversations.First();
			//	_context.Conversations.Remove(conversationToRemove);
			//	_context.SaveChanges();
			//}

			if (_context.Conversations.Count() < 1)
			{
				var anHourAgo = DateTime.UtcNow.AddHours(-1);
				var client = _context
					.Users
					.Where(p => p.FirstName == "Client")
					.First();
				var handler = _context
					.Users
					.Where(p => p.FirstName == "Handler")
					.First();

				var conversation = new Conversation
				{
					Title = "Example chat",
					InternalReference = "Salesforce ID",
					StartedOn = anHourAgo,
					Participants = new List<ConversationParticipant>
				{
					new ConversationParticipant
					{
						Participant = client
					},
					new ConversationParticipant
					{
						Participant = handler
					}
				},
					Messages = new List<Message>
				{
					new Message
					{
						Body = "I have a very difficult question, " +
							"please help!",
						SentBy = client,
						SentOn = anHourAgo
					},
					new Message
					{
						Body = "I'll ask one of our specialists.",
						SentBy = handler,
						SentOn = DateTime.UtcNow
					}
				},
					ConversationStatusHistory = new List<ConversationStatus>
				{
					new ConversationStatus
					{
						ConversationStatusEnum = ConversationStatusEnum.New,
						SetOn = anHourAgo
					},
					new ConversationStatus
					{
						ConversationStatusEnum = ConversationStatusEnum.InProgress,
						SetOn = DateTime.UtcNow
					}
				}
				};
				_context.Conversations.Add(conversation);
				_context.SaveChanges();

				conversation = _context.Conversations.First();
				var handlerParticipant = conversation
					.Participants
					.Where(p => p.ParticipantId == handler.Id)
					.Single();

				var clientParticipant = conversation
					.Participants
					.Where(p => p.ParticipantId == client.Id)
					.Single();

				conversation
					.Messages
					.OrderByDescending(p => p.SentOn)
					.First()
					.MessageReadStatuses = new List<MessageReadStatus>()
					{
					new MessageReadStatus
					{
						Participant = handlerParticipant,
						ReceivedOn = anHourAgo.AddMilliseconds(500),
						ReadOn = anHourAgo.AddMinutes(1)
					}
					};

				conversation
					.Messages
					.OrderByDescending(p => p.SentOn)
					.Last()
					.MessageReadStatuses = new List<MessageReadStatus>()
					{
					new MessageReadStatus
					{
						Participant = clientParticipant
					}
					};

				_context.Entry(conversation).State = EntityState.Modified;
				_context.SaveChanges();
			}
		}

    // GET: api/Conversations
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FullConversationDto>>> GetConversations()
    {
			var fullConversations = await _context
				.Conversations
				.Include(p => p.Messages)
					.ThenInclude(p => p.MessageReadStatuses)
				.Include(p => p.Participants)
					.ThenInclude(p => p.Participant)
				.Include(p => p.ConversationStatusHistory)
				.ToListAsync();

			var fullConversationDtos = new List<FullConversationDto>();

			foreach (var fullConversation in fullConversations)
			{
				fullConversationDtos
					.Add(_messagingDomainMapper
						.ToFullConversationDto(fullConversation));
			}

			return fullConversationDtos;
		}

    // GET: api/Conversations/5
    [HttpGet("{id}")]
    public async Task<ActionResult<FullConversationDto>> GetConversation(Guid id)
    {
			var conversation = await _context
				.Conversations
				.Where(p => p.Id == id)
				.Include(p => p.Messages)
					.ThenInclude(p => p.MessageReadStatuses)
				.Include(p => p.Participants)
					.ThenInclude(p => p.Participant)
				.Include(p => p.ConversationStatusHistory)
				.SingleAsync();

			if (conversation == null)
      {
        return NotFound();
      }

      return _messagingDomainMapper
				.ToFullConversationDto(conversation);
    }

    // PUT: api/Conversations/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutConversation(
			Guid id, 
			FullConversationDto conversationDto)
    {
      if (id != conversationDto.Id)
      {
        return BadRequest();
      }

			var conversation = _messagingDomainMapper
				.ToConversation(conversationDto);

      _context.Entry(conversation).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ConversationExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/Conversations
    [HttpPost]
    public ActionResult<FullConversationDto> PostConversation([FromBody] FullConversationDto conversationDto)
    {
			var conversation = _messagingDomainMapper
				.ToConversation(conversationDto);

      _context.Conversations.Add(conversation);
      _context.SaveChanges();

      return CreatedAtAction(
				"GetConversation", 
				new { id = conversationDto.Id }, 
				conversationDto);
    }

    // DELETE: api/Conversations/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Conversation>> DeleteConversation(Guid id)
    {
      var conversation = await _context.Conversations.FindAsync(id);
      if (conversation == null)
      {
          return NotFound();
      }

      _context.Conversations.Remove(conversation);
      await _context.SaveChangesAsync();

      return conversation;
    }

    private bool ConversationExists(Guid id)
    {
      return _context.Conversations.Any(e => e.Id == id);
    }
  }
}
