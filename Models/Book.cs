using System.ComponentModel.DataAnnotations;

namespace accountbook.Models
{
    public class Book {
        public int Id {
            get;
            set;
        }

        [Required]
        public string Name {
            get;
            set;
        }
    }
}