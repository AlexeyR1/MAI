using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.DTO;

namespace TrackingTasksProgressSystem.Services.DTOTransformers
{
    public class ProblemAttachmentDTOTransformer : IDtoTranformer<ProblemAttachment, AttachmentDTO>
    {
        ProblemAttachment IDtoTranformer<ProblemAttachment, AttachmentDTO>.FromDto(AttachmentDTO dto)
        {
            return new ProblemAttachment(dto.Name, dto.Data);
        }


        AttachmentDTO IReadOnlyDtoTranformer<ProblemAttachment, AttachmentDTO>.ToDto(ProblemAttachment attachment)
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
