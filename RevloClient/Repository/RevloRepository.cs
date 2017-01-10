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
    public class RevloRepository
    {
        public RewardResponse GetAllRewards(String key)
        {
            RewardResponse rewards;
            String result;
            var request = (HttpWebRequest)WebRequest.Create("http://api.revlo.co/1/rewards?");

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

        public RedemptionResponse GetAllRedemptions(String key)
        {
            RedemptionResponse redemptions;
            String result;

            var request = (HttpWebRequest)WebRequest.Create("http://api.revlo.co/1/redemptions?page=1&refunded=false");

            request.Method = WebRequestMethods.Http.Get;
            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.Host = "api.revlo.co:443";
            request.Headers.Add("x-api-key", key);

            var httpResponse = (HttpWebResponse)request.GetResponse();
            var streamReader = new StreamReader(httpResponse.GetResponseStream());

            result = streamReader.ReadToEnd();
            redemptions = JsonConvert.DeserializeObject<RedemptionResponse>(result);
            return redemptions;

        }

        public List<Redemption> GetRedemptionsByRewardID(String key, int id)
        {
            RedemptionResponse redemptions = GetAllRedemptions(key);
            List<Redemption> result = new List<Redemption>();
            List<Redemption> allRedemptions = redemptions.redemptions.ToList();

            foreach(Redemption temp in allRedemptions)
            {
                if(temp.reward_id == id && temp.completed==false)
                {
                    result.Add(temp);
                }
            }
            return result;
        }

        public void SetRedemptionCompleted(String key, int redId)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://api.revlo.co/1/redemptions/"+redId);
            request.Method = "PATCH";
            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.Host = "api.revlo.co:443";
            request.Headers.Add("x-api-key", key);

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = "{ \"completed\": \"true\" }";

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
            }
        }
    }
}
