using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Models.Abstract;
using System.Text.Json.Serialization;

namespace TrackingTasksProgressSystem.Models
{
    public class Employee : IEntity
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int? PositionId { get; private set; }
        public Position Position { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        [JsonIgnore]
        public List<Models.Task> AuthorBy { get; private set; }
        [JsonIgnore]
        public List<Models.Task> PerformingTasks { get; private set; }


        public Employee(string firstName,
                        string lastName,
                        Position position,
                        string email,
                        string password) : this(firstName, lastName, email, password)
        {
            PositionId = position.Id;
            Position = position;
        }


        private Employee(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }
    }
}
