using EduWork.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduWork.Data.Configurations
{
    internal class NonWorkingDayConfiguration : IEntityTypeConfiguration<NonWorkingDay>
    {
        public void Configure(EntityTypeBuilder<NonWorkingDay> builder)
        {
            builder
                .HasIndex(x => x.NonWorkingDate)
                .IsUnique();
            builder.HasData(
                new NonWorkingDay { Id = 1, Title = "Nova godina", NonWorkingDate = new DateOnly(2024, 1, 1) },
                new NonWorkingDay { Id = 2, Title = "Sveta tri kralja", NonWorkingDate = new DateOnly(2024, 6, 1) },
                new NonWorkingDay { Id = 3, Title = "Uskrs", NonWorkingDate = new DateOnly(2024, 3, 31) },
                new NonWorkingDay { Id = 4, Title = "Uskrsni ponedjeljak", NonWorkingDate = new DateOnly(2024, 4, 1) },
                new NonWorkingDay { Id = 5, Title = "Dan izbora", NonWorkingDate = new DateOnly(2024, 4, 17) },
                new NonWorkingDay { Id = 6, Title = "Praznik rada", NonWorkingDate = new DateOnly(2024, 5, 1) },
                new NonWorkingDay { Id = 7, Title = "Dan državnosti i Tijelovo", NonWorkingDate = new DateOnly(2024, 5, 30) },
                new NonWorkingDay { Id = 8, Title = "Dan antifašističke borbe", NonWorkingDate = new DateOnly(2024, 6, 22) },
                new NonWorkingDay { Id = 9, Title = "Dan pobjede i domovinske zahvalnosti", NonWorkingDate = new DateOnly(2024, 8, 5) },
                new NonWorkingDay { Id = 10, Title = "Velika Gospa", NonWorkingDate = new DateOnly(2024, 8, 15) },
                new NonWorkingDay { Id = 11, Title = "Dan svih svetih", NonWorkingDate = new DateOnly(2024, 11, 1) },
                new NonWorkingDay { Id = 12, Title = "Dan sjećanja na žrtve Domovinskog rata", NonWorkingDate = new DateOnly(2024, 11, 18) },
                new NonWorkingDay { Id = 13, Title = "Božić", NonWorkingDate = new DateOnly(2024, 12, 25) },
                new NonWorkingDay { Id = 14, Title = "Sveti Stjepan", NonWorkingDate = new DateOnly(2024, 12, 26) }
                );
            
        }
    }
}
