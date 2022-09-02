using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace dbaccess.Models
{
    public partial class ASQContext : DbContext
    {
        public ASQContext()
        {
        }

        public ASQContext(DbContextOptions<ASQContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbAccountCreationTracking> TbAccountCreationTrackings { get; set; }
        public virtual DbSet<TbAccountType> TbAccountTypes { get; set; }
        public virtual DbSet<TbAccountUpdateTracking> TbAccountUpdateTrackings { get; set; }
        public virtual DbSet<TbBlogPost> TbBlogPosts { get; set; }
        public virtual DbSet<TbCardType> TbCardTypes { get; set; }
        public virtual DbSet<TbExtZoomMeeting> TbExtZoomMeetings { get; set; }
        public virtual DbSet<TbExtZoomMeetingRecording> TbExtZoomMeetingRecordings { get; set; }
        public virtual DbSet<TbExtZoomUser> TbExtZoomUsers { get; set; }
        public virtual DbSet<TbExtZoomWebHook> TbExtZoomWebHooks { get; set; }
        public virtual DbSet<TbFocus> TbFoci { get; set; }
        public virtual DbSet<TbFocusBlogPostMapping> TbFocusBlogPostMappings { get; set; }
        public virtual DbSet<TbFocusHostMapping> TbFocusHostMappings { get; set; }
        public virtual DbSet<TbFocusMeetingMapping> TbFocusMeetingMappings { get; set; }
        public virtual DbSet<TbFocusUserMapping> TbFocusUserMappings { get; set; }
        public virtual DbSet<TbFocusVideoMapping> TbFocusVideoMappings { get; set; }
        public virtual DbSet<TbHost> TbHosts { get; set; }
        public virtual DbSet<TbImg> TbImgs { get; set; }
        public virtual DbSet<TbMail> TbMails { get; set; }
        public virtual DbSet<TbMeeting> TbMeetings { get; set; }
        public virtual DbSet<TbMeetingCreationTracking> TbMeetingCreationTrackings { get; set; }
        public virtual DbSet<TbMeetingRecording> TbMeetingRecordings { get; set; }
        public virtual DbSet<TbMeetingRecordingDownloadTracking> TbMeetingRecordingDownloadTrackings { get; set; }
        public virtual DbSet<TbMeetingReview> TbMeetingReviews { get; set; }
        public virtual DbSet<TbMeetingStatus> TbMeetingStatuses { get; set; }
        public virtual DbSet<TbMeetingUpdateTracking> TbMeetingUpdateTrackings { get; set; }
        public virtual DbSet<TbMeetingUserMapping> TbMeetingUserMappings { get; set; }
        public virtual DbSet<TbNotification> TbNotifications { get; set; }
        public virtual DbSet<TbNotificationTracking> TbNotificationTrackings { get; set; }
        public virtual DbSet<TbNotificationType> TbNotificationTypes { get; set; }
        public virtual DbSet<TbPaymentInfo> TbPaymentInfos { get; set; }
        public virtual DbSet<TbTimezone> TbTimezones { get; set; }
        public virtual DbSet<TbUser> TbUsers { get; set; }
        public virtual DbSet<TbVid> TbVids { get; set; }
        public virtual DbSet<TbVideo> TbVideos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) { }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbAccountCreationTracking>(entity =>
            {
                entity.ToTable("tb_AccountCreationTracking");

                entity.Property(e => e.Failed).HasColumnType("bit(1)");

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.Request)
                    .IsRequired()
                    .HasColumnType("json");

                entity.Property(e => e.Tracking)
                    .IsRequired()
                    .HasColumnType("json");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TbAccountType>(entity =>
            {
                entity.ToTable("tb_AccountType");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TbAccountUpdateTracking>(entity =>
            {
                entity.ToTable("tb_AccountUpdateTracking");

                entity.Property(e => e.Failed).HasColumnType("bit(1)");

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.Request)
                    .IsRequired()
                    .HasColumnType("json");

                entity.Property(e => e.Tracking)
                    .IsRequired()
                    .HasColumnType("json");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TbBlogPost>(entity =>
            {
                entity.ToTable("tb_BlogPost");

                entity.HasIndex(e => e.CreationUserId, "CreationUserId");

                entity.HasIndex(e => e.ImgId, "ImgId");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("mediumtext");

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);

                entity.HasOne(d => d.CreationUser)
                    .WithMany(p => p.TbBlogPosts)
                    .HasForeignKey(d => d.CreationUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_BlogPost_ibfk_1");

                entity.HasOne(d => d.Img)
                    .WithMany(p => p.TbBlogPosts)
                    .HasForeignKey(d => d.ImgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_BlogPost_ibfk_2");
            });

            modelBuilder.Entity<TbCardType>(entity =>
            {
                entity.ToTable("tb_CardType");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TbExtZoomMeeting>(entity =>
            {
                entity.ToTable("tb_ext_ZoomMeeting");

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.Payload)
                    .IsRequired()
                    .HasColumnType("json");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TbExtZoomMeetingRecording>(entity =>
            {
                entity.ToTable("tb_ext_ZoomMeetingRecording");

                entity.HasIndex(e => e.ExtMeetingId, "ExtMeetingId");

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.Payload)
                    .IsRequired()
                    .HasColumnType("json");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);

                entity.HasOne(d => d.ExtMeeting)
                    .WithMany(p => p.TbExtZoomMeetingRecordings)
                    .HasForeignKey(d => d.ExtMeetingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_ext_ZoomMeetingRecording_ibfk_1");
            });

            modelBuilder.Entity<TbExtZoomUser>(entity =>
            {
                entity.ToTable("tb_ext_ZoomUser");

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.Payload)
                    .IsRequired()
                    .HasColumnType("json");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TbExtZoomWebHook>(entity =>
            {
                entity.ToTable("tb_ext_ZoomWebHook");

                entity.HasIndex(e => e.MeetingId, "MeetingId");

                entity.Property(e => e.EventType)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.Payload)
                    .IsRequired()
                    .HasColumnType("json");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Meeting)
                    .WithMany(p => p.TbExtZoomWebHooks)
                    .HasForeignKey(d => d.MeetingId)
                    .HasConstraintName("tb_ext_ZoomWebHook_ibfk_1");
            });

            modelBuilder.Entity<TbFocus>(entity =>
            {
                entity.ToTable("tb_Focus");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TbFocusBlogPostMapping>(entity =>
            {
                entity.ToTable("tb_FocusBlogPostMapping");

                entity.HasIndex(e => e.FocusId, "FocusId");

                entity.HasIndex(e => new { e.BlogPostId, e.FocusId }, "UC_BLOGPOST_FOCUS_MAPPING")
                    .IsUnique();

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);

                entity.HasOne(d => d.BlogPost)
                    .WithMany(p => p.TbFocusBlogPostMappings)
                    .HasForeignKey(d => d.BlogPostId)
                    .HasConstraintName("tb_FocusBlogPostMapping_ibfk_1");

                entity.HasOne(d => d.Focus)
                    .WithMany(p => p.TbFocusBlogPostMappings)
                    .HasForeignKey(d => d.FocusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_FocusBlogPostMapping_ibfk_2");
            });

            modelBuilder.Entity<TbFocusHostMapping>(entity =>
            {
                entity.ToTable("tb_FocusHostMapping");

                entity.HasIndex(e => e.FocusId, "FocusId");

                entity.HasIndex(e => new { e.HostId, e.FocusId }, "UC_HOST_FOCUS_MAPPING")
                    .IsUnique();

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Focus)
                    .WithMany(p => p.TbFocusHostMappings)
                    .HasForeignKey(d => d.FocusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_FocusHostMapping_ibfk_2");

                entity.HasOne(d => d.Host)
                    .WithMany(p => p.TbFocusHostMappings)
                    .HasForeignKey(d => d.HostId)
                    .HasConstraintName("tb_FocusHostMapping_ibfk_1");
            });

            modelBuilder.Entity<TbFocusMeetingMapping>(entity =>
            {
                entity.ToTable("tb_FocusMeetingMapping");

                entity.HasIndex(e => e.FocusId, "FocusId");

                entity.HasIndex(e => new { e.MeetingId, e.FocusId }, "UC_MEETING_FOCUS_MAPPING")
                    .IsUnique();

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Focus)
                    .WithMany(p => p.TbFocusMeetingMappings)
                    .HasForeignKey(d => d.FocusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_FocusMeetingMapping_ibfk_2");

                entity.HasOne(d => d.Meeting)
                    .WithMany(p => p.TbFocusMeetingMappings)
                    .HasForeignKey(d => d.MeetingId)
                    .HasConstraintName("tb_FocusMeetingMapping_ibfk_1");
            });

            modelBuilder.Entity<TbFocusUserMapping>(entity =>
            {
                entity.ToTable("tb_FocusUserMapping");

                entity.HasIndex(e => e.FocusId, "FocusId");

                entity.HasIndex(e => new { e.UserId, e.FocusId }, "UC_USER_FOCUS_MAPPING")
                    .IsUnique();

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Focus)
                    .WithMany(p => p.TbFocusUserMappings)
                    .HasForeignKey(d => d.FocusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_FocusUserMapping_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TbFocusUserMappings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("tb_FocusUserMapping_ibfk_1");
            });

            modelBuilder.Entity<TbFocusVideoMapping>(entity =>
            {
                entity.ToTable("tb_FocusVideoMapping");

                entity.HasIndex(e => e.FocusId, "FocusId");

                entity.HasIndex(e => new { e.VideoId, e.FocusId }, "UC_VIDEO_FOCUS_MAPPING")
                    .IsUnique();

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Focus)
                    .WithMany(p => p.TbFocusVideoMappings)
                    .HasForeignKey(d => d.FocusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_FocusVideoMapping_ibfk_2");

                entity.HasOne(d => d.Video)
                    .WithMany(p => p.TbFocusVideoMappings)
                    .HasForeignKey(d => d.VideoId)
                    .HasConstraintName("tb_FocusVideoMapping_ibfk_1");
            });

            modelBuilder.Entity<TbHost>(entity =>
            {
                entity.ToTable("tb_Host");

                entity.HasIndex(e => e.ExtUserId, "ExtUserId");

                entity.Property(e => e.CareerSummary)
                    .IsRequired()
                    .HasMaxLength(1500);

                entity.Property(e => e.Company)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.IsConsultant)
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);

                entity.HasOne(d => d.ExtUser)
                    .WithMany(p => p.TbHosts)
                    .HasForeignKey(d => d.ExtUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_Host_ibfk_1");
            });

            modelBuilder.Entity<TbImg>(entity =>
            {
                entity.ToTable("tb_Img");

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.ThumbnailUrl)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<TbMail>(entity =>
            {
                entity.ToTable("tb_Mail");

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasMaxLength(2500);

                entity.Property(e => e.FromEmailAddress)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(900);

                entity.Property(e => e.ToEmailAddress)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TbMeeting>(entity =>
            {
                entity.ToTable("tb_Meeting");

                entity.HasIndex(e => e.CreationUserId, "CreationUserId");

                entity.HasIndex(e => e.ExtMeetingId, "ExtMeetingId");

                entity.HasIndex(e => e.HostId, "HostId");

                entity.HasIndex(e => e.ImgId, "ImgId");

                entity.HasIndex(e => e.MeetingStatusId, "MeetingStatusId");

                entity.HasIndex(e => e.TimezoneId, "TimezoneId");

                entity.HasIndex(e => e.Title, "Title")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);

                entity.HasOne(d => d.CreationUser)
                    .WithMany(p => p.TbMeetings)
                    .HasForeignKey(d => d.CreationUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_Meeting_ibfk_1");

                entity.HasOne(d => d.ExtMeeting)
                    .WithMany(p => p.TbMeetings)
                    .HasForeignKey(d => d.ExtMeetingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_Meeting_ibfk_5");

                entity.HasOne(d => d.Host)
                    .WithMany(p => p.TbMeetings)
                    .HasForeignKey(d => d.HostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_Meeting_ibfk_2");

                entity.HasOne(d => d.Img)
                    .WithMany(p => p.TbMeetings)
                    .HasForeignKey(d => d.ImgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_Meeting_ibfk_4");

                entity.HasOne(d => d.MeetingStatus)
                    .WithMany(p => p.TbMeetings)
                    .HasForeignKey(d => d.MeetingStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_Meeting_ibfk_3");

                entity.HasOne(d => d.Timezone)
                    .WithMany(p => p.TbMeetings)
                    .HasForeignKey(d => d.TimezoneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_Meeting_ibfk_6");
            });

            modelBuilder.Entity<TbMeetingCreationTracking>(entity =>
            {
                entity.ToTable("tb_MeetingCreationTracking");

                entity.Property(e => e.Failed).HasColumnType("bit(1)");

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.Request)
                    .IsRequired()
                    .HasColumnType("json");

                entity.Property(e => e.Tracking)
                    .IsRequired()
                    .HasColumnType("json");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TbMeetingRecording>(entity =>
            {
                entity.ToTable("tb_MeetingRecording");

                entity.HasIndex(e => e.ExtMeetingRecordingId, "ExtMeetingRecordingId");

                entity.HasIndex(e => e.MeetingId, "MeetingId");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);

                entity.HasOne(d => d.ExtMeetingRecording)
                    .WithMany(p => p.TbMeetingRecordings)
                    .HasForeignKey(d => d.ExtMeetingRecordingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_MeetingRecording_ibfk_2");

                entity.HasOne(d => d.Meeting)
                    .WithMany(p => p.TbMeetingRecordings)
                    .HasForeignKey(d => d.MeetingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_MeetingRecording_ibfk_1");
            });

            modelBuilder.Entity<TbMeetingRecordingDownloadTracking>(entity =>
            {
                entity.ToTable("tb_MeetingRecordingDownloadTracking");

                entity.Property(e => e.Failed).HasColumnType("bit(1)");

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.Request)
                    .IsRequired()
                    .HasColumnType("json");

                entity.Property(e => e.Tracking)
                    .IsRequired()
                    .HasColumnType("json");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TbMeetingReview>(entity =>
            {
                entity.ToTable("tb_MeetingReview");

                entity.HasIndex(e => e.MeetingUserMappingId, "MeetingUserMappingId");

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.Review)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);

                entity.HasOne(d => d.MeetingUserMapping)
                    .WithMany(p => p.TbMeetingReviews)
                    .HasForeignKey(d => d.MeetingUserMappingId)
                    .HasConstraintName("tb_MeetingReview_ibfk_1");
            });

            modelBuilder.Entity<TbMeetingStatus>(entity =>
            {
                entity.ToTable("tb_MeetingStatus");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TbMeetingUpdateTracking>(entity =>
            {
                entity.ToTable("tb_MeetingUpdateTracking");

                entity.Property(e => e.Failed).HasColumnType("bit(1)");

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.Request)
                    .IsRequired()
                    .HasColumnType("json");

                entity.Property(e => e.Tracking)
                    .IsRequired()
                    .HasColumnType("json");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TbMeetingUserMapping>(entity =>
            {
                entity.ToTable("tb_MeetingUserMapping");

                entity.HasIndex(e => e.MeetingId, "MeetingId");

                entity.HasIndex(e => new { e.UserId, e.MeetingId }, "UC_USER_MEETING_MAPPING")
                    .IsUnique();

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Meeting)
                    .WithMany(p => p.TbMeetingUserMappings)
                    .HasForeignKey(d => d.MeetingId)
                    .HasConstraintName("tb_MeetingUserMapping_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TbMeetingUserMappings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("tb_MeetingUserMapping_ibfk_1");
            });

            modelBuilder.Entity<TbNotification>(entity =>
            {
                entity.ToTable("tb_Notification");

                entity.HasIndex(e => e.NotificationTypeId, "NotificationTypeId");

                entity.HasIndex(e => e.UserId, "UserId");

                entity.Property(e => e.ExtMeetingUrl).HasMaxLength(650);

                entity.Property(e => e.ImgUrl).HasMaxLength(150);

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.MeetingUrl).HasMaxLength(150);

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Seen).HasColumnType("bit(1)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);

                entity.Property(e => e.VideoUrl).HasMaxLength(150);

                entity.HasOne(d => d.NotificationType)
                    .WithMany(p => p.TbNotifications)
                    .HasForeignKey(d => d.NotificationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_Notification_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TbNotifications)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_Notification_ibfk_1");
            });

            modelBuilder.Entity<TbNotificationTracking>(entity =>
            {
                entity.ToTable("tb_NotificationTracking");

                entity.Property(e => e.Failed).HasColumnType("bit(1)");

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.Request)
                    .IsRequired()
                    .HasColumnType("json");

                entity.Property(e => e.Tracking)
                    .IsRequired()
                    .HasColumnType("json");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TbNotificationType>(entity =>
            {
                entity.ToTable("tb_NotificationType");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TbPaymentInfo>(entity =>
            {
                entity.ToTable("tb_PaymentInfo");

                entity.HasIndex(e => e.CardNumber, "CardNumber")
                    .IsUnique();

                entity.HasIndex(e => e.CardTypeId, "CardTypeId");

                entity.Property(e => e.CardNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Cvc)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.ExpirationDate).HasColumnType("date");

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);

                entity.HasOne(d => d.CardType)
                    .WithMany(p => p.TbPaymentInfos)
                    .HasForeignKey(d => d.CardTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_PaymentInfo_ibfk_1");
            });

            modelBuilder.Entity<TbTimezone>(entity =>
            {
                entity.ToTable("tb_Timezone");

                entity.HasIndex(e => e.Display, "Display")
                    .IsUnique();

                entity.HasIndex(e => e.ExtCode, "ExtCode")
                    .IsUnique();

                entity.HasIndex(e => new { e.Display, e.ExtCode }, "UC_DISPLAY_EXCODE")
                    .IsUnique();

                entity.Property(e => e.Display)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ExtCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);

                entity.Property(e => e.UtcOffset).HasColumnType("tinyint");
            });

            modelBuilder.Entity<TbUser>(entity =>
            {
                entity.ToTable("tb_User");

                entity.HasIndex(e => e.AccountTypeId, "AccountTypeId");

                entity.HasIndex(e => e.Email, "Email")
                    .IsUnique();

                entity.HasIndex(e => e.HostId, "HostId");

                entity.HasIndex(e => e.ImgId, "ImgId");

                entity.HasIndex(e => e.PaymentInfoId, "PaymentInfoId");

                entity.HasIndex(e => e.TimezoneId, "TimezoneId");

                entity.HasIndex(e => e.Username, "Username")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.TbUsers)
                    .HasForeignKey(d => d.AccountTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_User_ibfk_1");

                entity.HasOne(d => d.Host)
                    .WithMany(p => p.TbUsers)
                    .HasForeignKey(d => d.HostId)
                    .HasConstraintName("tb_User_ibfk_3");

                entity.HasOne(d => d.Img)
                    .WithMany(p => p.TbUsers)
                    .HasForeignKey(d => d.ImgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_User_ibfk_2");

                entity.HasOne(d => d.PaymentInfo)
                    .WithMany(p => p.TbUsers)
                    .HasForeignKey(d => d.PaymentInfoId)
                    .HasConstraintName("tb_User_ibfk_5");

                entity.HasOne(d => d.Timezone)
                    .WithMany(p => p.TbUsers)
                    .HasForeignKey(d => d.TimezoneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_User_ibfk_4");
            });

            modelBuilder.Entity<TbVid>(entity =>
            {
                entity.ToTable("tb_Vid");

                entity.Property(e => e.FilePath)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<TbVideo>(entity =>
            {
                entity.ToTable("tb_Video");

                entity.HasIndex(e => e.CreationUserId, "CreationUserId");

                entity.HasIndex(e => e.ImgId, "ImgId");

                entity.HasIndex(e => e.Title, "Title")
                    .IsUnique();

                entity.HasIndex(e => e.VidId, "VidId");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Inactive).HasColumnType("bit(1)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UniqueId)
                    .IsRequired()
                    .HasMaxLength(38)
                    .IsFixedLength(true);

                entity.Property(e => e.VideoGroupId)
                    .HasMaxLength(38)
                    .IsFixedLength(true);

                entity.HasOne(d => d.CreationUser)
                    .WithMany(p => p.TbVideos)
                    .HasForeignKey(d => d.CreationUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_Video_ibfk_1");

                entity.HasOne(d => d.Img)
                    .WithMany(p => p.TbVideos)
                    .HasForeignKey(d => d.ImgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_Video_ibfk_3");

                entity.HasOne(d => d.Vid)
                    .WithMany(p => p.TbVideos)
                    .HasForeignKey(d => d.VidId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tb_Video_ibfk_2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
