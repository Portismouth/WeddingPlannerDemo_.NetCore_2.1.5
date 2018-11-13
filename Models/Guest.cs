namespace WeddingPlanner.Models
{
    public class Guest
    {
        public int GuestId { get; set; }
        public int AttendingId { get; set; }
        public User Attending { get; set; }
        public int WeddingId { get; set; }
        public Wedding Wedding { get; set; }
    }
}