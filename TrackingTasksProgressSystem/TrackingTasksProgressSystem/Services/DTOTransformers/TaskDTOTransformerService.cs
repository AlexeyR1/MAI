using System;
using System.Collections.Generic;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.DTO;
using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;
using TrackingTasksProgressSystem.Repository.Abstract;
using TrackingTasksProgressSystem.Repository.EF;
using TrackingTasksProgressSystem.Repository.ModelsRepository.EF;
using TrackingTasksProgressSystem.EFCore;
using TrackingTasksProgressSystem.DTO.ReadOnly;

namespace TrackingTasksProgressSystem.Services.DTOTransformers
{
    public class TaskDTOTransformerService : IDtoTranformerService<Models.Task, TaskDTO>
    {
        private readonly IRepositoryBase<Status> statusRepository;
        private readonly IRepositoryBase<Employee> employeeRepository;
        private readonly IRepositoryBase<Priority> priorityRepository;
        private readonly IRepositoryBase<Attachment> attachmentRepository;
        private readonly IDtoTranformerService<Attachment, AttachmentDTO> attachmentDtoTransformer;
        private readonly IReadOnlyDtoTranformerService<Status, StatusDTO> statusDtoTransformer;
        private readonly IReadOnlyDtoTranformerService<Employee, ShortEmployeeDTO> employeeDtoTransformer;
        private readonly IReadOnlyDtoTranformerService<Priority, PriorityDTO> priorityDtoTransformer;


        public TaskDTOTransformerService(TrackingTasksProgressDbContext dbContext)
        {
            statusRepository = new EFRepositoryBase<Status>(dbContext);
            employeeRepository = new EFRepositoryBase<Employee>(dbContext);
            priorityRepository = new EFRepositoryBase<Priority>(dbContext);
            attachmentRepository = new EFRepositoryBase<Attachment>(dbContext);
            attachmentDtoTransformer = new AttachmentDTOTransformerService();
            statusDtoTransformer = new StatusDTOTransformerService();
            employeeDtoTransformer = new ShortEmployeeDTOTransformerService();
            priorityDtoTransformer = new PriorityDTOTransformerService();
        }


        Models.Task IDtoTranformerService<Models.Task, TaskDTO>.FromDto(TaskDTO dto)
        {
            List<Attachment> problemAttachments = TransformAttachmentsFromDto(dto.ProblemAttachments);
            List<Attachment> responseAttachments = TransformAttachmentsFromDto(dto.ResponseAttachments);

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
            List<AttachmentDTO> problemAttachments = TransformAttachmentsToDto(task.ProblemAttachments);
            List<AttachmentDTO> responseAttachments = TransformAttachmentsToDto(task.ResponseAttachments);

            return new TaskDTO
            {
                Id = task.Id,
                Summary = task.Summary,
                Status = statusDtoTransformer.ToDto(task.Status),
                Author = employeeDtoTransformer.ToDto(task.Author),
                PerformingBy = employeeDtoTransformer.ToDto(task.PerformingBy),
                Priority = priorityDtoTransformer.ToDto(task.Priority),
                CreatedAt = task.CreatedAt,
                ProblemAnnotation = task.ProblemAnnotation,
                ResponseAnnotation = task.ResponseAnnotation,
                ProblemAttachments = problemAttachments,
                ResponseAttachments = responseAttachments
            };
        }


        private List<Attachment> TransformAttachmentsFromDto(List<AttachmentDTO> sourceList)
        {
            List<Attachment> resultList = new List<Attachment>();

            if (sourceList is not null)
            {
                sourceList.ForEach(attachmentDto =>
                {
                    // При редактировании задачи уже существующие прикрепления должны доставаться из БД
                    if (attachmentDto.Id != default)
                    {
                        resultList.Add(attachmentRepository.GetById(attachmentDto.Id));
                    }
                    else resultList.Add(attachmentDtoTransformer.FromDto(attachmentDto));
                });
            }

            return resultList;
        }


        private List<AttachmentDTO> TransformAttachmentsToDto(List<Attachment> sourceList)
        {
            List<AttachmentDTO> resultList = new List<AttachmentDTO>();
            sourceList.ForEach(attachment => resultList.Add(attachmentDtoTransformer.ToDto(attachment)));

            return resultList;
        }
    }
}
