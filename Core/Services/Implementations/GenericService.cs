using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OnlineClinic.Data.Entities;
using OnlineClinic.Data.Repositories;

namespace OnlineClinic.Core.Services
{
    public class GenericService<E>: IGenericService<E> where E: Entity
    {
        protected readonly IRepository<E> _repository;

        protected readonly IMapper _mapper;

        public GenericService(
            IRepository<E> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;   
        }

        public int Create<M>(M dataTransferObj)
        {
            var entity = _mapper.Map<E>(dataTransferObj);
            return _repository.Create(entity);
        }

        public bool Exists(int id) => _repository.Exists(id);

        public virtual IEnumerable<M> GetAll<M>(bool asNoTracking = true) => _mapper.Map<IEnumerable<M>>(_repository.GetAll(asNoTracking));

        public virtual M GetById<M>(int id) => _mapper.Map<M>(_repository.GetById(id));

        public virtual void Update<M>(M dataTransferObj) => _repository.Update(_mapper.Map<E>(dataTransferObj));

        public virtual async Task<int> CreateAsync<M>(M dataTransferObj) => await _repository.CreateAsync(_mapper.Map<E>(dataTransferObj));
    }
}