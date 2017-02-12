namespace MyCarsDb.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MyCarsDb.Data.Common;
    using MyCarsDb.Data.Models.Contracts;        

    public class UserLogin : IEntity
    {        
        public virtual int UserId { get; set; }
        
        [MaxLength(DataModelConstants.USER_LOGIN_LOGIN_PROVIDER_MAX_LENGTH)]
        public virtual string LoginProvider { get; set; }

        [MaxLength(DataModelConstants.USER_LOGIN_PROVIDER_KEY_MAX_LENGTH)]
        public virtual string ProviderKey { get; set; }               
    }
}
