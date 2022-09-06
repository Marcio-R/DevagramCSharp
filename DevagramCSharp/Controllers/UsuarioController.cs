﻿using DevagramCSharp.Dtos;
using DevagramCSharp.Models;
using DevagramCSharp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevagramCSharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : BaseController
    {
        public readonly ILogger<UsuarioController> _logger;
        public readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        [Authorize]
        public IActionResult ObterUsuario()
        {
            try
            {
                Usuario usuario = new Usuario()
                {
                    Email = "Marcio@gmail.com",
                    Nome = "Marcio",
                    Id = 100
                };
                return Ok(usuario);
            }
            catch (Exception e)
            {
                _logger.LogError("Ocorreu um erro ao obter o ");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErroRespostasDto()
                {
                    Descricao = $"Ocorreu o seguinte erro: {e.Message}",
                    Status = StatusCodes.Status500InternalServerError
                });
            }

        }
        [HttpPost]
        public IActionResult SalvarUsuaio([FromBody] Usuario usuario)
        {
            try
            {
                if (usuario != null)
                {
                    var erros = new List<string>();
                    if (string.IsNullOrEmpty(usuario.Nome) || string.IsNullOrWhiteSpace(usuario.Nome))
                    {
                        erros.Add("Nome inválido");
                    }
                    if (string.IsNullOrEmpty(usuario.Email) || string.IsNullOrWhiteSpace(usuario.Email) || !usuario.Email.Contains("@"))
                    {
                        erros.Add("E-mail inválido");
                    }
                    if (string.IsNullOrEmpty(usuario.Senha) || string.IsNullOrWhiteSpace(usuario.Senha))
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
                return Ok(usuario);
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
