using System.Collections.Generic;
using TrackingTasksProgressSystem.DTO;
using TrackingTasksProgressSystem.Models.Abstract;
using TrackingTasksProgressSystem.Repository.Abstract;
using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;

namespace TrackingTasksProgressSystem.Services.DTOTransformers
{
    public class AttachmentDTOTransformer<T> where T : BaseAttachment
    {
        public readonly IRepositoryBase<T> Repository;
        public readonly IDtoTranformer<T, AttachmentDTO> Transformer;


        public AttachmentDTOTransformer(IRepositoryBase<T> repository, IDtoTranformer<T, AttachmentDTO> dtoTransformer)
        {
            Repository = repository;
            Transformer = dtoTransformer;
        }


        public List<T> FromDto(List<AttachmentDTO> sourceList)
        {
            List<T> resultList = new List<T>();

            if (sourceList is not null)
            {
                foreach (var attachmentDto in sourceList)
                {
                    // При редактировании задачи уже существующие прикрепления должны доставаться из БД
                    if (attachmentDto.Id != default)
                    {
                        resultList.Add(Repository.GetById(attachmentDto.Id));
                    }
                    else resultList.Add(Transformer.FromDto(attachmentDto));
                }
            }

            return resultList;
        }


        public List<AttachmentDTO> ToDto(List<T> sourceList)
        {
            List<AttachmentDTO> resultList = new List<AttachmentDTO>();
            sourceList.ForEach(attachment => resultList.Add(Transformer.ToDto(attachment)));

            return resultList;
        }
    }
}
