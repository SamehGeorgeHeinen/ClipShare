using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipShare.Core.Entities
{
   public  class AppUser : IdentityUser<int>
    {
        [Required]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //Navigation
        public Channel Channel { set; get; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Subscribe> Subscriptions { get; set; }
        public ICollection<LikeDisLike> LikeDisLikes { get; set; }


    }

}
