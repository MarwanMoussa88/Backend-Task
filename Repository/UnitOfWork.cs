using AutoMapper;
using Backend_Task.Data;
using Backend_Task.Entities;
using Backend_Task.Repository.IRepository;
using Microsoft.AspNetCore.Identity;

namespace Backend_Task.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IProductRepository productRepository {get;}
        public IAuthManager AuthManager {get;}


        public UnitOfWork(ApplicationDbContext context,
            IMapper mapper,
            IConfiguration configuration,
            UserManager<User> userManager,
            IWebHostEnvironment webHostEnvironment)
        {
            this._context = context;
            this._mapper = mapper;
            this._configuration = configuration;
            this._userManager = userManager;
            this._webHostEnvironment = webHostEnvironment;
            productRepository = new ProductRepository(_context, _mapper,_webHostEnvironment);
            AuthManager=new AuthManager(_mapper,_userManager,_configuration);

        }
    }
}
