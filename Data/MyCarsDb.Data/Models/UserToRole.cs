namespace MyCarsDb.Data.Models
{
    using MyCarsDb.Data.Models.Contracts;

    public class UserToRole : IEntity
    {
        public int RoleId { get; set; }
        
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual Role Role { get; set; }
    }
}
