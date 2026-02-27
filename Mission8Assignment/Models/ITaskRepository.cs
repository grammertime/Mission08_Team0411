using System.Linq;

namespace Mission08_Team0411.Models
{
    public interface ITaskRepository
    {
        IQueryable<TaskItem> Tasks { get; }
        IQueryable<Category> Categories { get; }

        public void AddTask(TaskItem task);
        public void UpdateTask(TaskItem task);
        public void DeleteTask(TaskItem task);
    }
}