using DevagramCSharp.Data;
using DevagramCSharp.Models;

namespace DevagramCSharp.Repository.Implementation
{
    public class PublicacaoRepositoryImpl : IPublicacaoRepository
    {
        private readonly DevagramCSharpContext _context;

        public PublicacaoRepositoryImpl(DevagramCSharpContext context)
        {
            _context = context;
        }

        public void Publicar(Publicacao publicacao)
        {
            _context.Add(publicacao);
            _context.SaveChanges();
        }
    }
}
