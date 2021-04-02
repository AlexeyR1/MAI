using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.DTO.Abstract;
using TrackingTasksProgressSystem.DTO.ReadOnly;
using TrackingTasksProgressSystem.Models;

namespace TrackingTasksProgressSystem.DTO
{
    public class TaskDTO : IDto
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public StatusDTO Status { get; set; }
        public ShortEmployeeDTO Author { get; set; }
        public ShortEmployeeDTO PerformingBy { get; set; }
        public PriorityDTO Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ProblemAnnotation { get; set; }
        public string ResponseAnnotation { get; set; }
        public List<AttachmentDTO> ProblemAttachments { get; set; }
        public List<AttachmentDTO> ResponseAttachments { get; set; }


        //public Models.Task FromDto()
        //{
        //    List<Attachment> problemAttachments = new List<Attachment>();
        //    ProblemAttachments.ForEach(attachment => problemAttachments.Add(attachment.FromDto()));

        //    List<Attachment> responseAttachments = new List<Attachment>();
        //    ResponseAttachments.ForEach(attachment => responseAttachments.Add(attachment.FromDto()));

        //    return new Models.Task(Id,
        //                           Summary,
        //                           Status.FromDto(),
        //                           Author.FromDto(),
        //                           PerformingBy.FromDto(),
        //                           Priority.FromDto(),
        //                           CreatedAt,
        //                           ProblemAnnotation,
        //                           ResponseAnnotation,
        //                           problemAttachments,
        //                           responseAttachments);
        //}


        //public TaskDTO ToDto(Models.Task task)
        //{
        //    ProblemAttachments = new List<AttachmentDTO>();
        //    task.ProblemAttachments.ForEach(attachment => ProblemAttachments.Add(new AttachmentDTO().ToDto(attachment)));

        //    ResponseAttachments = new List<AttachmentDTO>();
        //    task.ResponseAttachments.ForEach(attachment => ResponseAttachments.Add(new AttachmentDTO().ToDto(attachment)));

        //    return new TaskDTO
        //    {
        //        Id = task.Id,
        //        Summary = task.Summary,
        //        Status = new StatusDTO().ToDto(task.Status),
        //        Author = new EmployeeDTO().ToDto(task.Author),
        //        PerformingBy = new EmployeeDTO().ToDto(task.PerformingBy),
        //        Priority = new PriorityDTO().ToDto(task.Priority),
        //        CreatedAt = task.CreatedAt,
        //        ProblemAnnotation = task.ProblemAnnotation,
        //        ResponseAnnotation = task.ResponseAnnotation
        //    };
        //}
    }
}
