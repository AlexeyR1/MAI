using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Models.Abstract;
using System.Text.Json.Serialization;

namespace TrackingTasksProgressSystem.Models
{
    public class Department : IEntity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        [JsonIgnore]
        public List<Position> Positions { get; private set; }


        public Department(string name)
        {
            Name = name;
        }


        /// <summary>
        /// Используется только для заполнения таблицы начальными данными
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public Department(int id, string name) : this(name)
        {
            Id = id;
        }
    }
}
