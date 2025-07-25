using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClipShare.Core.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        //Navigation
        
        public ICollection<Video> Videos { get; set; }
    }
}
