using System;
using System.Collections.Generic;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.DTO;
using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;
using TrackingTasksProgressSystem.Repository.Abstract;
using TrackingTasksProgressSystem.Repository.EF;
using TrackingTasksProgressSystem.EFCore;
using TrackingTasksProgressSystem.DTO.ReadOnly;

namespace TrackingTasksProgressSystem.Services.DTOTransformers
{
    public class TaskDTOTransformerService : IDtoTranformerService<Models.Task, TaskDTO>
    {
        private readonly IRepositoryBase<Status> statusRepository;
        private readonly IRepositoryBase<Employee> employeeRepository;
        private readonly IRepositoryBase<Priority> priorityRepository;
        private readonly AttachmentDTOTransformer<ProblemAttachment> problemAttachmentDtoTransformer;
        private readonly AttachmentDTOTransformer<ResponseAttachment> responseAttachmentDtoTransformer;
        private readonly IReadOnlyDtoTranformerService<Status, StatusDTO> statusDtoTransformer;
        private readonly IReadOnlyDtoTranformerService<Employee, ShortEmployeeDTO> employeeDtoTransformer;
        private readonly IReadOnlyDtoTranformerService<Priority, PriorityDTO> priorityDtoTransformer;


        public TaskDTOTransformerService(TrackingTasksProgressDbContext dbContext)
        {
            statusRepository = new EFRepositoryBase<Status>(dbContext);
            employeeRepository = new EFRepositoryBase<Employee>(dbContext);
            priorityRepository = new EFRepositoryBase<Priority>(dbContext);

            problemAttachmentDtoTransformer = new (new EFRepositoryBase<ProblemAttachment>(dbContext),
                                                   new ProblemAttachmentDTOTransformerService());

            responseAttachmentDtoTransformer = new (new EFRepositoryBase<ResponseAttachment>(dbContext),
                                                    new ResponseAttachmentDTOTransformerService());

            statusDtoTransformer = new StatusDTOTransformerService();
            employeeDtoTransformer = new ShortEmployeeDTOTransformerService();
            priorityDtoTransformer = new PriorityDTOTransformerService();
        }


        Models.Task IDtoTranformerService<Models.Task, TaskDTO>.FromDto(TaskDTO dto)
        {
            List<ProblemAttachment> problemAttachments = problemAttachmentDtoTransformer.FromDto(dto.ProblemAttachments);
            List<ResponseAttachment> responseAttachments = responseAttachmentDtoTransformer.FromDto(dto.ResponseAttachments);

            // Новые статус, автор, приоритет и т.д. не могут прийти, т.к. при создании задачи они выбираются из существующего списка.
            // Новым может быть только прикрепление к задаче и то ТОЛЬКО в случае, если у него id == default, то есть при создании задачи.
            return new Models.Task(dto.Summary,
                                   statusRepository.GetById(dto.Status.Id),
                                   employeeRepository.GetById(dto.Author.Id),
                                   employeeRepository.GetById(dto.PerformingBy.Id),
                                   priorityRepository.GetById(dto.Priority.Id),
                                   DateTime.Now,
                                   dto.ProblemAnnotation,
                                   dto.ResponseAnnotation,
                                   problemAttachments,
                                   responseAttachments);
        }


        TaskDTO IReadOnlyDtoTranformerService<Models.Task, TaskDTO>.ToDto(Models.Task task)
        {
            List<AttachmentDTO> problemAttachments = problemAttachmentDtoTransformer.ToDto(task.ProblemAttachments);
            List<AttachmentDTO> responseAttachments = responseAttachmentDtoTransformer.ToDto(task.ResponseAttachments);

            return new TaskDTO
            {
                Id = task.Id,
                Summary = task.Summary,
                Status = statusDtoTransformer.ToDto(task.Status),
                Author = employeeDtoTransformer.ToDto(task.Author),
                PerformingBy = employeeDtoTransformer.ToDto(task.PerformingBy),
                Priority = priorityDtoTransformer.ToDto(task.Priority),
                UpdatedAt = task.UpdatedAt,
                ProblemAnnotation = task.ProblemAnnotation,
                ResponseAnnotation = task.ResponseAnnotation,
                ProblemAttachments = problemAttachments,
                ResponseAttachments = responseAttachments
            };
        }
    }
}
