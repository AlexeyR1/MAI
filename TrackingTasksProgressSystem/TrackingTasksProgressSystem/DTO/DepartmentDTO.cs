using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.DTO.Abstract;
using TrackingTasksProgressSystem.Models;

namespace TrackingTasksProgressSystem.DTO
{
    public class DepartmentDTO : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }


        //public Department FromDto()
        //{
        //    return new Department(Name);
        //}


        //public DepartmentDTO ToDto(Department department)
        //{
        //    return new DepartmentDTO
        //    {
        //        Id = department.Id,
        //        Name = department.Name
        //    };
        //}
    }
}
