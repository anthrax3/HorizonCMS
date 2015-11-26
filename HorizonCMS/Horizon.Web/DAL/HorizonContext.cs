using Horizon.Web.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Horizon.Web.DAL
{
    public class HorizonContext : DbContext
    {
        public HorizonContext()
            : base("HorizonContext")
        {
        }

        public DbSet<Application> Application { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<Profile> UserProfile { get; set; }

        public DbSet<Template> Template { get; set; }

        public DbSet<Page> Page { get; set; }

        public DbSet<Keyword> Keyword { get; set; }

        public DbSet<LanguageCode> LanguageCode { get; set; }

        public DbSet<Language> Language { get; set; }

        public DbSet<Translation> Translation { get; set; }

        public DbSet<Blog> Blog { get; set; }

        public DbSet<Post> Post { get; set; }

        public DbSet<PostCategory> PostCategories { get; set; }

        public DbSet<PostType> PostTypes { get; set; }

        public DbSet<Comment> Comment { get; set; }

        public DbSet<Media> Media { get; set; }

        public DbSet<Section> Section { get; set; }

        public DbSet<Tag> Tag { get; set; }

        public DbSet<Theme> Theme { get; set; }

        public DbSet<Menu> Menu { get; set; }

        public DbSet<Notification> Notification { get; set; }

        public DbSet<NotificationStatus> NotificationStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            #region Applications

            modelBuilder.Entity<Application>()
                .HasOptional<Blog>(a => a.Blog)
                .WithRequired(b => b.Application);

            modelBuilder.Entity<Application>()
                .HasOptional<Theme>(a => a.Theme)
                .WithRequired(t => t.Application);

            #endregion

            #region Themes

            #endregion

            #region Setions

            #endregion

            #region Users - Roles mapping

            // Users - Roles mapping ( many to many )
            modelBuilder.Entity<User>()
                   .HasMany<Role>(s => s.Roles)
                   .WithMany(c => c.Users)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("UserRefId");
                       cs.MapRightKey("RoleRefId");
                       cs.ToTable("UserRole");
                   });




            #endregion

            #region Users - Notifications mapping

            modelBuilder.Entity<User>()
              .HasMany<Notification>(s => s.Notifications)
              .WithMany(n => n.Users)
              .Map(un =>
              {
                  un.MapLeftKey("UserRefId");
                  un.MapRightKey("NotificationRefId");
                  un.ToTable("UsersNotifications");
              });
              

            

            #endregion

            #region Notification - Notification Status

            modelBuilder.Entity<Notification>()
                .HasRequired<NotificationStatus>(n => n.Status)
                .WithMany(ns => ns.Notification);

            #endregion

            #region Sections Roles mapping

            modelBuilder.Entity<Section>()
                .HasMany<Role>(s => s.Roles)
                .WithMany(r => r.Sections)
                .Map(sr =>
            {
                sr.MapLeftKey("SectionRefId");
                sr.MapRightKey("RoleRefId");
                sr.ToTable("SectionRole");
            });

          


            #endregion

            #region Languages Mapping

            // Language - Codes mapping (1 to many)
            modelBuilder.Entity<Language>()
                .HasRequired<LanguageCode>(lc => lc.LanguageCode)
                .WithMany(lc => lc.Languages)
                .HasForeignKey(l => l.LngCdId);

            // Language - Page mapping ( 1 to many )
            modelBuilder.Entity<Page>()
                .HasRequired<Language>(p => p.Language)
                .WithMany(l => l.Pages)
                .HasForeignKey(p => p.LngId);

            // Translation - Language mapping (many to 1)
            modelBuilder.Entity<Translation>()
                .HasRequired<Language>(t => t.Language)
                .WithMany(l => l.Translations)
                .HasForeignKey(p => p.LngId);

            #endregion

            #region Keywords mapping

            modelBuilder.Entity<Page>()
                .HasMany<Keyword>(p => p.Keywords)
                .WithMany(k => k.Pages)
                .Map(pk =>
                {
                    pk.MapLeftKey("PageRefId");
                    pk.MapRightKey("KeywordRefId");
                    pk.ToTable("PageKeywords");
                });
            #endregion

            #region Blog - Post mapping

            modelBuilder.Entity<Blog>()
                 .HasMany<Post>(p => p.Posts)
                 .WithRequired(c => c.Blog)
                 .HasForeignKey(p => p.BlgId);


            modelBuilder.Entity<Post>()
                .HasMany<Comment>(p => p.Comments)
                .WithRequired(c => c.Post)
                .HasForeignKey(c => c.PstId);

            modelBuilder.Entity<Post>()
                .HasRequired<PostCategory>(p => p.Category)
                .WithMany(pc => pc.Posts)
                .HasForeignKey(pc => pc.CatId);

            modelBuilder.Entity<Post>()
                .HasRequired<PostType>(p => p.PostType)
                .WithMany(pt => pt.Posts)
                .HasForeignKey(pt => pt.PstId);

            modelBuilder.Entity<Post>()
                .HasMany<Media>(p => p.Images);

            modelBuilder.Entity<Post>()
                .HasRequired<User>(p => p.Author);

            modelBuilder.Entity<Post>()
                .HasMany<Tag>(p => p.Tags)
                .WithMany(t => t.Posts)
                .Map(pt =>
                {
                    pt.MapLeftKey("PostRefId");
                    pt.MapRightKey("TagRefId");
                    pt.ToTable("PostTags");
                });


            #endregion

            #region Pages Mappings

            // Page - templates mapping ( many to 1)
            modelBuilder.Entity<Page>()
                     .HasRequired<Template>(p => p.Template)
                     .WithMany(t => t.Pages)
                     .HasForeignKey(p => p.TplId);

            modelBuilder.Entity<Page>()
                .HasMany<Page>(p => p.Childs)
                .WithOptional(p => p.Parent);



            #endregion

            #region Templates & Menus

            modelBuilder.Entity<Template>()
                .HasOptional<Menu>(t => t.Menu)
                .WithRequired(m => m.Template);

            modelBuilder.Entity<Menu>()
                .HasRequired<Language>(m => m.Language);

            #endregion

        }


    }
}