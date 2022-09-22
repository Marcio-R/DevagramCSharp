using DevagramCSharp.Data;
using DevagramCSharp.Models;

namespace DevagramCSharp.Repository.Implementation
{
    public class ComentarioRepositoryImp : IComentarioRepository
    {
        private readonly DevagramCSharpContext _context;

        public ComentarioRepositoryImp(DevagramCSharpContext context)
        {
            _context = context;
        }

        public void Comentar(Comentario comentario)
        {
            _context.Add(comentario);
            _context.SaveChanges();
        }
    }
}
