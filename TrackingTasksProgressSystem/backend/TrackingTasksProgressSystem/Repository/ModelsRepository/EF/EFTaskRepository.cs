using System.Linq;
using Microsoft.EntityFrameworkCore;
using Tasks = System.Threading.Tasks;
using System.Collections.Generic;
using TrackingTasksProgressSystem.Repository.EF;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.EFCore;

namespace TrackingTasksProgressSystem.Repository.ModelsRepository.EF
{
    public class EFTaskRepository : EFRepositoryDeleter<Task>
    {
        public EFTaskRepository(TrackingTasksProgressDbContext dbContext) : base(dbContext) { }


        public override async Tasks.Task<int> AddAsync(Task task)
        {
            if (task is null) return 0;

            AddAttachmentsCollection(task);
            dbContext.Add(task);

            return await SaveAsync();
        }


        public override IEnumerable<Task> GetAll()
        {
            return dbContext.Set<Task>()
                .Include(task => task.Status)
                .Include(task => task.Author)
                .Include(task => task.PerformingBy)
                .Include(task => task.Priority)
                .Include(task => task.ProblemAttachments)
                .Include(task => task.ResponseAttachments);
        }


        public async override Tasks.Task<int> UpdateAsync(int id, Task task)
        {
            if (task is null) return 0;

            UpdateAttachmentsCollection(id, task);
            dbContext.Set<Task>().Attach(task);
            dbContext.Entry(task).Property(item => item.Id).CurrentValue = id;
            dbContext.Set<Task>().Update(task);

            return await SaveAsync();
        }


        private void AddAttachmentsCollection(Task task)
        {
            // При добавлении задачи могут быть только новые прикрепления
            dbContext.Set<ProblemAttachment>().AddRange(task.ProblemAttachments);
            dbContext.Set<ResponseAttachment>().AddRange(task.ResponseAttachments);
        }


        private void UpdateAttachmentsCollection(int id, Task task)
        {
            // Удаление ранее добавленных прикреплений у задачи с номером id
            Task existingTask = GetById(id);
            dbContext.Set<ProblemAttachment>().RemoveRange(existingTask.ProblemAttachments);
            dbContext.Set<ResponseAttachment>().RemoveRange(existingTask.ResponseAttachments);

            // Добавление новых прикреплений к задаче с номером id
            dbContext.Set<ProblemAttachment>().UpdateRange(task.ProblemAttachments);
            dbContext.Set<ResponseAttachment>().UpdateRange(task.ResponseAttachments);

            dbContext.Entry(existingTask).State = EntityState.Detached;
        }
    }
}
