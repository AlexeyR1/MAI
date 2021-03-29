﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Models.Abstract;
using TrackingTasksProgressSystem.DTO.Abstract;

namespace TrackingTasksProgressSystem.Services.DTOTransformers.Abstract
{
    public interface IReadOnlyDtoTranformerService<TEntity, TDto>
        where TEntity : IEntity
        where TDto : IDto
    {
        TDto ToDto(TEntity entity);
    }
}