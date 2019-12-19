using System.Collections.Generic;

namespace Introspekt.DAL.Models
{
    public class Order
    {
        public int Id { get; set; }

        public long ChatId { get; set; }
        
        public int UserId { get; set; }

        public string Status { get; set; }

        public string Email { get; set; }

        public int Amount { get; set; }

        public string Date { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
