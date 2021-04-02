using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.DTO.Abstract;
using TrackingTasksProgressSystem.Models;

namespace TrackingTasksProgressSystem.DTO
{
    public class PositionDTO : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DepartmentDTO Department { get; set; }


        //public Position FromDto()
        //{
        //    return new Position(Name, Department.FromDto());
        //}


        //public PositionDTO ToDto(Position position)
        //{
        //    return new PositionDTO
        //    {
        //        Id = position.Id,
        //        Name = position.Name,
        //        Department = new DepartmentDTO().ToDto(position.Department)
        //    };
        //}
    }
}
