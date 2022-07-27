namespace RealEstates.Models
{
    public class PropertyTag
    {
        public virtual Property Property { get; set; }

        public int PropertyId { get; set; }

        public virtual Tag Tag { get; set; }

        public int TagId { get; set; }
    }
}
