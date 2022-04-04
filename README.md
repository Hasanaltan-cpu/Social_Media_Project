![DDD](https://gblobscdn.gitbook.com/assets%2F-MRsSS_At9fCka5xk3SA%2F-MWEn-jX0WmLhDQvP1MO%2F-MWEnIepGpMPsjyZC6bK%2Fddd.png?alt=media&token=634a6c86-68b9-43f6-b25d-5ffd9875d14d)





  
Domain Driven Design architecture schema.



<h6>In this project i use these packages & tools :<h6>

* Microsoft.AspNetCore.SignalR(v1.1.0)
* Microsoft.AspNetCore.Authentication.Google(v3.1.6)
* Microsoft.AspNetCore.Mvc.NewtonsoftJson(v3.1.7)
* Microsoft.VisualStudio.Web.CodeGeneration.Design(v3.1.5)
* Automapper(v10.1.1),
* FluentValidation.AspNetCore(v9.5.1),
* Microsoft.AspNetCore.Identity(v9.5.3)
* Microsoft.extensions.Hosting(v.3.1.6)
* SixLabors.ImageSharp(v1.0.0)
* Microsoft.EntityFrameworkCore(v3.1.6)
* Microsoft.Extensions.Identity.Stores(v3.1.6)
* Microsoft.AspNetCore.Identity.EntityFrameWorkCore(v3.1.6)
* Microsoft.EntityFrameworkCore.Design(v3.1.6)
* Microsoft.EntityFrameWorkCore.SqlServer(v3.1.6)
* Microsoft.EntityFrameWorkCore.SqlServer.Design(v1.1.6)
* Microsoft.EntityFrameWorkCore.Tools(v3.1.6)
* AutoMapper.Extensions.Microsoft.DependencyInjection(v8.0.1)
* AdminLTE v.3.0.5 for Theme


1-1-First of all,open a Blank Solution.

2- Social_Media_Project.DomainLayer will open as a .Net Core Library Project.

2.1.Create an Enums folder.

2.2.Create an Entities folder.

2.2.1. Open an Interface folder.

2.2.2.Create IBase<t> type and IBaseEntity interface classes.
    
      public interface IBaseEntity
    {
        DateTime CreateDate { get; }

        DateTime? ModifiedDate { get; set; }
        DateTime? DeletedDate { get; set; }

        Status Status { get; set; }
    }
    
    
2.2.3.Create any entities what you need.
    
Note:At the user process step,i will use .Net Core Identity class that's why AppUserRole and AppUser classes will inherit from Identity class.
    
    public class AppUser :IdentityUser<int>, IBaseEntity
    {
        public AppUser()
        {
            Posts = new List<Post>();
            Shares = new List<Share>();
            Likes = new List<Like>();
            Mentions = new List<Mention>();
            Followers = new List<Follow>();
            Followings = new List<Follow>();
            Messages = new HashSet<Message>();
        }

        public virtual ICollection<Message> Messages { get; set; }
        public string Name { get; set; }

        public string ImagePath { get; set; } = "/images/users/default.jpg";

        public DateTime CreateDate { get { return DateTime.Now; } private set { } }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        public Status Status { get; set; }

        public List<Post> Posts { get; set; }

        public List<Share> Shares { get; set; }

        public List<Like> Likes { get; set; }
        public List<Mention> Mentions { get; set; }

        [InverseProperty("Follower")]
        public List<Follow> Followers { get; set; }

        [InverseProperty("Following")]

        public List<Follow> Followings { get; set; }

    }
    
    
What is the InverseProperty? 
   
    -Inverse Property attribute is used when you need to indicate that navigation property Follower is related to the same foreign key as another navigation property Following.

2.3.Create a Repositories folder.In this project,i create methods which async programming for the CRUD operations. For DIP create an Interfaces for each entity.
Don't create IAppRole because of ORM . This class just uses for entity.
      
      public class AppUserRepository:BaseRepository<AppUser>,IAppUserRepository
    {
        public AppUserRepository(ApplicationDbContext context) : base(context) { }
    }	
  
  
2.4.Create an UnitofWork folder => IUnitOfWork.cs interface.In this part,i add repositories which i need to use to interface of UnitofWork.
  
  <h4>What is the UnitOfWork? <h4>
  
  
    -As u know,every transaction has expense for the database in terms of bringing data.UnitOfWork provide us to all transaction just goes from one bridge to database,furthermore,
    after all transaction we use just one SaveChanges().
    
    public  interface IUnitOfWork :IAsyncDisposable => IAsyncDisposable means , an object whic was created by class it will be delete from heap part of RAM after UnitofWork will be done.
    {
       IPostRepository Post { get; }

       IMentionRepository Mention { get; }

       IAppUserRepository AppUser { get; }

        IFollowRepository Follow { get; }
        ILikeRepository Like { get; }
        IShareRepository Share { get; }

        Task Commit();

        Task ExecuteSqlRaw(string sql, params object[] parameters);

    }
    
3-Social_Media_Project.InfrastructureLayer ClassLibrary(.Core) Project is opened.

3.1.Create a Mapping folder.Create Abstract & Concrete Folders. Mapping process is completed here.
  

public abstract class BaseMap<T>: IEntityTypeConfiguration<T> where T: class,IBaseEntity => We mapping here what we need to relationship on the SQL Database.
    {

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Status).IsRequired(true);
            builder.Property(x => x.CreateDate).IsRequired(true);
            builder.Property(x => x.ModifiedDate).IsRequired(false);
            builder.Property(x => x.DeletedDate).IsRequired(false);
        }
    }
 3.2.Create a Context folder.Create an AppLicationDbContex.cs.
  
  public class ApplicationDbContext:IdentityDbContext<AppUser,AppRole,int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Mention> Mentions { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MessageMap());
            builder.ApplyConfiguration(new PostMap());
            builder.ApplyConfiguration(new MentionMap());
            builder.ApplyConfiguration(new AppUserMap());
            builder.ApplyConfiguration(new ShareMap());
            builder.ApplyConfiguration(new LikeMap());
            builder.ApplyConfiguration(new FollowMap());
            base.OnModelCreating(builder);
        }

