using DevagramCSharp.Dtos;
using DevagramCSharp.Models;
using DevagramCSharp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevagramCSharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult EfetuarLogin([FromBody] LoginRequisicaoDto loginrequisicao)
        {
            try
            {
                if (!String.IsNullOrEmpty(loginrequisicao.Senha) && !String.IsNullOrEmpty(loginrequisicao.Email) &&
                    !String.IsNullOrWhiteSpace(loginrequisicao.Senha) && !String.IsNullOrWhiteSpace(loginrequisicao.Email))
                {
                    string email = "marcio@gmail.com";
                    string senha = "senha123";

                    if (loginrequisicao.Email == email && loginrequisicao.Senha == senha)
                    {
                        Usuario usuario = new Usuario()
                        {
                            Email = loginrequisicao.Email,
                            Id = 12,
                            Nome = "Marcio Alves"
                        };
                        return Ok(new LoginRespostaDto()
                        {
                            Email = usuario.Email,
                            Nome = usuario.Nome,
                            Token = TokenService.CriarToken(usuario)
                        }); ;
                    }
                    else
                    {
                        return BadRequest(new ErroRespostasDto()
                        {
                            Descricao = "Email ou senha inválido!",
                            Status = StatusCodes.Status400BadRequest
                        });
                    }
                }
                else
                {
                    return BadRequest(new ErroRespostasDto()
                    {
                        Descricao = "Usuário não preencheu os campos de login corretamente",
                        Status = StatusCodes.Status400BadRequest
                    });
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocorreu um erro no login {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErroRespostasDto()
                {
                    Descricao = "Ocorreu um erro ao fazer o login",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}
