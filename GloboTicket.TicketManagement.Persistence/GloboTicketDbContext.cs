using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GloboTicket.TicketManagement.Domain.Common;
using GloboTicket.TicketManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GloboTicket.TicketManagement.Persistence
{
    public class GloboTicketDbContext : DbContext
    {
        public GloboTicketDbContext(DbContextOptions<GloboTicketDbContext> options) : base(options)
        {
            
        }

        public DbSet<Event> Events { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GloboTicketDbContext).Assembly);



            var concertGuid = Guid.Parse("{7d6981b4-e35e-4921-90d0-6da79f2e6664}");
            var musicalGuid = Guid.Parse("{e4a187b7-f7f7-4c38-908f-e5c34eedf63d}");
            var playGuid = Guid.Parse("{7eaf1171-58dc-474f-ac70-ca604c764bdd}");
            var conferenceGuid = Guid.Parse("{ba1f3083-8409-4bc9-9de5-3d20952519cb}");

            modelBuilder.Entity<Category>().HasData(new Category {CategoryId = concertGuid, Name = "Concerts"});
            modelBuilder.Entity<Category>().HasData(new Category {CategoryId = musicalGuid, Name = "Musicals"});
            modelBuilder.Entity<Category>().HasData(new Category {CategoryId = playGuid, Name = "Plays"});
            modelBuilder.Entity<Category>().HasData(new Category {CategoryId = conferenceGuid, Name = "Conferences"});



            modelBuilder.Entity<Event>().HasData(new Event
            {
                EventId = Guid.Parse("{41038fae-68f6-4230-b7b4-587db0265fec}"),
                Name = "John Egbert Live",
                Price = 65,
                Artist = "John Egbert",
                Date = DateTime.Now.AddMonths(6),
                Description = "Join John for his farwell tour across 15 continents. John really needs no introduction since he has ...",
                ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/banjo.jpg",
                CategoryId = concertGuid
        });

            modelBuilder.Entity<Event>().HasData(new Event
            {
                EventId = Guid.Parse("{19d0fd95-0511-4cac-ab55-9260e162e19f}"),
                Name = "Deneme 1",
                Price = 85,
                Artist = "Deneme 1",
                Date = DateTime.Now.AddMonths(9),
                Description = "Deneme 1 for his farwell tour across 15 continents. John really needs no introduction since he has ...",
                ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/michael.jpg",
                CategoryId = playGuid
            });


            modelBuilder.Entity<Event>().HasData(new Event
            {
                EventId = Guid.Parse("{d94b6f13-da2a-466d-a174-90a74aa8f609}"),
                Name = "Deneme 2",
                Price = 265,
                Artist = "Deneme 2",
                Date = DateTime.Now.AddMonths(4),
                Description = "Deneme 2 for his farwell tour across 15 continents. John really needs no introduction since he has ...",
                ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/dj.jpg",
                CategoryId = musicalGuid
            });

            modelBuilder.Entity<Event>().HasData(new Event
            {
                EventId = Guid.Parse("{6e095756-0c65-4118-91c2-26ba81cc80a3}"),
                Name = "Deneme 3",
                Price = 165,
                Artist = "Deneme 3",
                Date = DateTime.Now.AddMonths(6),
                Description = "Deneme 3 for his farwell tour across 15 continents. John really needs no introduction since he has ...",
                ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/banjo.jpg",
                CategoryId = conferenceGuid
            });

            modelBuilder.Entity<Order>().HasData(new Order
            {
                Id = Guid.Parse("60d61a22-44ad-4f35-8d6c-6a63d6dc6e46"), OrderTotal = 135, OrderPaid = true,
                OrderPlaced = DateTime.Now, UserId = Guid.Parse("{72f223e5-d1e8-4295-85da-4df5bb39a0f5}")
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate=DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
