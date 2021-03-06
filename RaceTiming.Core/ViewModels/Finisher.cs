﻿
using RedRat.RaceTiming.Core.Util;
using RedRat.RaceTiming.Data;

namespace RedRat.RaceTiming.Core.ViewModels
{
    public class Finisher
    {
        public int Position { get; set; }
        public string Name { get; set; }
        public GenderEnum Gender { get; set; }
        public int Number { get; set; }
        public double Time { get; set; }
        public string Category { get; set; }
        public AgeGroup.AgeGroupEnum CategoryEnum { get; set; }
        public string CategoryPosition { get; set; }
        public string Club { get; set; }
        public string Team { get; set; }
        public string Wma { get; set; }
    }
}
