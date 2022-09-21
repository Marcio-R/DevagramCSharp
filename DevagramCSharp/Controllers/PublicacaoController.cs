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

        public PublicacaoController(ILogger<PublicacaoController> logger, IPublicacaoRepository publicacaoRepository, IUsuarioRepository usuarioRepository) : base(usuarioRepository)
        {
            _logger = logger;
            _publicacaoRepository = publicacaoRepository;
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
                    if(publicacaodto.Foto == null)
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
    }
}
