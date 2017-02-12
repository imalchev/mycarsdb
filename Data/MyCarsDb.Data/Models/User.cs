namespace MyCarsDb.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using System;
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity;

    using MyCarsDb.Data.Common;
    using MyCarsDb.Data.Models.Contracts;

    public class User : IUser<int>, IEntity
    {
        private ICollection<UserToRole> _usersToRoles;
        private ICollection<UserLogin> _logins;
        private ICollection<UserClaim> _claims;
        private ICollection<UserToVehicle> _usersToVehicles;

        public User()
        {
            _usersToRoles = new HashSet<UserToRole>();
            _logins = new HashSet<UserLogin>();
            _claims = new HashSet<UserClaim>();
            _usersToVehicles = new HashSet<UserToVehicle>();
        }

        public int Id { get; set; }

        [MaxLength(DataModelConstants.USER_EMAIL_MAX_LENGTH)]
        public string UserName { get; set; }

        [MaxLength(DataModelConstants.USER_EMAIL_MAX_LENGTH)]
        public string Email { get; set; }

        public int AccessFailedCount { get; set; }               
                       
        public bool EmailConfirmed { get; set; }
        
        public bool LockoutEnabled { get; set; }
        
        public DateTime? LockoutEndDateUtc { get; set; }

        [MaxLength(DataModelConstants.USER_PASSWORD_HASH_MAX_LENGTH)]
        public string PasswordHash { get; set; }

        [MaxLength(DataModelConstants.USER_PHONE_NUMBER_MAX_LENGTH)]
        public string PhoneNumber { get; set; }
        
        public bool PhoneNumberConfirmed { get; set; }

        [MaxLength(DataModelConstants.USER_SECURITY_STAMP_MAX_LENGTH)]
        public string SecurityStamp { get; set; }
        
        public bool TwoFactorEnabled { get; set; }               

        [MaxLength(DataModelConstants.USER_NAME_MAX_LENGTH)]
        public string Name { get; set; }

        public virtual ICollection<UserToRole> UsersToRoles
        {
            get { return _usersToRoles; }
            set { _usersToRoles = value; }
        }

        public virtual ICollection<UserLogin> Logins
        {
            get { return _logins; }
            set { _logins = value; }
        }

        public virtual ICollection<UserClaim> Claims
        {
            get { return _claims; }
            set { _claims = value; }
        }

        public virtual ICollection<UserToVehicle> UsersToVehicles
        {
            get { return _usersToVehicles; }
            set { _usersToVehicles = value; }
        }
    }
}
