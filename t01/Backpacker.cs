namespace Travelling
{
    public class Backpacker : Traveler
        {
            public Backpacker(string name) : base(name)
            {
            }

            public override string GetTravelSummary()
            {
             return $"Traveler: {name} [Backpacker] | Location: {location} | Route: {GetRoute()}";
            }
        }
}
