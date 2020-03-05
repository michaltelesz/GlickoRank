using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlickoRank.Models
{
    public class Activity
    {
        public int ID { get; set; }
        public DateTime Period { get; set; }
        public string InstanceId { get; set; }

        public int Mode { get; set; }

        public virtual ICollection<CharacterActivity> CharacterActivities { get; set; }
    }
}
