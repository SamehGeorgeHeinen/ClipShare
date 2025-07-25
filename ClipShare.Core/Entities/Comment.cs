using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipShare.Core.Entities
{
    public class Comment
    {
        //PK (AppuserId,VideoID)
        //FK= AppuserId and  FK=VideoID
       
        public int AppuserId { get; set; }
        public int VideoID { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime PosyedAt { get; set; } = DateTime.UtcNow;
        //Navigation
        public AppUser  AppUser { get; set; }
        public Video Video { get; set; }

    }
}
