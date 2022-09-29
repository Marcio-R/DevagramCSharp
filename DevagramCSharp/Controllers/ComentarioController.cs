using DevagramCSharp.Dtos;
using DevagramCSharp.Models;
using DevagramCSharp.Repository;
using DevagramCSharp.Repository.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevagramCSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : BaseController
    {
        private readonly ILogger<ComentarioController> _logger;
        private readonly IComentarioRepository _comentarioRepository;

        public ComentarioController(ILogger<ComentarioController> logger, IUsuarioRepository usuarioRepository, IComentarioRepository comentarioRepository) : base(usuarioRepository)
        {
            _logger = logger;
            _comentarioRepository = comentarioRepository;
        }

        [HttpPut]
        public IActionResult Comentar([FromBody] ComentarioRequisicaoDto comentariodto)
        {
            try
            {
                if (comentariodto != null)
                {
                    if (String.IsNullOrEmpty(comentariodto.Descricao) || String.IsNullOrWhiteSpace(comentariodto.Descricao))
                    {
                        _logger.LogError("O comentario rescebido estava vazio");
                        return BadRequest("Por favor coloque seu comentario");
                    }
                    Comentario comentario = new Comentario();
                    comentario.Descricao = comentariodto.Descricao;
                    comentario.IdPublicacao = comentariodto.IdPublicacao;
                    comentario.IdUsuario = LerToken().Id;

                    _comentarioRepository.Comentar(comentario);
                }
                return Ok("Comentario salvo com sucesso!");
            }
            catch (Exception e)
            {

                _logger.LogError("Ocorreu um erro ao comentar");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErroRespostasDto()
                {
                    Descricao = $"Ocorreu um erro ao comentar {e.Message}",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}
