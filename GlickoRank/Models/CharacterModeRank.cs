using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlickoRank.Models
{
    public class CharacterModeRank
    {
        public int ID { get; set; }

        public virtual Character Character { get; set; }
        public int CharacterID { get; set; }

        public int Mode { get; set; }

        public float Rating { get; set; }
        public float RD { get; set; }
        public float Volatility { get; set; }
    }
}
