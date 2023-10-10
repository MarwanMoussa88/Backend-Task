using AutoMapper;
using AutoMapper.QueryableExtensions;
using Backend_Task.Data;
using Backend_Task.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Backend_Task.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class 
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GenericRepository(ApplicationDbContext context,IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public async Task<TResult> Add<TSource, TResult>(TSource entity)
        {
            var genericEntity=_mapper.Map<T>(entity);
            await _context.AddAsync(genericEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<TResult>(genericEntity);
        }

        public async Task Delete(string? id)
        {
            var entity = await _context.FindAsync<T>(id);
            if (entity is null)
            {
                throw new Exception();
            }
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> Exists(string id)
        {
            return await _context.FindAsync<T>(id) is null;
        }

        public async Task<TResult> Get<TResult>(string id)
        {

            var entity = await _context.FindAsync<T>(id);
            if (entity is null)
            {
                throw new Exception();
            }
            return _mapper.Map<TResult>(entity);
        }

        public async Task<IEnumerable<TResult>> GetAll<TResult>()
        {
            var entities = await _context.Set<T>().ProjectTo<TResult>(_mapper.ConfigurationProvider).ToListAsync();
            return entities;
        }

        public async Task Update<TSource>(string id, TSource source)
        {
            //Get Orignal from id
            var entity = await _context.FindAsync<T>(id);
            if (entity is null)
            {
                throw new Exception();
            }
            //Map Dto Object to orignal object
            _mapper.Map(source, entity);
            //Update
            _context.Update(entity);
            //Save
            await _context.SaveChangesAsync();

        }
    }
}
