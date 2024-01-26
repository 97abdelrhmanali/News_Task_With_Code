using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.Core.Entities;

namespace News.Repository.Data.configrations
{
    internal class NewsConfigrations : IEntityTypeConfiguration<NewsEntity>
    {
        public void Configure(EntityTypeBuilder<NewsEntity> builder)
        {
            builder.ToTable("News");
            builder.Property(N => N.Title).IsRequired();
            
            builder.HasMany(N => N.Categories)
                .WithMany(C => C.News);

            builder.HasOne(N => N.Source)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
