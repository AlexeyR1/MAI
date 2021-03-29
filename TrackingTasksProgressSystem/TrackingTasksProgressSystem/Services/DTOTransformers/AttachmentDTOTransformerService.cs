using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.DTO;

namespace TrackingTasksProgressSystem.Services.DTOTransformers
{
    public class AttachmentDTOTransformerService : IDtoTranformerService<Attachment, AttachmentDTO>
    {
        Attachment IDtoTranformerService<Attachment, AttachmentDTO>.FromDto(AttachmentDTO dto)
        {
            return new Attachment(dto.Name, dto.Data, DateTime.Now);
        }


        AttachmentDTO IReadOnlyDtoTranformerService<Attachment, AttachmentDTO>.ToDto(Attachment attachment)
        {
            return new AttachmentDTO
            {
                Id = attachment.Id,
                Name = attachment.Name,
                Data = attachment.Data,
                CreatedAt = attachment.CreatedAt
            };
        }
    }
}
