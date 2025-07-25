using ClipShare.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipShare.DataAccess.Data.Config
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            //define primary key AppUserId and VideoId
            builder.HasKey(c => new { c.AppuserId, c.VideoID });
            builder.HasOne(a => a.AppUser).WithMany(c => c.Comments).HasForeignKey(c => c.AppuserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(a => a.Video).WithMany(c => c.Comments).HasForeignKey(c => c.VideoID).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
