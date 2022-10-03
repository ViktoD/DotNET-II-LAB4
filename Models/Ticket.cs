using System.ComponentModel.DataAnnotations;

namespace Lab4.Models
{
    public class Ticket
    {
        [Key]
        public int ID { get; set; }
        public int ReaderID { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public Reader Reader { get; set; } = null!;

    }
}
