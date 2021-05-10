using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YueKeepAccountService.Models {
    public class Category {
        public int Id {
            get;
            set;
        }

        [Required]
        public string Name {
            get;
            set;
        }

        public string Icon {
            get;
            set;
        }

        public TransactionType Type {
            get;
            set;
        }

        public int? ParentId {
            get;
            set;
        }

        public Category Parent {
            get;
            set;
        }

        public List<Category> Children {
            get;
            set;
        }

        public int? BookId {
            get;
            set;
        }   

        public Book Book {
            get;
            set;
        }
    }
}