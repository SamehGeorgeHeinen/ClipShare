using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipShare.Core.Entities
{
    public class LikeDisLike
    {
        //PK (AppuserId,VideoID)
        //FK= AppuserId and  FK=VideoID

        public int AppuserId { get; set; }
        public int VideoID { get; set; }
        public bool Liked { get; set; } = true;
        //Navigation
        public AppUser AppUser { get; set; }
        public Video Video { get; set; }

    }
}
