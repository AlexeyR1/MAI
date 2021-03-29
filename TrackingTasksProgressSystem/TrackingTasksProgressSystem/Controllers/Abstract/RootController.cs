using System.Linq;
using System.Collections.Generic;
using Tasks = System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackingTasksProgressSystem.Repository.Abstract;
using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;
using TrackingTasksProgressSystem.Models.Abstract;
using TrackingTasksProgressSystem.DTO.Abstract;

namespace TrackingTasksProgressSystem.Controllers.Abstract
{
    public abstract class RootController<TEntity, TDto, TRepository> : ControllerBase
        where TEntity : IEntity
        where TDto : IDto
        where TRepository : IRepositoryBase<TEntity>
    {
        private protected TRepository repository;
        private protected IDtoTranformerService<TEntity, TDto> dtoTransformer;


        public RootController(TRepository repository, IDtoTranformerService<TEntity, TDto> dtoTransformer)
        {
            this.repository = repository;
            this.dtoTransformer = dtoTransformer;
        }


        [HttpPost]
        public virtual async Tasks.Task<IActionResult> Add([FromBody] TDto dto)
        {
            var entity = dtoTransformer.FromDto(dto);
            if (await repository.AddAsync(entity) == 0) return StatusCode(500);

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dtoTransformer.ToDto(entity));
        }


        [HttpGet("{id}")]
        public virtual IActionResult GetById(int id)
        {
            var entity = repository.GetById(id);
            if (entity is null) return NotFound();

            return Ok(dtoTransformer.ToDto(entity));
        }


        [HttpGet]
        public virtual IActionResult GetAll()
        {
            var collection = repository.GetAll();
            if (!collection.Any()) return NotFound();

            List<TDto> dtoCollection = new List<TDto>(); 
            foreach (var entity in collection)
            {
                dtoCollection.Add(dtoTransformer.ToDto(entity));
            }

            return Ok(dtoCollection);
        }


        [HttpPut("{id}")]
        public virtual async Tasks.Task<IActionResult> Update(int id, [FromBody] TDto dto)
        {
            var entity = dtoTransformer.FromDto(dto);
            if (await repository.UpdateAsync(id, entity) == 0) return StatusCode(500);

            return Ok(dtoTransformer.ToDto(entity));
        }
    }
}
