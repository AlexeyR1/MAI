using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.DTO.Abstract;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.Models.Abstract;

namespace TrackingTasksProgressSystem.DTO
{
    public class EmployeeDTO : IDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PositionDTO Position { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // Можно ли передавать на сервер Dto, содержащую пароль? (С сервера можно (при создании сотрудника))


        //public Employee FromDto()
        //{
        //    return new Employee(FirstName,
        //                        LastName,
        //                        Position.FromDto(),
        //                        Email,
        //                        Password);
        //}


        //public EmployeeDTO ToDto(Employee employee)
        //{
        //    return new EmployeeDTO
        //    {
        //        Id = employee.Id,
        //        FirstName = employee.FirstName,
        //        LastName = employee.LastName,
        //        Position = new PositionDTO().ToDto(employee.Position),
        //        Email = employee.Email,
        //        Password = employee.Password
        //    };
        //}
    }
}
