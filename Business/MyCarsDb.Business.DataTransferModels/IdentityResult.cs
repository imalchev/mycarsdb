namespace MyCarsDb.Business.DataTransferModels
{
    using System.Collections.Generic;

    public class IdentityResult
    {
        public ICollection<string> Errors { get; set; }
        public bool Succeeded { get; set; }
    }
}
