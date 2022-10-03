using System.ComponentModel.DataAnnotations;

namespace Lab4.Models
{
    public class Book
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int Price { get; set; }
        public int YearPublished { get; set; }
    }
}
