using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevloClient.Entities.ExtraEntities;
using RevloClient.Entities;

namespace RevloClient.Repository
{
    public class SongRepository
    {
        public List<Song> GetSongs(int songrequestRewardID, String key )
        {
            RevloRepository revloRepo = new RevloRepository();
            List<Redemption> songrequests = revloRepo.GetRedemptionsByRewardID(key, songrequestRewardID);
            List<Song> result = new List<Song>();

            foreach (Redemption temp in songrequests)
            {
                Song newsong = new Song();
                newsong.redemption_id = temp.redemption_id;
                newsong.username = temp.username;
                newsong.song = temp.user_input.song;
                result.Add(newsong);
            }

            return result;
        }
    }
}
