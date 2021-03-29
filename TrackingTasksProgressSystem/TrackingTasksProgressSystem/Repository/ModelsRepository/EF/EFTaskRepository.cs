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

            // При добавлении задачи могут быть только новые прикрепления
            dbContext.Set<Attachment>().AddRange(task.ProblemAttachments);
            dbContext.Set<Attachment>().AddRange(task.ResponseAttachments);

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
                .ThenInclude(attachment => attachment.TasksProblemAttachments)
                .Include(task => task.ResponseAttachments)
                .ThenInclude(attachment => attachment.TasksResponseAttachments);
        }


        public async override Tasks.Task<int> UpdateAsync(int id, Task task)
        {
            if (task is null) return 0;

            dbContext.Set<Attachment>().UpdateRange(task.ProblemAttachments);
            dbContext.Set<Attachment>().UpdateRange(task.ResponseAttachments);

            dbContext.Set<Task>().Attach(task);
            dbContext.Entry(task).Property(item => item.Id).CurrentValue = id;
            dbContext.Set<Task>().Update(task);

            return await SaveAsync();
        }
    }
}
