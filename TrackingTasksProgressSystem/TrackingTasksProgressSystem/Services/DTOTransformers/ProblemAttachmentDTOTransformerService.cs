using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.DTO;
using TrackingTasksProgressSystem.Models.Abstract;

namespace TrackingTasksProgressSystem.Services.DTOTransformers
{
    public class ProblemAttachmentDTOTransformerService : IDtoTranformerService<ProblemAttachment, AttachmentDTO>
    {
        ProblemAttachment IDtoTranformerService<ProblemAttachment, AttachmentDTO>.FromDto(AttachmentDTO dto)
        {
            return new ProblemAttachment(dto.Name, dto.Data);
        }


        AttachmentDTO IReadOnlyDtoTranformerService<ProblemAttachment, AttachmentDTO>.ToDto(ProblemAttachment attachment)
        {
            return new AttachmentDTO
            {
                Id = attachment.Id,
                Name = attachment.Name,
                Data = attachment.Data
            };
        }
    }
}
