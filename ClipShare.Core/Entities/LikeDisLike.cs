using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipShare.Core.Entities
{
    public class LikeDisLike
    {
        public LikeDisLike()
        {

        }
        public LikeDisLike(int appUserId, int videoId, bool liked)
        {
            AppuserId = appUserId;
            VideoID = videoId;
            Liked = liked;
        }

        //PK (AppuserId,VideoID)
        //FK= AppuserId and  FK=VideoID

        public int AppuserId { get; set; }
        public int VideoID { get; set; }
        public bool Liked { get; set; } 
        //Navigation
        public AppUser AppUser { get; set; }
        public Video Video { get; set; }

    }
}
