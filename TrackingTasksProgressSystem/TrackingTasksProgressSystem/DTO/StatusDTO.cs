﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.DTO.Abstract;
using TrackingTasksProgressSystem.Models;

namespace TrackingTasksProgressSystem.DTO
{
    public class StatusDTO : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }


        //public Status FromDto()
        //{
        //    return new Status(Id, Name);
        //}


        //public StatusDTO ToDto(Status status)
        //{
        //    return new StatusDTO
        //    {
        //        Id = status.Id,
        //        Name = status.Name,
        //    };
        //}
    }
}
