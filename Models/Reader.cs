using System.ComponentModel.DataAnnotations;

namespace Lab4.Models
{
    public class Reader
    {
        [Key]
        public int ID { get; set; }
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string PatronymicName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int Phone { get; set; } 
        public DateTime DateBirth { get; set; }

    }
}
