using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Models.Abstract;
using TrackingTasksProgressSystem.DTO.Abstract;

namespace TrackingTasksProgressSystem.Services.DTOTransformers.Abstract
{
    public interface IDtoTranformer<TEntity, TDto> : IReadOnlyDtoTranformer<TEntity, TDto>
        where TEntity : IEntity
        where TDto : IDto
    {
        TEntity FromDto(TDto dto);
    }
}
