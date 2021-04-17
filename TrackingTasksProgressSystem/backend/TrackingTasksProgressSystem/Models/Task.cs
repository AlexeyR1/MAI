using System;
using System.Collections.Generic;
using TrackingTasksProgressSystem.Models.Abstract;

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
        public DateTime UpdatedAt { get; private set; }
        public string ProblemDescription { get; private set; }
        public string ResponseDescription { get; private set; }
        public List<ProblemAttachment> ProblemAttachments { get; private set; }
        public List<ResponseAttachment> ResponseAttachments { get; private set; }


        public Task(string summary,
                    Status status,
                    Employee author,
                    Employee performingBy,
                    Priority priority,
                    DateTime createdAt,
                    string problemDescription,
                    string responseDescription,
                    List<ProblemAttachment> problemAttachments,
                    List<ResponseAttachment> responseAttachments) : this(summary, createdAt, problemDescription, responseDescription)
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
                     DateTime updatedAt,
                     string problemDescription,
                     string responseDescription)
        {
            Summary = summary;
            UpdatedAt = updatedAt;
            ProblemDescription = problemDescription;
            ResponseDescription = responseDescription;
        }
    }
}
