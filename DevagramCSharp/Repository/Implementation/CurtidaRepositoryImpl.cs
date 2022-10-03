using DevagramCSharp.Data;
using DevagramCSharp.Models;

namespace DevagramCSharp.Repository.Implementation
{
    public class CurtidaRepositoryImpl : ICurtidaRepository
    {
        private readonly DevagramCSharpContext _context;

        public CurtidaRepositoryImpl(DevagramCSharpContext context)
        {
            _context = context;
        }

        public void Curtir(Curtida curtida)
        {
            _context.Add(curtida);
            _context.SaveChanges();
        }

        public void Descurtir(Curtida descurtida)
        {
            _context.Remove(descurtida);
            _context.SaveChanges();
        }

        public Curtida GetCurtida(int idPublicacao, int idUsuario)
        {
            return _context.Curtidas.FirstOrDefault(c => c.IdPublicacao == idPublicacao && c.IdUsuario == idUsuario);
        }

        public List<Curtida> GetCurtidaPorPublicacao(int idPublicacao)
        {
            return _context.Curtidas.Where(c => c.IdPublicacao == idPublicacao).ToList();
        }
    }
}
