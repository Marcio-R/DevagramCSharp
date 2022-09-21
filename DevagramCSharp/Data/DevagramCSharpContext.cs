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

        public DbSet<Usuario> Usuario { get; set; } = default!;
        public DbSet<Seguidor> Seguidores { get; set; } = default!;
        public DbSet<Publicacao> Publicacaos { get; set; } = default!;
    }
}
