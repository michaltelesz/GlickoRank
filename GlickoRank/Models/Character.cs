using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlickoRank.Models
{
    public class Character
    {
        public int ID { get; set; }
        public string CharacterId { get; set; }
        public string MembershipId { get; set; }
        public int MembershipType { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CharacterActivity> CharacterActivities { get; set; }
    }
}
