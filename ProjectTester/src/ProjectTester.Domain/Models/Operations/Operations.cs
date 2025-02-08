namespace ProjectTester.Domain.Models.Operations
{
    /// <summary>
    /// Represent the <see cref="Operations"/> information.
    /// </summary>
    public class Operations
    {
        public string? Operation { get; set; }
        public int UnitCost { get; set; }
        public int Quantity { get; set; }
    }
}