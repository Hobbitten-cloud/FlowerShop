using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Note()
        {

        }
        public Note(string title, string description, byte[] picture)
        {
            Title = title;
            Description = description;
            Picture = picture;
        }
    }
}
