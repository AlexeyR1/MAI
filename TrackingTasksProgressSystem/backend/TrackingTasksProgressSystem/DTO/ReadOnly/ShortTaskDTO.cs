using TrackingTasksProgressSystem.DTO.Abstract;

namespace TrackingTasksProgressSystem.DTO.ReadOnly
{
    public class ShortTaskDTO : IDto
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public StatusDTO Status { get; set; }
        public ShortEmployeeDTO PerformingBy { get; set; }
    }
}
