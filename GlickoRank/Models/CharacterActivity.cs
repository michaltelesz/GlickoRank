using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlickoRank.Models
{
    public class CharacterActivity
    {
        public virtual Character Character { get; set; }
        public int CharacterId { get; set; }

        public virtual Activity Activity { get; set; }
        public int ActivityId { get; set; }
    }
}
