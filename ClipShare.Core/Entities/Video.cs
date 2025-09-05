using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipShare.Core.Entities
{
    public class Video : BaseEntity
    {
        [Required]
        public string ThumbnailUrl { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
       // public byte[] Contents { get; set; }
        public int CategoryId { get; set; }
        public int ChannelId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

      // public string ContentType { get; set; }


        // Navigations
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [ForeignKey("ChannelId")]
        public Channel Channel { get; set; }

       public VideoFile VideoFile { get; set; }

       public ICollection<Comment> Comments { get; set; }
       public ICollection<LikeDisLike> LikeDisLikes { get; set; }
        public ICollection<VideoView> Viewers { get; set; }
    }
}