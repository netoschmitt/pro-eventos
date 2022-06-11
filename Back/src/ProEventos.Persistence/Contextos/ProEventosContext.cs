using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Domain.Identity;

namespace ProEventos.Persistence.Contextos
{
    public class ProEventosContext : IdentityDbContext<IdentityDbContext<User, Role, int,
                                                                        IdentityUserClaim<int>,
                                                                        IdentityUserRole<int>,
                                                                        IdentityUserLogin<int>, 
                                                                        IdentityRoleClaim<int>, 
                                                                        IdentityUserToken<int>>
    {
        public ProEventosContext(DbContextOptions<ProEventosContext> options)
         : base(options) { }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }
        public DbSet<RedeSocial> RedeSociais { get; set; }

        /* associaçao classe PalestranteEvento - junçao entre os eventos e palestrantes */
        protected override void  OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<PalestranteEvento>()
            .HasKey(PE => new {PE.EventoId, PE.PalestranteId});

            //modelbuilder... tem uma entidade chamada evento -> e 
            // este evento tem muitas redes socials
            // dado as rs, uma rs so pertence a um evento- cada rs em um evento
            // qundo estiver no ONDelete seja cascade/cascateada
            modelBuilder.Entity<Evento>()
                .HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Evento)
                .OnDelete(DeleteBehavior.Cascade);
            // palestrante
            modelBuilder.Entity<Palestrante>()
                .HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Palestrante)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}