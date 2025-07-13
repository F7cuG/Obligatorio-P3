using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace obligatorio_PIII.Models
{
    public partial class obligatorioP3Entities1 : DbContext
    {
        public obligatorioP3Entities1()
            : base("name=obligatorioP3Entities1")
        {
        }

        public virtual DbSet<usuarios> Usuarios { get; set; }
        public virtual DbSet<roles> Roles { get; set; }
        public virtual DbSet<permisos> Permisos { get; set; }
        public virtual DbSet<clientes> Clientes { get; set; }
        public virtual DbSet<programas> Programas { get; set; }
        public virtual DbSet<noticias> Noticias { get; set; }
        public virtual DbSet<comentarios> Comentarios { get; set; }
        public virtual DbSet<patrocinadores> patrocinadores { get; set; }
        public virtual DbSet<PlanDeAnuncios> PlanDeAnuncios { get; set; }
        public virtual DbSet<clima> Clima { get; set; }
        public virtual DbSet<cotizaciones> Cotizaciones { get; set; }
        public virtual DbSet<conductores> Conductores { get; set; }

    }
}
