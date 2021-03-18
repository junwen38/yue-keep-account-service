using System;
using System.ComponentModel.DataAnnotations;

namespace accountbook.Models {
    public class Item {
        public int Id {
            get;
            set;
        }

        public DateTime Date {
            get;
            set;
        }

        public decimal Cash {
            get;
            set;
        }

        public TransactionType Type {
            get;
            set;
        }

        public int Category1Id {
            get;
            set;
        }

        public Category Category1 {
            get;
            set;
        }

        public int? Category2Id {
            get;
            set;
        }

        public Category Category2 {
            get;
            set;
        }

        public string Comment {
            get;
            set;
        }
    }
}