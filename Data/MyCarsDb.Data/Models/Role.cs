namespace MyCarsDb.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNet.Identity;

    using MyCarsDb.Data.Models.Contracts;    
    using MyCarsDb.Data.Common;

    public class Role : IRole<int>, IEntity
    {
        private ICollection<UserToRole> _usersToRoles;

        public Role()
        {
            _usersToRoles = new HashSet<UserToRole>();
        }

        public Role(string name) 
            : this()
        {
            this.Name = name;
        }
        
        public int Id { get; set; }

        [MaxLength(DataModelConstants.ROLE_NAME_MAX_LENGTH)]
        public string Name { get; set; }
        
        public virtual ICollection<UserToRole> UsersToRoles
        {
            get { return _usersToRoles; }
            set { _usersToRoles = value; }
        }
    }
}
