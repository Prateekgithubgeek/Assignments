using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Day_4
{
    public class Author
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; } // Link to Author
        public int PublishedYear { get; set; }
        public string Genre { get; set; }
    }
}
