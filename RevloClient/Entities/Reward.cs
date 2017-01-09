using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevloClient.Entities
{
    public class Reward
    {
        public int reward_id { get; set; }
        public DateTime creaded_at { get; set; }
        public String title { get; set; }
        public String bot_command { get; set; }
        public bool enabled { get; set; }
        public int points { get; set; }
        public bool sub_only { get; set; }
        public String[] input_fields { get; set; }
        public String description { get; set; }
        public String img_url { get; set; }

    }
}