3.3.Create a Repositories folder.Repositories will be contrete on this part from Interface.
  
  
    public  interface IAppUserRepository:IRepository<AppUser> => With this way, we handle a losecoupled classes.
    {
    }
    
3.4.create a UnitofWork folder.
  
   public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            this._db = db ??
            throw new ArgumentNullException("Database can not be null");

        }
        private IPostRepository _postRepository;
        public IPostRepository Post { get { return _postRepository ?? (_postRepository = new PostRepository(_db)); } }
        private IMentionRepository _mentionRepository;
        public IMentionRepository Mention {  get { return _mentionRepository ?? (_mentionRepository = new MentionRepository(_db)); } }
        private IAppUserRepository _appUserRepository;
        public IAppUserRepository AppUser { get { return _appUserRepository ?? (_appUserRepository = new AppUserRepository(_db)); } }
        private IFollowRepository _followRepository;
        public IFollowRepository Follow { get { return _followRepository ?? (_followRepository = new FollowRepository(_db)); } }
        private ILikeRepository _likeRepository;
        public ILikeRepository Like { get { return _likeRepository ?? (_likeRepository = new LikeRepository(_db)); } }
        private IShareRepository _shareRepository;
        public IShareRepository Share { get { return _shareRepository ?? (_shareRepository = new ShareRepository(_db)); } }
        
        public async  Task Commit()
        {
            await _db.SaveChangesAsync();
        }

        private bool isDisposed = false;
        public async  ValueTask DisposeAsync()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                await DisposeAsync(true);
                GC.SuppressFinalize(this);
            };
        }

        protected async ValueTask DisposeAsync(bool disposing)
        {
            if (disposing)
            {
                await _db.DisposeAsync();
            }
        }
        public async  Task ExecuteSqlRaw(string sql, params object[] parameters)
        {
            await _db.Database.ExecuteSqlRawAsync(sql,parameters);
        }
    }
    
 3.5.EntityFrameworkCore.SqlServer&EntityFrameWorkCore.Tools packages is downloaded.
 
 4-Social_Media_Project.ApplicationLayer ClassLibrary(.Core) Project is opened.

