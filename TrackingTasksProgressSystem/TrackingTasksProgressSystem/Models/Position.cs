using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Models.Abstract;
using System.Text.Json.Serialization;

namespace TrackingTasksProgressSystem.Models
{
    public class Position : IEntity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int DepartmentId { get; private set; }
        public Department Department { get; private set; }
        [JsonIgnore]
        public List<Employee> Employees { get; private set; }


        public Position(string name, Department department) : this(name)
        {
            Name = name;
            DepartmentId = department.Id;
            Department = department;
        }


        private Position(string name)
        {
            Name = name;
        }
    }
}
