namespace Introspekt.DAL.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int CourseId { get; set; }

        public virtual Order Order { get; set; }

        public virtual Course Course { get; set; }
    }
}