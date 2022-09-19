using DevagramCSharp.Data;
using DevagramCSharp.Models;

namespace DevagramCSharp.Repository.Implementation
{
    public class SeguidorRepositoryImp : ISeguidorRepository
    {
        private readonly DevagramCSharpContext _context;

        public SeguidorRepositoryImp(DevagramCSharpContext context)
        {
            _context = context;
        }

        public bool DesSeguir(Seguidor seguidor)
        {
            try
            {
                _context.Remove(seguidor);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Seguidor GetSeguidor(int idseguidor, int idseguido)
        {
            return _context.Seguidores.FirstOrDefault(s => s.IdUsuarioSeguidor == idseguidor && s.IdUsuarioSeguido == idseguido);
        }

        public bool Seguir(Seguidor seguidor)
        {
            try
            {
                _context.Add(seguidor);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
