using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.DTO;

namespace TrackingTasksProgressSystem.Services.DTOTransformers
{
    public class ResponseAttachmentDTOTransformerService : IDtoTranformerService<ResponseAttachment, AttachmentDTO>
    {
        ResponseAttachment IDtoTranformerService<ResponseAttachment, AttachmentDTO>.FromDto(AttachmentDTO dto)
        {
            return new ResponseAttachment(dto.Name, dto.Data);
        }


        AttachmentDTO IReadOnlyDtoTranformerService<ResponseAttachment, AttachmentDTO>.ToDto(ResponseAttachment attachment)
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
