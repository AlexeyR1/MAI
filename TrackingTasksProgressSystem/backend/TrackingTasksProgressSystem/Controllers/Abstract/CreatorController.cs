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
    public abstract class CreatorController<TEntity, TDto> : BaseCreatorController<TEntity, TDto, IRepositoryCreator<TEntity>>
        where TEntity : IEntity
        where TDto : IDto
    {
        public CreatorController(IRepositoryCreator<TEntity> repository, IDtoTranformer<TEntity, TDto> dtoTransformer) : base(repository, dtoTransformer) { }
    }
}
