using DevagramCSharp.Data;
using DevagramCSharp.Dtos;
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

        public List<PublicacaoFeedRespostaDto> GetPublicacoesFeed(int idUsuario)
        {
            var feed =
                from publicacoes in _context.Publicacaos
                join seguidores in _context.Seguidores on publicacoes.IdUsuario equals seguidores.IdUsuarioSeguido
                where seguidores.IdUsuarioSeguidor == idUsuario
                select new PublicacaoFeedRespostaDto
                {
                    IdPublicacao = publicacoes.Id,
                    Descricao = publicacoes.Descricao,
                    Foto = publicacoes.Foto,
                    IdUsuario = publicacoes.IdUsuario
                };
            return feed.ToList();
        }

        public void Publicar(Publicacao publicacao)
        {
            _context.Add(publicacao);
            _context.SaveChanges();
        }
    }
}
