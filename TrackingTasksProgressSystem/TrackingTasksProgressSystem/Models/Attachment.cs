using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Models.Intermediate;
using TrackingTasksProgressSystem.Models.Abstract;
using System.Text.Json.Serialization;

namespace TrackingTasksProgressSystem.Models
{
    public class Attachment : IEntity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public byte[] Data { get; private set; }
        public DateTime CreatedAt { get; private set; }
        [JsonIgnore]
        public List<Models.Task> ToProblems { get; private set; }
        [JsonIgnore]
        public List<Models.Task> ToResponses { get; private set; }

        #region Поля связи с промежуточной таблицей
        [JsonIgnore]
        public List<TasksProblemAttachments> TasksProblemAttachments { get; private set; }
        [JsonIgnore]
        public List<TasksResponseAttachments> TasksResponseAttachments { get; private set; }
        #endregion


        public Attachment(string name, byte[] data, DateTime createdAt)
        {
            Name = name;
            Data = data;
            CreatedAt = createdAt;
        }
    }
}
