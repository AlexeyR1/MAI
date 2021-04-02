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
        public DateTime UpdatedAt { get; set; }
        public string ProblemAnnotation { get; set; }
        public string ResponseAnnotation { get; set; }
        public List<AttachmentDTO> ProblemAttachments { get; set; }
        public List<AttachmentDTO> ResponseAttachments { get; set; }
    }
}
