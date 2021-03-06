﻿// <auto-generated />
using System;
using MiMessaging.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MiMessaging.Api.Migrations
{
    [DbContext(typeof(MiMessagingContext))]
    [Migration("20190109140152_FixedLinkBetweenUserAndConversations")]
    partial class FixedLinkBetweenUserAndConversations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-preview3-35497")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MiMessaging.Entities.IdentityDomain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int>("UserType");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MiMessaging.Entities.MessagingDomain.Attachment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AddedById");

                    b.Property<DateTime>("AddedOn");

                    b.Property<string>("Description");

                    b.Property<Guid?>("MessageId");

                    b.HasKey("Id");

                    b.HasIndex("AddedById");

                    b.HasIndex("MessageId");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("MiMessaging.Entities.MessagingDomain.Conversation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("InternalReference");

                    b.Property<DateTime>("StartedOn");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Conversations");
                });

            modelBuilder.Entity("MiMessaging.Entities.MessagingDomain.ConversationParticipant", b =>
                {
                    b.Property<Guid>("ConversationId");

                    b.Property<Guid>("ParticipantId");

                    b.HasKey("ConversationId", "ParticipantId");

                    b.HasIndex("ParticipantId");

                    b.ToTable("ConversationParticipants");
                });

            modelBuilder.Entity("MiMessaging.Entities.MessagingDomain.ConversationStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ConversationId");

                    b.Property<int>("ConversationStatusEnum");

                    b.Property<DateTime>("SetOn");

                    b.HasKey("Id");

                    b.HasIndex("ConversationId");

                    b.ToTable("ConversationStatuses");
                });

            modelBuilder.Entity("MiMessaging.Entities.MessagingDomain.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<Guid>("ConversationId");

                    b.Property<Guid>("SentById");

                    b.Property<DateTime>("SentOn");

                    b.HasKey("Id");

                    b.HasIndex("ConversationId");

                    b.HasIndex("SentById");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("MiMessaging.Entities.MessagingDomain.Attachment", b =>
                {
                    b.HasOne("MiMessaging.Entities.IdentityDomain.User", "AddedBy")
                        .WithMany()
                        .HasForeignKey("AddedById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MiMessaging.Entities.MessagingDomain.Message")
                        .WithMany("Attachments")
                        .HasForeignKey("MessageId");
                });

            modelBuilder.Entity("MiMessaging.Entities.MessagingDomain.ConversationParticipant", b =>
                {
                    b.HasOne("MiMessaging.Entities.MessagingDomain.Conversation", "Conversation")
                        .WithMany("Participants")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MiMessaging.Entities.IdentityDomain.User", "Participant")
                        .WithMany("Conversations")
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MiMessaging.Entities.MessagingDomain.ConversationStatus", b =>
                {
                    b.HasOne("MiMessaging.Entities.MessagingDomain.Conversation", "Conversation")
                        .WithMany("ConversationStatusHistory")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MiMessaging.Entities.MessagingDomain.Message", b =>
                {
                    b.HasOne("MiMessaging.Entities.MessagingDomain.Conversation", "Conversation")
                        .WithMany("Messages")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MiMessaging.Entities.IdentityDomain.User", "SentBy")
                        .WithMany("SentMessages")
                        .HasForeignKey("SentById")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
