using Newtonsoft.Json;
using RevloClient.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace RevloClient.Repository
{
    public class RewardRepository
    {
        public RewardResponse GetAllRewards(String key)
        {
            RewardResponse rewards;
            String result;
            var request = (HttpWebRequest)WebRequest.Create("https://api.revlo.co/1/rewards?");

            request.Method = WebRequestMethods.Http.Get;
            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.Host = "api.revlo.co:443";
            request.Headers.Add("x-api-key", key);

            var httpResponse = (HttpWebResponse)request.GetResponse();
            var streamReader = new StreamReader(httpResponse.GetResponseStream());

            result = streamReader.ReadToEnd();
            rewards = JsonConvert.DeserializeObject<RewardResponse>(result);
            return rewards;
        } 
    }
}
