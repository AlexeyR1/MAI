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


        /// <summary>
        /// Используется только для заполнения таблицы начальными данными
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public Status(int id, string name) : this(name)
        {
            Id = id;
        }
    }
}
