using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevloClient.Entities.ExtraEntities;

namespace RevloClient.Entities
{
    public class Redemption
    {
        public int reward_id;
        public int redemption_id;
        public DateTime created_at;
        public bool refunded;
        public bool completed;
        public User_Input user_input;
        public String username;

                    
    }
}
