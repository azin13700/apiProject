
namespace  apiProject.Domain.Entities
{
    public class WorkExperience
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string FromYear { get; set; }
        public string ToYear { get; set; }
             public bool IsDelete { get; set; }
        public string relationType { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
