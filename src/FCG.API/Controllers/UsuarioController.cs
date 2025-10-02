using System.Security.Claims;
using FCG.Application.DTOs.Inputs;
using FCG.Application.DTOs.Inputs.Usuarios;
using FCG.Application.DTOs.Outputs;
using FCG.Application.DTOs.Outputs.Usuarios;
using FCG.Application.DTOs.Queries.Usuarios;
using FCG.Application.Security;
using FCG.Application.Services;
using FCG.Infra.Security.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.API.Controllers
{
    [Authorize]
    [Route("usuarios")]
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioAppService _usuarioAppService;

        public UsuarioController(ILogger<UsuarioController> logger,
            IUsuarioAppService usuarioAppService)
        {
            _logger = logger;
            _usuarioAppService = usuarioAppService;
        }

        /// <summary>
        /// Pesquisa usuários cadastrados no sistema com filtros opcionais.
        /// </summary>
        /// <remarks>
        /// Permite que administradores consultem a lista de usuários cadastrados, com suporte à paginação.
        /// </remarks>
        /// <param name="query">
        /// Parâmetros da pesquisa, incluindo número da página e tamanho da página.
        /// </param>
        /// <response code="200">Retorna a lista paginada de usuários encontrados.</response>
        [Authorize(Roles = Roles.ADMINISTRADOR)]
        [HttpGet("pesquisar", Name = "PesquisarUsuarios")]
        [ProducesResponseType(typeof(PaginacaoOutput<UsuarioItemListaOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> PesquisarUsuarios([FromQuery] PesquisarUsuariosQuery query)
        {
            if (query.Pagina <= 0 || query.TamanhoPagina <= 0)
                return BadRequest(new { error = "Parâmetros inválidos." });

            var resultado = await _usuarioAppService.PesquisarUsuarios(query);

            return Ok(resultado);
        }

        /// <summary>
        /// Obtém os dados de um usuário pelo seu identificador.
        /// </summary>
        /// <remarks>
        /// Requer acesso de administrador. 
        /// Retorna os dados do usuário.
        /// </remarks>
        /// <param name="id">Identificador do usuário.</param>
        /// <response code="200">Usuário encontrado com sucesso. Retorna os dados do usuário.</response>
        /// <response code="404">Usuário não encontrado.</response>
        [Authorize(Roles = Roles.ADMINISTRADOR)]
        [HttpGet("{id}", Name = "ObterUsuario")]
        [ProducesResponseType(typeof(UsuarioOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterUsuario([FromRoute] Guid id)
        {
            var usuario = await _usuarioAppService.ObterPorId(id);

            return usuario is null ? NotFound() : Ok(usuario);
        }


        /// <summary>
        /// Cria um novo usuário no sistema.
        /// </summary>
        /// <remarks>
        /// Requer acesso de administrador. 
        /// É necessário informar nome, e-mail válido e uma senha que atenda aos critérios de segurança definidos: 
        /// mínimo de 8 caracteres, com pelo menos uma letra maiúscula, uma minúscula, um número e um símbolo.
        /// </remarks>
        /// <param name="input">Dados necessários para o registro do usuário.</param>
        /// <response code="201">Usuário criado com sucesso. Retorna os dados do usuário.</response>
        /// <response code="400">Requisição inválida ou senha incorreta.</response>
        [Authorize(Roles = Roles.ADMINISTRADOR)]
        [HttpPost(Name = "CriarUsuario")]
        [ProducesResponseType(typeof(UsuarioOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseOutput), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CriarUsuario([FromBody] CriarUsuarioInput input)
        {
            var resultado = await _usuarioAppService.Criar(input);

            return !resultado.Success
                ? BadRequest(resultado)
                : CreatedAtRoute("CriarUsuario", new { id = resultado.Data.Id }, resultado.Data);
        }

        /// <summary>
        /// Remove um usuário do sistema.
        /// </summary>
        /// <remarks>
        /// Requer acesso de administrador. 
        /// Essa operação é irreversível e remove permanentemente o usuário da base de dados.
        /// </remarks>
        /// <param name="id">Identificador do usuário.</param>
        /// <response code="204">Usuário removido com sucesso.</response>
        /// <response code="400">Requisição inválida ou usuário não encontrado.</response>
        [Authorize(Roles = Roles.ADMINISTRADOR)]
        [HttpDelete("{id}", Name = "RemoverUsuario")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BaseOutput), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoverUsuario([FromRoute] Guid id)
        {
            var resultado = await _usuarioAppService.Remover(id);

            return !resultado.Success ? BadRequest(resultado) : NoContent();
        }

        #region Biblioteca

        /// <summary>
        /// Obtém os jogos do usuário autenticado.
        /// </summary>
        /// <remarks>
        /// Retorna os jogos que o usuário possui em sua biblioteca.
        /// </remarks>
        /// <response code="200">Retorna a lista dos jogos do usuário.</response>
        [Authorize(Roles = Roles.USUARIO)]
        [HttpGet("biblioteca", Name = "ObterBibliotecaUsuario")]
        [ProducesResponseType(typeof(IEnumerable<UsuarioJogoOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterBibliotecaUsuario()
        {
            var resultado = await _usuarioAppService.ObterBibliotecaUsuario();

            return Ok(resultado);
        }

        /// <summary>
        /// Adiciona um novo jogo na biblioteca do usuário autenticado
        /// </summary>
        /// <remarks>
        /// É necessário informar o id de um jogo que o usuário ainda não possui
        /// </remarks>
        /// <param name="input">Dados necessários para adicionar o jogo na biblioteca.</param>
        /// <response code="200">Jogo adicionado a biblioteca com sucesso. Retorna os dados do jogo.</response>
        /// <response code="400">Requisição inválida.</response>
        [Authorize(Roles = Roles.USUARIO)]
        [HttpPost("biblioteca", Name = "AdicionarJogoBibliotecaUsuario")]
        [ProducesResponseType(typeof(UsuarioJogoOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseOutput), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdicionarJogoBibliotecaUsuario([FromBody] AdicionarJogoBibliotecaInput input)
        {
            var resultado = await _usuarioAppService.AdicionarJogoBibliotecaUsuario(input);

            return !resultado.Success ? BadRequest(resultado) : Ok(resultado.Data);
        }

        #endregion
    }
}