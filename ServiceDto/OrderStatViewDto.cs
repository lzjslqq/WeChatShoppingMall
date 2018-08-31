namespace ServiceDto
{
    public class OrderStatViewDto
    {
        public int NotPayCount { get; set; }
        public int NotDeliverCount { get; set; }
        public int DeliveredCount { get; set; }
        public int FinishedCount { get; set; }
    }
}