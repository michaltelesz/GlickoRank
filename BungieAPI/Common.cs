using System;
using System.Collections.Generic;
using System.Text;

namespace BungieAPI
{
    public class Stat
    {
        public string statId { get; set; }
        public Basic basic { get; set; }
    }

    public class Basic
    {
        public float value { get; set; }
        public string displayValue { get; set; }
    }
}
