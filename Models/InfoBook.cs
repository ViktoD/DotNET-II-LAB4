using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Lab4.Models
{
  
    public class InfoBook
    {
        [Key]
        public int ID { get; set; }
        public int BookID { get; set; }
        public int TicketID { get; set; }
        public DateTime DateTakeBook { get; set; }
        public DateTime? DateReturnBook { get; set; }
        public Ticket Ticket { get; set; } = null!;
        public Book Book { get; set; } = null!;

    }
}
