using TrackingTasksProgressSystem.DTO.Abstract;

namespace TrackingTasksProgressSystem.DTO.ReadOnly
{
    public class ShortEmployeeDTO : IDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
