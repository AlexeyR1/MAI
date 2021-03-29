using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.DTO.Abstract;
using TrackingTasksProgressSystem.Models;

namespace TrackingTasksProgressSystem.DTO
{
    public class AttachmentDTO : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public DateTime CreatedAt { get; set; }


        //public Attachment FromDto()
        //{
        //    return new Attachment(Id, Name, Data, CreatedAt);
        //}


        //public AttachmentDTO ToDto(Attachment attachment)
        //{
        //    return new AttachmentDTO
        //    {
        //        Id = attachment.Id,
        //        Name = attachment.Name,
        //        Data = attachment.Data,
        //        CreatedAt = attachment.CreatedAt
        //    };
        //}
    }
}