4.1.Create Models folder.In this folder includes Data Transfer Objects(DTOs) and View Models (Vms).
  
4.2.Implement the AutoMapper.

  You can download from NuGet Package Manager or u can write to console window:
  
  PM> Install-Package AutoMapper
  
  *Why we need Automapper?
  
  -We need to transfer some datas from database to model for to use.In this part,we use Data Transfer Object (DTO).In addition to that,thanks to AutoMapper first usage is primitive
  i mean it is oldschool but the second one is beneficials of AutoMapper.We don't need to write all properties for pointing out EntityModel & EntityDto relationship.
  
  The First One;
  
  var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EntityModel, EntityDTO>();
            });
  IMapper iMapper = config.CreateMapper();
  var source = new AuthorModel();
  entity.Id = 1;                              
  entity.FirstName = "Hasan";
  entity.LastName = "ALTAN";
  entity.Address = "Turkey";
  var destination = iMapper.Map<EntityModel, EntityDTO>(source);
  
  AutoMapper;
   public Mapping()
        {
            CreateMap<AppUser, RegisterDto>().ReverseMap();
            CreateMap<AppUser, LoginDto>().ReverseMap();
            CreateMap<AppUser, ExternalLoginDto>().ReverseMap();
            CreateMap<AppUser, EditProfileDto>().ReverseMap();
            CreateMap<AppUser, ProfileSummaryDto>().ReverseMap();
            CreateMap<Follow, FollowDto>().ReverseMap();
            CreateMap<Like, LikeDto>().ReverseMap();
            CreateMap<Post, SendPostDto>().ReverseMap();

            CreateMap<Mention, MentionDto>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.AppUser.Name))
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.AppUser.UserName))
                .ForMember(d => d.UserImage, opt => opt.MapFrom(s => s.AppUser.ImagePath))
                .ReverseMap();

            CreateMap<Mention, AddMentionDto>().ReverseMap();
        }


4.2.1.Create a folder which name is Mapper.Mapping.cs class is created.Mapping.cs inherited from Profile.cs that's why we should download AutoMapper package.

4.2.2.AutoMapper & AutoMapper.DependencyInjection packages download to dependencies.

4.3.Create a folder which name is InversionOfControl then create DependencyInjection class.In this class,all coupled classes will register and resolve.In this project i use Built-in Container but we can choose another container as a 3rd part Autofact.

4.4.Create a Services folder.

Note:SixLabors.ImageSharp was downloaded for "Image Process".

4.4.1.Create Services=>Interfaces folder.For using PresentationLayer we create service interface.

public interface IAppUserService
    {
        Task DeleteUser(params object[] parameters);

        Task<IdentityResult> Register(RegisterDto model);

        Task<SignInResult> Login(LoginDto model);

        Task LogOut();

        Task<int> UserIdFromName(string UserName);

        AuthenticationProperties ExternalLogin(string provider, string redirectUrl);

        Task<ExternalLoginInfo> GetExternalLoginInfo();

        Task<SignInResult> ExternalLoginSignIn(string provider, string key);

        Task<IdentityResult> ExternalRegister(ExternalLoginInfo info, ExternalLoginDto model);

        Task<EditProfileDto> GetById(int id);

        Task EditUser(EditProfileDto id);

        Task<ProfileSummaryDto> GetByName(string userName);

        Task<List<SearchUserDto>> SearchUser(string keyword,int pageIndex);

        Task<List<FollowListVm>> UsersFollowings(int id, int pageIndex);

        Task<List<FollowListVm>> UsersFollowers(int id, int pageIndex);
 }

