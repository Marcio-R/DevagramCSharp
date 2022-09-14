using DevagramCSharp.Dtos;
using DevagramCSharp.Models;
using DevagramCSharp.Repository;
using DevagramCSharp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevagramCSharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : BaseController
    {
        public readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository) : base(usuarioRepository)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public IActionResult ObterUsuario()
        {
            try
            {
                Usuario usuario = LerToken();

                return Ok(new UsuarioRespostaDto
                {
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                });
            }
            catch (Exception e)
            {
                _logger.LogError("Ocorreu um erro ao obter o usuário");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErroRespostasDto()
                {
                    Descricao = $"Ocorreu o seguinte erro: {e.Message}",
                    Status = StatusCodes.Status500InternalServerError
                });
            }

        }

        [HttpPut]
        public IActionResult AtualizarUsuario([FromForm] UsuarioRequisicaoDto usuariodto)
        {
            try
            {
                Usuario usuario = LerToken();

                if (usuariodto != null)
                {
                    var erros = new List<string>();
                    if (string.IsNullOrEmpty(usuariodto.Nome) || string.IsNullOrWhiteSpace(usuariodto.Nome))
                    {
                        erros.Add("Nome inválido");
                    }

                    if (erros.Count > 0)
                    {
                        return BadRequest(new ErroRespostasDto()
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Erros = erros
                        });
                    }
                    else
                    {
                        CosmicService cosmicservice = new CosmicService();

                        usuario.Nome = usuariodto.Nome;
                        usuario.FotoPerfil = cosmicservice.EnviarImagem(new ImagemDto { Imagem = usuariodto.FotoPerfil, Nome = usuariodto.Nome.Replace(" ", "") });
                        _usuarioRepository.AtualizarUsuario(usuario);
                    }

                }
                return Ok("Usuário salvo e atualizado!");
            }
            catch (Exception e)
            {

                _logger.LogError("Ocorreu um erro ao Salvar o usuario ");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErroRespostasDto()
                {
                    Descricao = $"Ocorreu o seguinte erro: {e.Message}",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult SalvarUsuaio([FromForm] UsuarioRequisicaoDto usuariodto)
        {
            try
            {

                if (usuariodto != null)
                {
                    var erros = new List<string>();
                    if (string.IsNullOrEmpty(usuariodto.Nome) || string.IsNullOrWhiteSpace(usuariodto.Nome))
                    {
                        erros.Add("Nome inválido");
                    }
                    if (string.IsNullOrEmpty(usuariodto.Email) || string.IsNullOrWhiteSpace(usuariodto.Email) || !usuariodto.Email.Contains("@"))
                    {
                        erros.Add("E-mail inválido");
                    }
                    if (string.IsNullOrEmpty(usuariodto.Senha) || string.IsNullOrWhiteSpace(usuariodto.Senha))
                    {
                        erros.Add("Senha inválida");
                    }

                    if (erros.Count > 0)
                    {
                        return BadRequest(new ErroRespostasDto()
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Erros = erros
                        });
                    }

                    CosmicService cosmicservice = new CosmicService();

                    Usuario usuario = new Usuario()
                    {
                        Email = usuariodto.Email,
                        Senha = usuariodto.Senha,
                        Nome = usuariodto.Nome,
                        FotoPerfil = cosmicservice.EnviarImagem(new ImagemDto { Imagem = usuariodto.FotoPerfil, Nome = usuariodto.Nome.Replace(" ", "") })
                    };

                    usuario.Senha = Utils.MD5Utils.GerarHashMD5(usuario.Senha);
                    usuario.Email = usuario.Email.ToLower();
                    if (!_usuarioRepository.VerificarEmail(usuario.Email))
                    {
                        _usuarioRepository.Salva(usuario);
                    }
                    else
                    {
                        return BadRequest(new ErroRespostasDto()
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Descricao = "Usuário já cadastrado"
                        });
                    }
                }
                return Ok("Usuario salvo com sucesso!");
            }
            catch (Exception e)
            {

                _logger.LogError("Ocorreu um erro ao Salvar o usuario ");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErroRespostasDto()
                {
                    Descricao = $"Ocorreu o seguinte erro: {e.Message}",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}
