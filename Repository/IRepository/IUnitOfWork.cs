namespace Backend_Task.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public IProductRepository productRepository { get;  }
        public IAuthManager AuthManager { get; }
    }
}
