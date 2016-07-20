using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ElevenNote.Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ElevenNoteUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ElevenNoteUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ElevenNoteDBContext : IdentityDbContext<ElevenNoteUser>
    {
        public ElevenNoteDBContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ElevenNoteDBContext Create()
        {
            return new ElevenNoteDBContext();
        }

        //this gives a collection of all notes in database and db connection string
        public DbSet<NoteEntity> Notes { get; set; }


        //tries as part of the entity framework to create and maintain the database
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations
                .Add(new IdentityUserLoginConfiguration())
                .Add(new IdentityUserLoginConfiguration());
        }
    }

    public class IdentityUserLoginConfiguration
        :EntityTypeConfiguration<IdentityUserLogin>
    {
        public IdentityUserLoginConfiguration()
        {
            HasKey(iur => iur.UserId);
        }
    }

    public class IdentityUserRoleConfiguration
        :EntityTypeConfiguration<IdentityUserRole>
    {
        public IdentityUserRoleConfiguration()
        {
            HasKey(iur => iur.RoleId);
        }
    }
}