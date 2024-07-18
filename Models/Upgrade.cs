namespace ASPClicker.Models
{
    public class Upgrade
    {
        public string Id { get; set; } = string.Empty;
        public double Cost { get; set; }
        public double Increment { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Count { get; set; } = 0;

        public void IncreaseCost()
        {
            Cost *= 1.15;
        }
    }
}