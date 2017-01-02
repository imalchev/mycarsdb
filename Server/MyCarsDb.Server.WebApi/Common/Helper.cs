using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MyCarsDb.Server.WebApi.Common
{
    public static class Helper 
	
    {
	
       private const string Salt = "1$#@";
	
	
       public static int  DecodeId(string urlId)
       {
           var base64EncodedBytes = Convert.FromBase64String(urlId);
	
           var bytesAsString = Encoding.UTF8.GetString(base64EncodedBytes);
	
           bytesAsString = bytesAsString.Replace(Salt, string.Empty);
	
           return int.Parse(bytesAsString);
       }

       public static string EncodeId(int id)	
       {
           var plainTextBytes = Encoding.UTF8.GetBytes(id + Salt);
	        
           return Convert.ToBase64String(plainTextBytes);
       }
    }
}