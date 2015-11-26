using Horizon.Web.Models;
using System;
using System.Collections.Generic;

namespace Horizon.Web.DAL
{
    public class HorizonInitializer : System.Data.Entity.DropCreateDatabaseAlways<HorizonContext> //.DropCreateDatabaseIfModelChanges<HorizonContext>
    {
        protected override void Seed(HorizonContext context)
        {
            
            #region Applications

            var applications = new List<Application>
            {
               new Application{Id = Guid.NewGuid(), Blog = null, Name="Eduardo Macho application", Theme = null   }
            };

            applications.ForEach(a => context.Application.Add(a));
            context.SaveChanges();

            #endregion

            #region Themes

            var themes = new List<Theme>
            {
                new Theme{Id = Guid.NewGuid(), Name="Default", IsActive=true, Application = applications[0]}
            };

            themes.ForEach(t => context.Theme.Add(t));
            context.SaveChanges();

            #endregion

            #region Media

            var media = new List<Media>
            {
                new Media{Id = Guid.NewGuid(), Name="HTML 5 intro", Description="Lorem ipsum dolor sit amet", FileUrl="html-5-jpg.jpg"},
                new Media{Id = Guid.NewGuid(), Name="CSS 3 Image", Description="Lorem ipsum dolor sit amet", FileUrl="css-3-jpg.jpg"},
                new Media{Id = Guid.NewGuid(), Name="Entity Framework", Description="Lorem ipsum dolor sit amet", FileUrl="entity-framework-jpg.jpg"},
                new Media{Id = Guid.NewGuid(), Name="Entity Framework", Description="Lorem ipsum dolor sit amet", FileUrl="entity-framework-1-jpg.jpg"},
                new Media{Id = Guid.NewGuid(), Name="Entity Framework", Description="Lorem ipsum dolor sit amet", FileUrl="entity-framework-2-jpg.jpg"},
            };

            media.ForEach(m => context.Media.Add(m));
            context.SaveChanges();

            #endregion

            #region Roles & Users


            // Add Roles
            var roles = new List<Role>
            {
                new Role{Id= Guid.NewGuid(), Name="Guest", Description="Guest role"},
                new Role{Id= Guid.NewGuid(), Name="Admin", Description="Admin role"},
                new Role{Id= Guid.NewGuid(), Name="SuperAdmin", Description="Super Admin role"}
            };

            roles.ForEach(s => context.Role.Add(s));
            context.SaveChanges();

            //var profiles = null;

            // Add users
            var users = new List<User>{
                new User{ Id = Guid.NewGuid(), Name="Eduardo", Email="eduardo.macho.cacho@gmail.com", Password= Helpers.PasswordHelper.HashPassword("tremendous"), 
                    CreateDate=DateTime.Parse("30/08/2015"), IsActive = true, IsLocked = false, Roles = new List<Role>{roles[0] as Role, roles[2] as Role},
                    Profile = new Profile{ Id = Guid.NewGuid(), FirstName="Eduardo", LastName="Macho", Country="España", State="Madrid", Town="Madrid", Address="Claudio coello 51, 4º Ctro. Izda", Image="foto_CV.jpg", PostalCode="28001"}, 
                    UserLogin = null},
                     new User{ Id = Guid.NewGuid(), Name="Bimba", Email="bimba@gmail.com", Password= Helpers.PasswordHelper.HashPassword("Bimba"), 
                    CreateDate=DateTime.Parse("30/08/2015"), IsActive = true, IsLocked = true, Roles = new List<Role>{roles[0] as Role, roles[1] as Role},
                    Profile = new Profile{ Id = Guid.NewGuid(), FirstName="Eduardo", LastName="Macho", Country="España", State="Madrid", Town="Madrid", Address="Claudio coello 51, 4º Ctro. Izda", Image="none", PostalCode="28001"},  
                    UserLogin = null},
                     new User{ Id = Guid.NewGuid(), Name="Pipina", Email="sol.marzellier@telefonica.net", Password= Helpers.PasswordHelper.HashPassword("Bimba"), 
                    CreateDate=DateTime.Parse("30/08/2015"), IsActive = true, IsLocked = true, Roles = new List<Role>{roles[0] as Role},
                    Profile = new Profile{ Id = Guid.NewGuid(), FirstName="Eduardo", LastName="Macho", Country="España", State="Madrid", Town="Madrid", Address="Claudio coello 51, 4º Ctro. Izda", Image="none", PostalCode="28001"},  
                    UserLogin = null}
                };

            users.ForEach(s => context.User.Add(s));
            context.SaveChanges();

            #endregion

            #region Notification status

            var notificationStatus = new List<NotificationStatus>{
                new NotificationStatus{ Id = Guid.NewGuid(), Name="Unread", Type="Alert"},
                new NotificationStatus{ Id = Guid.NewGuid(), Name="Read", Type="Alert"}
            };

            notificationStatus.ForEach(ns => context.NotificationStatus.Add(ns));
            context.SaveChanges();

            var notifications = new List<Notification>{
               
                new Notification{Id = Guid.NewGuid(), Name="New user registered",Title="New user registered", Content="A new user with #ID:fmdksalfmdskla has been registered", Users = new List<User> { users[0] },Status = notificationStatus[0],Priority = 0, Read = false, CreateDate = DateTime.Now    },
                new Notification{Id = Guid.NewGuid(), Name="New user registered",Title="New user registered", Content="A new user with #ID:fmdksalfmdskla has been registered", Users = new List<User> { users[0] },Status = notificationStatus[0],Priority = 0, Read = false, CreateDate = DateTime.Now    }
                
            };

            notifications.ForEach(n => context.Notification.Add(n));
            context.SaveChanges();

            #endregion

            #region Sections
            var sections = new List<Section>
            {
                new Section{Id = Guid.NewGuid(), Name="dashboard", Title="Dashboard", Controller="#", Glyphicon="dashboard", Roles = new List<Role>{roles[1],roles[2]}, SortOrder = 0},
                new Section{Id = Guid.NewGuid(), Name="settings", Title="Settings", Controller="#", Glyphicon="wrench",Roles = new List<Role>{roles[1],roles[2]}, SortOrder = 3},
                new Section{Id = Guid.NewGuid(), Name="blog", Title="Blog", Controller="blogs", Glyphicon="comment",Roles = new List<Role>{roles[2]}, SortOrder = 2},
                new Section{Id = Guid.NewGuid(), Name="languages", Title="Languages", Controller="languages", Glyphicon="globe",Roles = new List<Role>{roles[2]}, SortOrder = 4},
                new Section{Id = Guid.NewGuid(), Name="security", Title="Security", Controller="security", Glyphicon="lock", Roles = new List<Role>{roles[2]}, SortOrder = 5},
                new Section{Id = Guid.NewGuid(), Name="Media", Title="Media", Controller="media", Glyphicon="picture",Roles = new List<Role>{roles[2]}, SortOrder=6},             
                new Section{Id = Guid.NewGuid(), Name="pages", Title="Pages", Controller="pages", Glyphicon="file", Roles = new List<Role>{roles[2]}, SortOrder=1}                
            };

            sections.ForEach(s => context.Section.Add(s));
            context.SaveChanges();

            //var subsections = new List<Section>
            //{

            //    new Section{Id = Guid.NewGuid(), Name="blogs", Title="Blog settings", PrntId = sections[3].Id, ParentSection = sections[3], Controller="blogs", Glyphicon="page",Roles = new List<Role>{roles[1]}},
            //    new Section{Id = Guid.NewGuid(), Name="posts", Title="Posts", PrntId=sections[3].Id, ParentSection = sections[3], Controller="posts", Glyphicon="post",Roles = new List<Role>{roles[1]}},
            //    new Section{Id = Guid.NewGuid(), Name="postcategories", Title="Post Categories", PrntId=sections[3].Id, ParentSection = sections[3], Controller="postcategories", Glyphicon="post",Roles = new List<Role>{roles[1]}},
            //    new Section{Id = Guid.NewGuid(), Name="postcomments", Title="Post Comments", PrntId=sections[3].Id, ParentSection = sections[3], Controller="postcomments", Glyphicon="post",Roles = new List<Role>{roles[1]}},
            //    new Section{Id = Guid.NewGuid(), Name="posttypes", Title="Post Types", PrntId=sections[3].Id, ParentSection = sections[3], Controller="posttypes", Glyphicon="post",Roles = new List<Role>{roles[1]}}


            //};

            //subsections.ForEach(s => context.Section.Add(s));
            //context.SaveChanges();

            #endregion

            #region Codes & Languages & Translations

            var codes = new List<LanguageCode>
            {
                new LanguageCode{ Id = Guid.NewGuid(), Code = "es-ES"},
                new LanguageCode{ Id = Guid.NewGuid(), Code = "en-GB"},
                new LanguageCode{ Id = Guid.NewGuid(), Code = "fr-FR"}

            };

            codes.ForEach(s => context.LanguageCode.Add(s));
            context.SaveChanges();

            var languages = new List<Language>
            {
                new Language{Id = Guid.NewGuid(), Description="Spanish from spain", Name="Spanish-Spain", IsActive=true, LanguageCode = codes[0] as LanguageCode, LngCdId = codes[0].Id, SortOrder = 0},
                new Language{Id = Guid.NewGuid(), Description="English from Great Britain", Name="English-Great Britain", IsActive=true, LanguageCode = codes[1] as LanguageCode, LngCdId = codes[1].Id,SortOrder = 1},
                new Language{Id = Guid.NewGuid(), Description="Spanish from spain", Name="French-France", IsActive=true, LanguageCode = codes[2] as LanguageCode, LngCdId = codes[2].Id,SortOrder = 2}
            };

            languages.ForEach(s => context.Language.Add(s));
            context.SaveChanges();

            var translations = new List<Translation>()
            {
                new Translation{ Id = Guid.NewGuid(), Name="Shared.Layout.HelloMessage", Content = "¡Hola {0}!", Language = languages[0], LngId = languages[0].Id },
                new Translation{ Id = Guid.NewGuid(), Name="Shared.Layout.Login", Content = "Ingresar", Language = languages[0], LngId = languages[0].Id },
                new Translation{ Id = Guid.NewGuid(), Name="Shared.Layout.Login", Content = "Salir", Language = languages[0], LngId = languages[0].Id }
            };

            translations.ForEach(t => context.Translation.Add(t));
            context.SaveChanges();

            #endregion

            #region Keywords

            var keywords = new List<Keyword>
            {
                new Keyword{ Id = Guid.NewGuid(), KeywordName="Eduardo Macho" },
                new Keyword{ Id = Guid.NewGuid(), KeywordName="Computer Sciences" },
                new Keyword{ Id = Guid.NewGuid(), KeywordName="Web developer" },
                new Keyword{ Id = Guid.NewGuid(), KeywordName="Desarrollador Web" }
            };
            keywords.ForEach(k => context.Keyword.Add(k));
            context.SaveChanges();

            #endregion

            #region Templates & pages



            var templates = new List<Template>{
                new Template{ Id = Guid.NewGuid(), Name="Layout", Content="<html><head></head><body>@RenderBody()</body></html>", CreateDate=DateTime.Parse("30/08/2015"), IsActive = true, UpdateDate=null},
                new Template{ Id = Guid.NewGuid(), Name="AdminLayout", Content="<html><head></head><body>@RenderBody()</body></html>", CreateDate=DateTime.Parse("30/08/2015"), IsActive = true, UpdateDate=null},
            };

            templates.ForEach(s => context.Template.Add(s));
            context.SaveChanges();



            var pages = new List<Page>{
                new Page{Id  = Guid.NewGuid(), Name = "home", Content = "<h1>Welcome</h1><p>Home page</p>",  CreateDate=DateTime.Now, Template = templates[0], TplId = templates[0].Id, Title="Welcome to my page", UpdateDate=null, Language = languages[0] as Language, LngId = languages[0].Id, Keywords = new List<Keyword>{ keywords[0] as Keyword,keywords[1] as Keyword},IsDefault = true},
                new Page{Id  = Guid.NewGuid(), Name = "about", Content = "<h1>Welcome</h1><p>About PAge</p>", CreateDate=DateTime.Now, Template = templates[0], TplId = templates[0].Id,  Title="About my page", UpdateDate=null, Language = languages[0] as Language, LngId = languages[0].Id, Keywords = new List<Keyword>{ keywords[2] as Keyword}, IsDefault=false}
            };

            pages.ForEach(s => context.Page.Add(s));
            context.SaveChanges();

            var menus = new List<Menu>{
                new Menu{ Id= Guid.NewGuid(), Name="Main Menu", Template = templates[0], TplId = templates[0].Id, Pages = new List<Page>{ pages[0], pages[1] }, Language = languages[0]}
            };

            menus.ForEach(m => context.Menu.Add(m));
            context.SaveChanges();

            #endregion

            #region Tags

            var tags = new List<Tag>
            {
                 new Tag{Id = Guid.NewGuid(), Name="css"},
                 new Tag{Id = Guid.NewGuid(), Name=".net"},
                 new Tag{Id = Guid.NewGuid(), Name="css3"},
                 new Tag{Id = Guid.NewGuid(), Name="scrum"},
                 new Tag{Id = Guid.NewGuid(), Name="unit-testing"}

            };

            #endregion

            #region Blog & Post & Comments

            var blog = new List<Blog> { new Blog { Id = Guid.NewGuid(), Title = "Independent blog", Name = "Your source for technological knowledge", Description = "Your source for technological knowledge", IsActive = true, AppId = applications[0].Id, Application = applications[0] } };

            blog.ForEach(b => context.Blog.Add(b));
            context.SaveChanges();

            var postcategories = new List<PostCategory>
            {
                new PostCategory{ Id = Guid.NewGuid(), Name=".NET Development", CategoryName="net-development",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis eleifend sollicitudin mauris, iaculis ornare diam ornare sed. Ut sapien ligula, luctus in ex at, scelerisque ullamcorper elit. Donec hendrerit ultrices nisi non ullamcorper. Maecenas sodales lorem nec elementum cursus. Nam ut lorem aliquam, sagittis augue quis, dignissim lorem. Aenean eu molestie ex. Nulla elementum tincidunt est. Integer lacinia urna non justo finibus, vitae maximus elit fringilla."},
                new PostCategory{ Id = Guid.NewGuid(), Name="CSS3", CategoryName ="css3",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis eleifend sollicitudin mauris, iaculis ornare diam ornare sed. Ut sapien ligula, luctus in ex at, scelerisque ullamcorper elit. Donec hendrerit ultrices nisi non ullamcorper. Maecenas sodales lorem nec elementum cursus. Nam ut lorem aliquam, sagittis augue quis, dignissim lorem. Aenean eu molestie ex. Nulla elementum tincidunt est. Integer lacinia urna non justo finibus, vitae maximus elit fringilla."},                
                new PostCategory{ Id = Guid.NewGuid(), Name="Javascript", CategoryName="javascript",
                Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis eleifend sollicitudin mauris, iaculis ornare diam ornare sed. Ut sapien ligula, luctus in ex at, scelerisque ullamcorper elit. Donec hendrerit ultrices nisi non ullamcorper. Maecenas sodales lorem nec elementum cursus. Nam ut lorem aliquam, sagittis augue quis, dignissim lorem. Aenean eu molestie ex. Nulla elementum tincidunt est. Integer lacinia urna non justo finibus, vitae maximus elit fringilla."},
                new PostCategory{ Id = Guid.NewGuid(), Name="SCRUM", CategoryName="scrum",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis eleifend sollicitudin mauris, iaculis ornare diam ornare sed. Ut sapien ligula, luctus in ex at, scelerisque ullamcorper elit. Donec hendrerit ultrices nisi non ullamcorper. Maecenas sodales lorem nec elementum cursus. Nam ut lorem aliquam, sagittis augue quis, dignissim lorem. Aenean eu molestie ex. Nulla elementum tincidunt est. Integer lacinia urna non justo finibus, vitae maximus elit fringilla."},
                new PostCategory{ Id = Guid.NewGuid(), Name="Management", CategoryName="management",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis eleifend sollicitudin mauris, iaculis ornare diam ornare sed. Ut sapien ligula, luctus in ex at, scelerisque ullamcorper elit. Donec hendrerit ultrices nisi non ullamcorper. Maecenas sodales lorem nec elementum cursus. Nam ut lorem aliquam, sagittis augue quis, dignissim lorem. Aenean eu molestie ex. Nulla elementum tincidunt est. Integer lacinia urna non justo finibus, vitae maximus elit fringilla."},
                     new PostCategory{ Id = Guid.NewGuid(), Name="SQL Server", CategoryName="sql-server",
                    Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis eleifend sollicitudin mauris, iaculis ornare diam ornare sed. Ut sapien ligula, luctus in ex at, scelerisque ullamcorper elit. Donec hendrerit ultrices nisi non ullamcorper. Maecenas sodales lorem nec elementum cursus. Nam ut lorem aliquam, sagittis augue quis, dignissim lorem. Aenean eu molestie ex. Nulla elementum tincidunt est. Integer lacinia urna non justo finibus, vitae maximus elit fringilla."}

            };

            postcategories.ForEach(pc => context.PostCategories.Add(pc));
            context.SaveChanges();

            var posttypes = new List<PostType> 
            {
                new PostType{ Id = Guid.NewGuid(), Name ="Image post", Description="A post with a default image" },
                new PostType{ Id = Guid.NewGuid(), Name ="Video post", Description="A post with a default video" },
                new PostType{ Id = Guid.NewGuid(), Name ="Text post", Description="A post with text" }

            };

            posttypes.ForEach(pt => context.PostTypes.Add(pt));
            context.SaveChanges();

            var posts = new List<Post>
            {
                new Post{Id= Guid.NewGuid(), Blog = blog[0] as Blog, Title="Your first CodeFirst application", 
                    Excerpt="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris porttitor libero ac facilisis ullamcorper. Vivamus ligula sem, convallis in accumsan et, venenatis et lectus. Cras sed placerat sem. Maecenas justo lectus, elementum vitae porta sed, eleifend non est. Cras gravida quam mi, et efficitur erat maximus quis. Cras ut odio ut diam vehicula posuere at ut neque. Suspendisse vulputate consequat justo, quis dapibus nibh tristique ac...", 
                    Content="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed facilisis est quis facilisis molestie. Proin aliquam mi eget eros lobortis, eget dignissim est venenatis. Donec quis lacinia ipsum, ac dignissim neque. Duis velit nibh, consectetur quis placerat vel, tristique at leo. Nunc a dui nisl. In nunc erat, varius lobortis quam eget, efficitur blandit elit. Cras tincidunt mattis lectus, ut molestie lorem tempus vitae. Nam imperdiet nec magna quis malesuada. Sed ultricies mattis feugiat. Praesent volutpat lobortis nibh at pharetra. Nullam eget dignissim turpis. Aenean euismod, massa aliquet sodales rhoncus, neque purus hendrerit nulla, sit amet ornare libero elit et velit. Aliquam vitae diam tellus. Pellentesque a varius elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed facilisis est quis facilisis molestie. Proin aliquam mi eget eros lobortis, eget dignissim est venenatis. Donec quis lacinia ipsum, ac dignissim neque. Duis velit nibh, consectetur quis placerat vel, tristique at leo. Nunc a dui nisl. In nunc erat, varius lobortis quam eget, efficitur blandit elit. Cras tincidunt mattis lectus, ut molestie lorem tempus vitae. Nam imperdiet nec magna quis malesuada. Sed ultricies mattis feugiat. Praesent volutpat lobortis nibh at pharetra. Nullam eget dignissim turpis. Aenean euismod, massa aliquet sodales rhoncus, neque purus hendrerit nulla, sit amet ornare libero elit et velit. Aliquam vitae diam tellus. Pellentesque a varius elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed facilisis est quis facilisis molestie. Proin aliquam mi eget eros lobortis, eget dignissim est venenatis. Donec quis lacinia ipsum, ac dignissim neque. Duis velit nibh, consectetur quis placerat vel, tristique at leo. Nunc a dui nisl. In nunc erat, varius lobortis quam eget, efficitur blandit elit. Cras tincidunt mattis lectus, ut molestie lorem tempus vitae. Nam imperdiet nec magna quis malesuada. Sed ultricies mattis feugiat. Praesent volutpat lobortis nibh at pharetra. Nullam eget dignissim turpis. Aenean euismod, massa aliquet sodales rhoncus, neque purus hendrerit nulla, sit amet ornare libero elit et velit. Aliquam vitae diam tellus. Pellentesque a varius elit.", Comments = null, BlgId = blog[0].Id,  CreateDate=DateTime.Now, Category = postcategories[5] as PostCategory, CatId = postcategories[5].Id,
                PostType = posttypes[0] as PostType, PstId=posttypes[0].Id,
                Viewed = 100,Loved = 3, Shared=1, Url = "your-first-codefirst-application", Images = new List<Media>{media[2], media[3], media[4]}, Author = users[0], AuthId = users[0].Id},
                new Post{Id= Guid.NewGuid(), Blog = blog[0] as Blog, Title="Unveiling the best tricks of HTML 5", 
                    Excerpt="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris porttitor libero ac facilisis ullamcorper. Vivamus ligula sem, convallis in accumsan et, venenatis et lectus. Cras sed placerat sem. Maecenas justo lectus, elementum vitae porta sed, eleifend non est. Cras gravida quam mi, et efficitur erat maximus quis. Cras ut odio ut diam vehicula posuere at ut neque. Suspendisse vulputate consequat justo, quis dapibus nibh tristique ac...", 
                    Content="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed facilisis est quis facilisis molestie. Proin aliquam mi eget eros lobortis, eget dignissim est venenatis. Donec quis lacinia ipsum, ac dignissim neque. Duis velit nibh, consectetur quis placerat vel, tristique at leo. Nunc a dui nisl. In nunc erat, varius lobortis quam eget, efficitur blandit elit. Cras tincidunt mattis lectus, ut molestie lorem tempus vitae. Nam imperdiet nec magna quis malesuada. Sed ultricies mattis feugiat. Praesent volutpat lobortis nibh at pharetra. Nullam eget dignissim turpis. Aenean euismod, massa aliquet sodales rhoncus, neque purus hendrerit nulla, sit amet ornare libero elit et velit. Aliquam vitae diam tellus. Pellentesque a varius elit.", Comments = null, BlgId = blog[0].Id, CreateDate=DateTime.Now, Category = postcategories[0] as PostCategory, CatId = postcategories[0].Id,
                PostType = posttypes[2] as PostType, PstId=posttypes[2].Id, Url = "unveiling-the-best-tricks-of-html-5", Author = users[0], AuthId = users[0].Id},
                new Post{Id= Guid.NewGuid(), Blog = blog[0] as Blog, Title="Rotating layers with CSS3", Excerpt="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed facilisis est quis facilisis molestie. Proin aliquam mi eget eros lobortis, eget dignissim est venenatis. Donec quis lacinia ipsum, ac dignissim neque. Duis velit nibh, consectetur quis placerat vel...", Content="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed facilisis est quis facilisis molestie. Proin aliquam mi eget eros lobortis, eget dignissim est venenatis. Donec quis lacinia ipsum, ac dignissim neque. Duis velit nibh, consectetur quis placerat vel, tristique at leo. Nunc a dui nisl. In nunc erat, varius lobortis quam eget, efficitur blandit elit. Cras tincidunt mattis lectus, ut molestie lorem tempus vitae. Nam imperdiet nec magna quis malesuada. Sed ultricies mattis feugiat. Praesent volutpat lobortis nibh at pharetra. Nullam eget dignissim turpis. Aenean euismod, massa aliquet sodales rhoncus, neque purus hendrerit nulla, sit amet ornare libero elit et velit. Aliquam vitae diam tellus. Pellentesque a varius elit.", Comments = null, BlgId = blog[0].Id,  CreateDate=DateTime.Now, Category = postcategories[1] as PostCategory, CatId = postcategories[1].Id,
                PostType = posttypes[2] as PostType, PstId=posttypes[2].Id, Viewed = 90, Loved = 45, Shared=2, Url="rotating-layers-with-css3" , Author = users[0], AuthId = users[0].Id},
                new Post{Id= Guid.NewGuid(), Blog = blog[0] as Blog, Title="CSS class for noobs", Url = "css", Excerpt="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed facilisis est quis facilisis molestie. Proin aliquam mi eget eros lobortis, eget dignissim est venenatis. Donec quis lacinia ipsum, ac dignissim neque. Duis velit nibh, consectetur quis placerat vel...", Content="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed facilisis est quis facilisis molestie. Proin aliquam mi eget eros lobortis, eget dignissim est venenatis. Donec quis lacinia ipsum, ac dignissim neque. Duis velit nibh, consectetur quis placerat vel, tristique at leo. Nunc a dui nisl. In nunc erat, varius lobortis quam eget, efficitur blandit elit. Cras tincidunt mattis lectus, ut molestie lorem tempus vitae. Nam imperdiet nec magna quis malesuada. Sed ultricies mattis feugiat. Praesent volutpat lobortis nibh at pharetra. Nullam eget dignissim turpis. Aenean euismod, massa aliquet sodales rhoncus, neque purus hendrerit nulla, sit amet ornare libero elit et velit. Aliquam vitae diam tellus. Pellentesque a varius elit.", Comments = null, BlgId = blog[0].Id,  CreateDate=DateTime.Now, Category = postcategories[1] as PostCategory, CatId = postcategories[1].Id,
                PostType = posttypes[2] as PostType, PstId=posttypes[2].Id, Viewed = 40,Loved = 45, Shared=2, Author = users[0], AuthId = users[0].Id},
                new Post{Id= Guid.NewGuid(), Blog = blog[0] as Blog, Title="Installing SQL Server 2008 Standard on Windows Server 2008", 
                    Excerpt="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris porttitor libero ac facilisis ullamcorper. Vivamus ligula sem, convallis in accumsan et, venenatis et lectus. Cras sed placerat sem. Maecenas justo lectus, elementum vitae porta sed, eleifend non est. Cras gravida quam mi, et efficitur erat maximus quis. Cras ut odio ut diam vehicula posuere at ut neque. Suspendisse vulputate consequat justo, quis dapibus nibh tristique ac...", 
                    Content="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed facilisis est quis facilisis molestie. Proin aliquam mi eget eros lobortis, eget dignissim est venenatis. Donec quis lacinia ipsum, ac dignissim neque. Duis velit nibh, consectetur quis placerat vel, tristique at leo. Nunc a dui nisl. In nunc erat, varius lobortis quam eget, efficitur blandit elit. Cras tincidunt mattis lectus, ut molestie lorem tempus vitae. Nam imperdiet nec magna quis malesuada. Sed ultricies mattis feugiat. Praesent volutpat lobortis nibh at pharetra. Nullam eget dignissim turpis. Aenean euismod, massa aliquet sodales rhoncus, neque purus hendrerit nulla, sit amet ornare libero elit et velit. Aliquam vitae diam tellus. Pellentesque a varius elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed facilisis est quis facilisis molestie. Proin aliquam mi eget eros lobortis, eget dignissim est venenatis. Donec quis lacinia ipsum, ac dignissim neque. Duis velit nibh, consectetur quis placerat vel, tristique at leo. Nunc a dui nisl. In nunc erat, varius lobortis quam eget, efficitur blandit elit. Cras tincidunt mattis lectus, ut molestie lorem tempus vitae. Nam imperdiet nec magna quis malesuada. Sed ultricies mattis feugiat. Praesent volutpat lobortis nibh at pharetra. Nullam eget dignissim turpis. Aenean euismod, massa aliquet sodales rhoncus, neque purus hendrerit nulla, sit amet ornare libero elit et velit. Aliquam vitae diam tellus. Pellentesque a varius elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed facilisis est quis facilisis molestie. Proin aliquam mi eget eros lobortis, eget dignissim est venenatis. Donec quis lacinia ipsum, ac dignissim neque. Duis velit nibh, consectetur quis placerat vel, tristique at leo. Nunc a dui nisl. In nunc erat, varius lobortis quam eget, efficitur blandit elit. Cras tincidunt mattis lectus, ut molestie lorem tempus vitae. Nam imperdiet nec magna quis malesuada. Sed ultricies mattis feugiat. Praesent volutpat lobortis nibh at pharetra. Nullam eget dignissim turpis. Aenean euismod, massa aliquet sodales rhoncus, neque purus hendrerit nulla, sit amet ornare libero elit et velit. Aliquam vitae diam tellus. Pellentesque a varius elit.", 
                    Comments = null, BlgId = blog[0].Id,  CreateDate=DateTime.Now, Category = postcategories[5] as PostCategory, CatId = postcategories[5].Id,
                PostType = posttypes[0] as PostType, PstId=posttypes[0].Id,
                Viewed = 100,Loved = 3, Shared=1, Url = "installing-sql-server-2008-standard-on-windows-server-2008", Author = users[0], AuthId = users[0].Id}
            };

            posts.ForEach(p => context.Post.Add(p));
            context.SaveChanges();

            var comments = new List<Comment>
            {
                new Comment{Id = Guid.NewGuid(), Content="This is a test comment", Post = posts[0] as Post, PstId = posts[0].Id, CreateDate = DateTime.Now, IsActive= true, Author= users[0] as User}
            };

            comments.ForEach(c => context.Comment.Add(c));
            context.SaveChanges();

            #endregion


        }

    }
}