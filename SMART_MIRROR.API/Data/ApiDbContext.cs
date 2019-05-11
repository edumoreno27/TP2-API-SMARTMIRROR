using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SMART_MIRROR.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMART_MIRROR.API.Data
{
    public class ApiDbContext : IdentityDbContext<User>
    {

        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }

        public DbSet<Gadget> Gadgets { get; set; }
        public DbSet<UserGadgets> UserGadgets { get; set; }
        public DbSet<BooleanTable> BooleanTables { get; set; }
        public DbSet<DiaryGoogle> DiaryGoogles { get; set; }
        public DbSet<EmailInformation> EmailInformations { get; set; }
        public DbSet<DiaryInformation> DiaryInformations { get; set; }
        public DbSet<HotelService> HotelServices { get; set; }
        public DbSet<HotelServiceInformation> HotelServiceInformations { get; set; }
        public DbSet<MusicAction> MusicActions { get; set; }

        public DbSet<MusicNoUser> MusicNoUserActions { get; set; }
        public DbSet<HotelServiceNoUser> HotelServiceNoUsersActions { get; set; }

        public DbSet<NewsInformation> NewsInformationAction { get; set; }
        public DbSet<NewsInformationNoUser> NewsInformationNoUserAction { get; set; }
    }
}
