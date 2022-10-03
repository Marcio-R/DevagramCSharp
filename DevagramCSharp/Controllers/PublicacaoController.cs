using DevagramCSharp.Dtos;
using DevagramCSharp.Migrations;
using DevagramCSharp.Models;
using DevagramCSharp.Repository;
using DevagramCSharp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Publicacao = DevagramCSharp.Models.Publicacao;

namespace DevagramCSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacaoController : BaseController
    {
        private readonly ILogger<PublicacaoController> _logger;
        private readonly IPublicacaoRepository _publicacaoRepository;
        private readonly IComentarioRepository _comentarioRepository;
        private readonly ICurtidaRepository _curtidaRepository;
        public PublicacaoController(ILogger<PublicacaoController> logger, IPublicacaoRepository publicacaoRepository, IUsuarioRepository usuarioRepository
            , IComentarioRepository comentarioRepository, ICurtidaRepository curtidaRepository) : base(usuarioRepository)
        {
            _logger = logger;
            _publicacaoRepository = publicacaoRepository;
            _comentarioRepository = comentarioRepository;
            _curtidaRepository = curtidaRepository;
        }
        [HttpPost]
        public IActionResult Publicar([FromForm] PublicacaoRequisicaoDto publicacaodto)
        {
            try
            {
                Usuario usuario = LerToken();
                CosmicService cosmic = new CosmicService();
                if (publicacaodto != null)
                {
                    if (string.IsNullOrEmpty(publicacaodto.Descricao) && string.IsNullOrWhiteSpace(publicacaodto.Descricao))
                    {
                        _logger.LogError("A descrição está inválida: ");
                        return BadRequest("É obrigatorio a descrição na publicação");
                    }
                    if (publicacaodto.Foto == null)
                    {
                        _logger.LogError("A foto está inválida ");
                        return BadRequest("É obrigatorio a foto na publicação!");
                    }
                    Publicacao publicacao = new Publicacao()
                    {
                        Descricao = publicacaodto.Descricao,
                        IdUsuario = usuario.Id,
                        Foto = cosmic.EnviarImagem(new ImagemDto { Imagem = publicacaodto.Foto, Nome = "publicacao" })
                    };
                    _publicacaoRepository.Publicar(publicacao);
                }
                return Ok("Publicaçõa salva com sucesseo");
            }
            catch (Exception e)
            {

                _logger.LogError("Ocorreu um erro na publicação: " + e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErroRespostasDto()
                {
                    Descricao = $"Ocorreu o seguinte erro: {e.Message}",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
        [HttpGet]
        public IActionResult FeedHome()
        {
            try
            {
                List<PublicacaoFeedRespostaDto> feed = _publicacaoRepository.GetPublicacoesFeed(LerToken().Id);
                foreach (PublicacaoFeedRespostaDto feedResposta in feed)
                {
                    Usuario usuario = _usuarioRepository.GetUsuarioPorId(feedResposta.IdUsuario);
                    UsuarioRespostaDto usuarioRespostaDto = new UsuarioRespostaDto()
                    {
                        Nome = usuario.Nome,
                        Avatar = usuario.FotoPerfil,
                        IdUsuario = usuario.Id
                    };
                    feedResposta.Usuario = usuarioRespostaDto;

                    List<Comentario> comentarios = _comentarioRepository.GetComentarioPublicacao(feedResposta.IdPublicacao);
                    feedResposta.Comentarios = comentarios;

                    List<Curtida> curtidas = _curtidaRepository.GetCurtidaPorPublicacao(feedResposta.IdPublicacao);
                    feedResposta.Curtidas = curtidas;
                }
                return Ok(feed);
            }
            catch (Exception e)
            {

                _logger.LogError("Ocorreu um erro ao carregar o feed da Home: " + e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErroRespostasDto()
                {
                    Descricao = $"Ocorreu o seguinte erro ao carregar o feed da Home: {e.Message}",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}
