using AutoMapper;
using System.Diagnostics;
using TTM.DataAccess;
using TTM.Domain.Interfaces;

namespace TTM.Business
{
    public abstract class BaseService<TModel, TEntity> : ICrudService<TModel>
        where TEntity : class, IIdentity, new()
        where TModel : class, new()
    {

        //Context with dependency injection.
        protected readonly TTMContext _context;
        protected BaseService(TTMContext ttmContext)
        {
            _context = ttmContext;
            var mapperConfig = new MapperConfiguration(_mapperConfigurationExpression);
            _mapper = mapperConfig.CreateMapper();
        }

        //It is necessary to provide a structure that uses the incoming DTO to automatically identify the entity or vice versa.
        protected static readonly MapperConfigurationExpression _mapperConfigurationExpression;
        protected readonly IMapper _mapper;
        static BaseService()
        {
            _mapperConfigurationExpression = new MapperConfigurationExpression();
            _mapperConfigurationExpression.CreateMap<TModel, TEntity>();
            _mapperConfigurationExpression.CreateMap<TEntity, TModel>();
        }

        public virtual TModel GetById(int id)
        {
            try
            {
                var entity = _context.Set<TEntity>().Find(id);
                if (entity == null)
                {
                    return null;
                }
                return _mapper.Map<TModel>(entity);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return default;
            }
        }
        public virtual IEnumerable<TModel> GetAll()
        {
            try
            {
                var dtoList = new List<TModel>();
                var allEntities = _context.Set<TEntity>().ToList();
                foreach ( var entity in allEntities )
                {
                    var dto = _mapper.Map<TModel>(entity);
                    dtoList.Add(dto);
                }
                return dtoList;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return Enumerable.Empty<TModel>();
            }
        }
        public virtual CommandResult Create(TModel model)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(model);
                _context.Add(entity);
                _context.SaveChanges();
                return CommandResult.Success();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error(ex);
            }
        }
        public virtual CommandResult Update(TModel model)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(model);
                _context.Update(entity);
                _context.SaveChanges();
                return CommandResult.Success();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error(ex);
            }
        }
        public virtual CommandResult Delete(int id)
        {
            try
            {
                var entity = _context.Set<TEntity>().First(e => e.Id == id);
                if (entity == null)
                {
                    _context.Remove(entity);
                    _context.SaveChanges();
                    return CommandResult.Success();
                }
                else
                {
                    return CommandResult.Failure();
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error(ex);
            }
        }
        public virtual CommandResult Delete(TModel model)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(model);
                _context.Remove(entity);
                _context.SaveChanges();
                return CommandResult.Success();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error(ex);
            }
        }
        public virtual CommandResult RecordExists(TModel model)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(model);
                if (entity == null)
                {
                    return null;
                }
                return CommandResult.Success();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                return CommandResult.Error(ex);
            }
        }
    }
}
