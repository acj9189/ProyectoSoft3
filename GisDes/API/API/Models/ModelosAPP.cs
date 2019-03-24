namespace API.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelosAPP : DbContext
    {
        public ModelosAPP()
            : base("name=ModelosAPP")
        {
        }

        public virtual DbSet<Integrante> Integrante { get; set; }
        public virtual DbSet<Programa> Programa { get; set; }
        public virtual DbSet<RegistroApp> RegistroApp { get; set; }
        public virtual DbSet<SemilleroInvestigacion> SemilleroInvestigacion { get; set; }
        public virtual DbSet<semilleroPrograma> semilleroPrograma { get; set; }
        public virtual DbSet<Solicitud> Solicitud { get; set; }

       /* protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Integrante>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Integrante>()
                .Property(e => e.NivelAcademico)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Integrante>()
                .Property(e => e.TipoIntegrante)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Integrante>()
                .Property(e => e.Estado)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Integrante>()
                .HasMany(e => e.SemilleroInvestigacion)
                .WithRequired(e => e.Integrante)
                .HasForeignKey(e => e.Coordinador)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Integrante>()
                .HasOptional(e => e.Solicitud)
                .WithRequired(e => e.Integrante);

            modelBuilder.Entity<Integrante>()
                .HasOptional(e => e.Solicitud1)
                .WithRequired(e => e.Integrante1);

            modelBuilder.Entity<Programa>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Programa>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Programa>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Programa>()
                .HasOptional(e => e.semilleroPrograma)
                .WithRequired(e => e.Programa);

            modelBuilder.Entity<RegistroApp>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<RegistroApp>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<RegistroApp>()
                .Property(e => e.Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<RegistroApp>()
                .Property(e => e.Correo)
                .IsUnicode(false);

            modelBuilder.Entity<RegistroApp>()
                .Property(e => e.Carrera)
                .IsUnicode(false);

            modelBuilder.Entity<RegistroApp>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<SemilleroInvestigacion>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SemilleroInvestigacion>()
                .Property(e => e.Coordinador)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SemilleroInvestigacion>()
                .Property(e => e.LineaInvestigacion)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SemilleroInvestigacion>()
                .Property(e => e.Estado)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SemilleroInvestigacion>()
                .HasOptional(e => e.semilleroPrograma)
                .WithRequired(e => e.SemilleroInvestigacion);

            modelBuilder.Entity<SemilleroInvestigacion>()
                .HasOptional(e => e.Solicitud)
                .WithRequired(e => e.SemilleroInvestigacion);

            modelBuilder.Entity<semilleroPrograma>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Solicitud>()
                .Property(e => e.Id)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Solicitud>()
                .Property(e => e.NombreIntegrante)
                .IsUnicode(false);

            modelBuilder.Entity<Solicitud>()
                .Property(e => e.Correo)
                .IsUnicode(false);

            modelBuilder.Entity<Solicitud>()
                .Property(e => e.DescripcionPorqueQuiereIngresar)
                .IsUnicode(false);
        }*/
    }
}
