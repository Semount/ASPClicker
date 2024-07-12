﻿namespace ASPClicker.Models
{
    public class Modification
    {
        public string Id { get; set; } = string.Empty;
        public int Cost { get; set; }
        public string Description { get; set; } = string.Empty;
        public int DiscountPercentage { get; set; } = 0;
        public double ClickMultiplierPercentage { get; set; } = 0;
        public double CriticalChance { get; set; } = 0;
        public int Count { get; set; }
    }
}