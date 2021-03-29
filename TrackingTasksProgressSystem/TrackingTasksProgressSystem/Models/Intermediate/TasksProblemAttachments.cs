using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Models.Abstract;

namespace TrackingTasksProgressSystem.Models.Intermediate
{
    public class TasksProblemAttachments : IEntity
    {
        public int Id { get; private set; }
        public int TaskId { get; private set; }
        public Models.Task Task { get; private set; }
        public int? AttachmentId { get; private set; }
        public Attachment Attachment { get; private set; }


        // Необходимо для механизма привязки EF Core
        private TasksProblemAttachments() { }
    }
}
