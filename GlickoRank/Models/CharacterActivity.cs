using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlickoRank.Models
{
    public class CharacterActivity
    {
        public virtual Character Character { get; set; }
        public int CharacterID { get; set; }

        public virtual Activity Activity { get; set; }
        public int ActivityID { get; set; }

        public bool Completed { get; set; }
        public int Mode { get; set; }
        public int Standing { get; set; }
        public float Score { get; set; }
        public float TeamScore { get; set; }
    }
}
