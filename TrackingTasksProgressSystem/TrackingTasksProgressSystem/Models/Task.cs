using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Models.Intermediate;
using TrackingTasksProgressSystem.Models.Abstract;
using System.Text.Json.Serialization;

namespace TrackingTasksProgressSystem.Models
{
    public class Task : IEntity
    {
        public int Id { get; private set; }
        public string Summary { get; private set; }
        public int StatusId { get; private set; }
        public Status Status { get; private set; }
        public int AuthorId { get; private set; }
        public Employee Author { get; private set; }
        public int PerformingById { get; private set; }
        public Employee PerformingBy { get; private set; }
        public int PriorityId { get; private set; }
        public Priority Priority { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string ProblemAnnotation { get; private set; }
        public string ResponseAnnotation { get; private set; }
        public List<Attachment> ProblemAttachments { get; private set; }
        public List<Attachment> ResponseAttachments { get; private set; }

        #region Поля связи с промежуточной таблицей
        [JsonIgnore]
        public List<TasksProblemAttachments> TasksProblemAttachments { get; private set; }
        [JsonIgnore]
        public List<TasksResponseAttachments> TasksResponseAttachments { get; private set; }
        #endregion


        public Task(string summary,
                    Status status,
                    Employee author,
                    Employee performingBy,
                    Priority priority,
                    DateTime createdAt,
                    string problemAnnotation,
                    string responseAnnotation,
                    List<Attachment> problemAttachments,
                    List<Attachment> responseAttachments) : this(summary, createdAt, problemAnnotation, responseAnnotation)
        {
            StatusId = status.Id;
            Status = status;
            AuthorId = author.Id;
            Author = author;
            PerformingById = performingBy.Id;
            PerformingBy = performingBy;
            PriorityId = priority.Id;
            Priority = priority;
            ProblemAttachments = problemAttachments;
            ResponseAttachments = responseAttachments;
        }


        private Task(string summary,
                    DateTime createdAt,
                    string problemAnnotation,
                    string responseAnnotation)
        {
            Summary = summary;
            CreatedAt = createdAt;
            ProblemAnnotation = problemAnnotation;
            ResponseAnnotation = responseAnnotation;
        }
    }
}
