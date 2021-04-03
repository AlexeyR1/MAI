using System.Collections.Generic;
using System.Text.Json.Serialization;
using TrackingTasksProgressSystem.Models.Abstract;

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


        /// <summary>
        /// Используется только для заполнения таблицы начальными данными
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="department"></param>
        public Position(int id, string name, int departmentId) : this(name)
        {
            Id = id;
            DepartmentId = departmentId;
        }


        private Position(string name)
        {
            Name = name;
        }
    }
}
