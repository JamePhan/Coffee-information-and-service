namespace Library.DTO
{
    public class ScheduleInfo
    {
        public int ScheduleId { get; set; }

        public int? EventId { get; set; }

        public DateTime? Date { get; set; }

        public string? ImageUrl { get; set; }

        public int? CustomerId { get; set; }

        public string? Name { get; set; }

        public int? TicketCount { get; set; }
    }
}