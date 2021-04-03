using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Models.Abstract;
using System.Text.Json.Serialization;

namespace TrackingTasksProgressSystem.Models
{
    public class Priority : IEntity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        [JsonIgnore]
        public List<Models.Task> Tasks { get; private set; }


        public Priority(string name)
        {
            Name = name;
        }


        /// <summary>
        /// Используется только для заполнения таблицы начальными данными
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public Priority(int id, string name) : this(name)
        {
            Id = id;
        }
    }
}
