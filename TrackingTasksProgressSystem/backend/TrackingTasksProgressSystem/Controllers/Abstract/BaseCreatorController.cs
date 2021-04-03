using System;
using System.Collections.Generic;
using System.Linq;
using Tasks = System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackingTasksProgressSystem.DTO.Abstract;
using TrackingTasksProgressSystem.Models.Abstract;
using TrackingTasksProgressSystem.Repository.Abstract;
using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;

namespace TrackingTasksProgressSystem.Controllers.Abstract
{
    public abstract class BaseCreatorController<TEntity, TDto, TRepository> : RootController<TEntity, TDto, TRepository, IDtoTranformer<TEntity, TDto>>

        where TEntity : IEntity
        where TDto : IDto
        where TRepository : IRepositoryCreator<TEntity>
    {
        public BaseCreatorController(TRepository repository, IDtoTranformer<TEntity, TDto> dtoTransformer) : base(repository, dtoTransformer)
        { }


        [HttpPost]
        public virtual async Tasks.Task<IActionResult> Add([FromBody] TDto dto)
        {
            var entity = dtoTransformer.FromDto(dto);
            if (await repository.AddAsync(entity) == 0) return StatusCode(500);

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dtoTransformer.ToDto(entity));
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
