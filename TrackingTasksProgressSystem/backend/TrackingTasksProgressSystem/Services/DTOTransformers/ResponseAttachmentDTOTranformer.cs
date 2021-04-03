using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.DTO;

namespace TrackingTasksProgressSystem.Services.DTOTransformers
{
    public class ResponseAttachmentDTOTransformer : IDtoTranformer<ResponseAttachment, AttachmentDTO>
    {
        ResponseAttachment IDtoTranformer<ResponseAttachment, AttachmentDTO>.FromDto(AttachmentDTO dto)
        {
            return new ResponseAttachment(dto.Name, dto.Data);
        }


        AttachmentDTO IReadOnlyDtoTranformer<ResponseAttachment, AttachmentDTO>.ToDto(ResponseAttachment attachment)
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
