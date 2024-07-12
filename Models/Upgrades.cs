﻿namespace ASPClicker.Models
{
    public class Upgrade
    {
        public string Id { get; set; } = string.Empty;
        public int Cost { get; set; }
        public int Increment { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Count { get; set; } = 0;
        public double CostMultiplier { get; set; } = 1.15; // Увеличение стоимости на 15% после каждой покупки

        public void IncreaseCost()
        {
            Cost = (int)(Cost * CostMultiplier);
        }
    }
}