using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlickoRank.Models
{
    public class CharacterActivityModeRank
    {
        public int ID { get; set; }

        public virtual CharacterActivity CharacterActivity { get; set; }
        public int CharacterActivityID { get; set; }

        public int Mode { get; set; }

        public float Rating { get; set; }
        public float RD { get; set; }
        public float Volatility { get; set; }
    }
}