4.4.2.Create Services=>Concrete folder.For the services , we use service interface methods body.
    
      *This is just an example for task body part.
      
        public async Task DeleteUser(params object[] parameters)
        {
            await _unitOfWork.ExecuteSqlRaw("spDeleteUsers {0}", parameters);
        }
4.5.IoC=>InversionOfControl folder is opened.Then Register and Resolver on the DependencyInjection class.
   
   public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Mapping));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IFollowService, FollowService>();
            services.AddScoped<ILikeService, LikeService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IMentionService, MentionService>();

           

            services.AddIdentity<AppUser, AppRole>(x =>
             {
                 x.SignIn.RequireConfirmedPhoneNumber = false;
                 x.SignIn.RequireConfirmedAccount = false;
                 x.SignIn.RequireConfirmedEmail = false;
                 x.User.RequireUniqueEmail = true;
                 x.Password.RequiredLength = 1;
                 x.Password.RequiredUniqueChars = 0;
                 x.Password.RequireUppercase = false;
                 x.Password.RequireNonAlphanumeric = false;
                 x.Password.RequireLowercase = false;
             }).AddEntityFrameworkStores<ApplicationDbContext>();
            return services;
        }

    }
    
4.6.Create a Services=>Extensions=> ClaimPrincipleExtension.cs class.Create some rules for getting users informations.
  
  public static class ClaimsPrincipalExtensions
    {
        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Email);
        }

        public static int GetUserId (this ClaimsPrincipal principal)
        {
            return (Convert.ToInt32(principal.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        public static string GetUserName (this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Name);

        }

        public static bool IsCurrentUser(this ClaimsPrincipal principal,string id)
        {
            var currentUserId = GetUserId(principal).ToString();
            return string.Equals(currentUserId, id, StringComparison.OrdinalIgnoreCase);
        }
    }
    
4.7.Services=>Validations folder is opened.

		**FluentValidation.AspNetCore package is installed.
    
	  public class LoginValidation: AbstractValidator<LoginDto>  ---->FluentValidation provides us AbstractValidator parent class.
    {
        public LoginValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Enter a UserName");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Please Enter a Password");
        }
      
    }
    
    public  class ExternalLoginValidation:AbstractValidator<ExternalLoginDto>
    {
        public ExternalLoginValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Enter a Email adress").EmailAddress().WithMessage("Please Enter a valid E-mail adress.");

            RuleFor(x => x.Name).NotEmpty().WithMessage("Enter a Name").MinimumLength(3).MaximumLength(50).WithMessage("Minimum 3, Maximum 50 character please.");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("Enter a UserName").MinimumLength(3).MaximumLength(50).WithMessage("Minimum 3 , Maximum 50 character please.");

        }
    }
    
4.8.Social_Media_Project Asp.Net Core (Web Project) is opened.

4.8.1.Open a controller for every Model&View entity.

    For instance;
     public class MessageController : Controller
    {
       
        public readonly ApplicationDbContext _context;
        public readonly UserManager<AppUser> _userManager;
        public MessageController(UserManager<AppUser> userManager,
                                 ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }
        [Authorize]

        public async Task<IActionResult> Messenger()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUserName = currentUser.UserName;
            }
            
            var messages = await _context.Messages.ToListAsync();
            return View(messages);
        }


        public async Task<IActionResult> Create(Message message)
        {
            if (ModelState.IsValid)
            {
                message.UserName = User.Identity.Name;
                var sender = await _userManager.GetUserAsync(User);
                message.UserId = sender.Id;
                await _context.Messages.AddAsync(message);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest("Opps something has been wrong..");
        }
    }
	  
	  
	  
	  

	  
