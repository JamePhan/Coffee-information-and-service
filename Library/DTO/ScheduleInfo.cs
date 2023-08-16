namespace Library.DTO
{
    public class ScheduleInfo
    {
        public int ScheduleId { get; set; }

        public EventInfo? Event { get; set; }

        public CustomerInfo? Customer { get; set; }

        public int? TicketCount { get; set; }
    }
}