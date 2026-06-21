using System.ComponentModel.DataAnnotations;

namespace apiProject.Domain.Entities
{
    public class UserUnit
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int UnitId { get; set; }
        public Unit Unit { get; set; } = null!;

        // اگر خواستید مشخص کنید کاربر در این Unit چه نقشی دارد
        public string? PositionTitle { get; set; }

        public bool IsMainUnit { get; set; }
    }
}