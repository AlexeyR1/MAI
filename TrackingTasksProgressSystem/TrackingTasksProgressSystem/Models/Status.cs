using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Models.Abstract;
using System.Text.Json.Serialization;

namespace TrackingTasksProgressSystem.Models
{
    public class Status : IEntity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        [JsonIgnore]
        public List<Models.Task> Tasks { get; private set; }


        public Status(string name)
        {
            Name = name;
        }
    }
}
