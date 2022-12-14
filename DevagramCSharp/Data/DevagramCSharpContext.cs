using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DevagramCSharp.Models;

namespace DevagramCSharp.Data
{
    public class DevagramCSharpContext : DbContext
    {
        public DevagramCSharpContext (DbContextOptions<DevagramCSharpContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; } 
        public DbSet<Seguidor> Seguidores { get; set; } 
        public DbSet<Publicacao> Publicacaos { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Curtida> Curtidas { get; set; }


    }
}
