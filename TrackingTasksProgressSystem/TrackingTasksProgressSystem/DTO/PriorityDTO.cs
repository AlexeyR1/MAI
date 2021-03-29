using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.DTO.Abstract;
using TrackingTasksProgressSystem.Models;

namespace TrackingTasksProgressSystem.DTO
{
    public class PriorityDTO : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }


        //public Priority FromDto()
        //{
        //    return new Priority(Id, Name);
        //}


        //public PriorityDTO ToDto(Priority priority)
        //{
        //    return new PriorityDTO
        //    {
        //        Id = priority.Id,
        //        Name = priority.Name,
        //    };
        //}
    }
}
