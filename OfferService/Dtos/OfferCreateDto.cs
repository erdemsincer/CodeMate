namespace OfferService.Dtos
{
    public class OfferCreateDto
    {
        public Guid CourseId { get; set; }
        public decimal OfferedPrice { get; set; }
    }
}
