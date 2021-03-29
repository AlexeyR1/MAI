using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.DTO.Abstract;
using TrackingTasksProgressSystem.Models;

namespace TrackingTasksProgressSystem.DTO.ReadOnly
{
    public class ShortTaskDTO : IDto
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public StatusDTO Status { get; set; }


        //public ShortTaskDTO ToDto(Models.Task task)
        //{
        //    return new ShortTaskDTO
        //    {
        //        Id = task.Id,
        //        Summary = task.Summary,
        //        Status = new StatusDTO().ToDto(task.Status)
        //    };
        //}
    }
}
