using DevagramCSharp.Dtos;
using DevagramCSharp.Models;
using DevagramCSharp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevagramCSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeguidorController : BaseController
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ISeguidorRepository _seguidorRepository;

        public SeguidorController(ILogger<LoginController> logger, ISeguidorRepository seguidorRepository, IUsuarioRepository usuarioRepository) : base(usuarioRepository)
        {
            _logger = logger;
            _seguidorRepository = seguidorRepository;
        }

        [HttpPut]
        public IActionResult Seguir(int idSeguido)
        {
            try
            {
                Usuario usuarioseguido = _usuarioRepository.GetUsuarioPorId(idSeguido);
                Usuario usuarioseguidor = LerToken();

                if (usuarioseguido != null)
                {
                    Seguidor seguidor =  _seguidorRepository.GetSeguidor(usuarioseguidor.Id,usuarioseguido.Id);
                    if (seguidor != null)
                    {
                        _seguidorRepository.DesSeguir(seguidor);
                    }
                    else
                    {
                        Seguidor seguidornovo = new Seguidor()
                        {
                            IdUsuarioSeguido = usuarioseguido.Id,
                            IdUsuarioSeguidor = usuarioseguidor.Id,
                        };
                        _seguidorRepository.Seguir(seguidornovo);
                    }

                }
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError("Ocorreu um erro de Login: " + e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErroRespostasDto()
                {
                    Descricao = $"Ocorreu o seguinte erro: {e.Message}",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}
