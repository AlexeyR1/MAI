using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingTasksProgressSystem.Models.Abstract
{
    public abstract class BaseAttachment : IEntity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public byte[] Data { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public int TaskId { get; private set; }
        public Models.Task Task { get; private set; }


        public BaseAttachment(string name, byte[] data, DateTime createdAt)
        {
            Name = name;
            Data = data;
            CreatedAt = createdAt;
        }
    }
}
