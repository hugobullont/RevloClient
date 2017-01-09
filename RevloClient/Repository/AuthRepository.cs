using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RevloClient.Repository
{
    public class AuthRepository
    {
        public bool Validate(string key)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://api.revlo.co/1/rewards?");
            
            request.Method = WebRequestMethods.Http.Get;
            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.Host = "api.revlo.co:443";
            request.Headers.Add("x-api-key", key);

            try
            {
                var httpResponse = (HttpWebResponse)request.GetResponse();
            }
            catch(Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
