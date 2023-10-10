namespace Backend_Task.Repository.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<TResult> Add<TSource, TResult>(TSource entity);

        Task Update<TSource>(string id, TSource source);

        Task<IEnumerable<TResult>> GetAll<TResult>();

        Task<TResult> Get<TResult>(string id);

        Task Delete(string? id);

        Task<bool> Exists(string id);

    }
}
