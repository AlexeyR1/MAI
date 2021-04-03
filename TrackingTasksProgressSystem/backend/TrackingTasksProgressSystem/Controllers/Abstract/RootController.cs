using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TrackingTasksProgressSystem.Repository.Abstract;
using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;
using TrackingTasksProgressSystem.Models.Abstract;
using TrackingTasksProgressSystem.DTO.Abstract;

namespace TrackingTasksProgressSystem.Controllers.Abstract
{
    public abstract class RootController<TEntity, TDto, TRepository, TDtoTransformer> : ControllerBase
        where TEntity : IEntity
        where TDto : IDto
        where TRepository : IRepositoryReader<TEntity>
        where TDtoTransformer : IReadOnlyDtoTranformer<TEntity, TDto>

    {
        private protected TRepository repository;
        private protected TDtoTransformer dtoTransformer;


        public RootController(TRepository repository, TDtoTransformer dtoTransformer)
        {
            this.repository = repository;
            this.dtoTransformer = dtoTransformer;
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
    }
}
