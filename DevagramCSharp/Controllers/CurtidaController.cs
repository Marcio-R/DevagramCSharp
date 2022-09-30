﻿using DevagramCSharp.Dtos;
using DevagramCSharp.Models;
using DevagramCSharp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevagramCSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurtidaController : BaseController
    {
        private readonly ILogger<CurtidaController> _logger;
        private readonly ICurtidaRepository _curtidaRepository;

        public CurtidaController(ILogger<CurtidaController> logger, ICurtidaRepository curtidaRepository, IUsuarioRepository usuarioRepository) : base(usuarioRepository)
        {
            _logger = logger;
            _curtidaRepository = curtidaRepository;
        }
        [HttpPut]
        public IActionResult Curtir([FromBody] CurtidaRequisicaoDto curtidadto)
        {
            try
            {
                if (curtidadto != null)
                {
                    Curtida curtida = _curtidaRepository.GetCurtida(curtidadto.IdPublicacao, LerToken().Id);
                    if (curtida != null)
                    {
                        _curtidaRepository.Descurtir(curtida);
                        return Ok("Descurtida efetuada com sucesso!");
                    }
                    else
                    {

                        Curtida curtidaNova = new Curtida()
                        {
                            IdPublicacao = curtidadto.IdPublicacao,
                            IdUsuario = LerToken().Id
                        };
                        _curtidaRepository.Curtir(curtidaNova);
                        return Ok("Curtida efetuada com sucesso!");
                    }
                }
                else
                {
                    _logger.LogError("A requisição de curtir está vazia");
                    return BadRequest("A requisição de curtir está vazia");
                }
                
            }
            catch (Exception e)
            {

                _logger.LogError("Ocorreu um erro ao curtir/descurtir: " + e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErroRespostasDto()
                {
                    Descricao = $"Ocorreu o seguinte erro ao curtir/descurtir:",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }

    }
}
