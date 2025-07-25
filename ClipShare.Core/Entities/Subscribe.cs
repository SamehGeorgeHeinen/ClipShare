using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipShare.Core.Entities
{
    public class Subscribe
    {
        //PK (AppuserId,ChannelID)
        //FK= AppuserId and  FK=ChannelID

        public int AppuserId { get; set; }
        public int ChannelID { get; set; }
        //Navigation
        public AppUser AppUser { get; set; }
        public Channel Channel { get; set; }
    }
}
