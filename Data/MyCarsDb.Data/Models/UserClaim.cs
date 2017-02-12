namespace MyCarsDb.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MyCarsDb.Data.Common;
    using MyCarsDb.Data.Models.Contracts;    

    public class UserClaim : IEntity
    {        
        public int Id { get; set; }
        
        public int UserId { get; set; }

        [MaxLength(DataModelConstants.USER_CLAIM_CLAIM_TYPE_MAX_LENGTH)]
        public string ClaimType { get; set; }

        [MaxLength(DataModelConstants.USER_CLAIM_CLAIM_VALUE_MAX_LENGTH)]
        public string ClaimValue { get; set; }    
    }
}
