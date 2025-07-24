using AutoMapper;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TriviaRoyaleGame.Api.DtoModel.Models;
using TriviaRoyaleGame.Api.Extensions.Logging;
using TriviaRoyaleGame.Business.Services.Interfaces;
using TriviaRoyaleGame.Infrastructure.Models.Classes;

namespace TriviaRoyaleGame.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController(
        ICryptoService cryptoService,
        IMapper mapper,
        ILogger<CryptoController> logger,
        IHostEnvironment hostEnvironment,
        IConfiguration configuration) : ControllerBase
    {
        protected readonly ICryptoService _cryptoService = cryptoService ?? throw new ArgumentException(null, nameof(cryptoService));
        protected readonly IMapper _mapper = mapper ?? throw new ArgumentException(null, nameof(mapper));
        protected readonly ILogger _logger = logger ?? throw new ArgumentException(null, nameof(logger));
        protected readonly IHostEnvironment _hostEnvironment = hostEnvironment ?? throw new ArgumentException(null, nameof(hostEnvironment));
        protected readonly IConfiguration _configuration = configuration ?? throw new ArgumentException(null, nameof(configuration));

        [HttpPost("Encrypt")]
        public async Task<IActionResult> Encrypt(CryptoPayloadViewModel cryptoPayloadViewModel)
        {
            try
            {
                if (cryptoPayloadViewModel == null)
                {
                    _logger.LoggingMessageWarning("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, "CRYPTO PAYLOAD IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Encrypt(CryptoPayload cryptoPayload)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "cryptoPayloadViewModel received is null !"
                    });
                }
                var cryptoPayload = _mapper.Map<CryptoPayload>(cryptoPayloadViewModel);
                var response = await _cryptoService.EncryptAsync(cryptoPayload);
                if (response == null)
                {
                    _logger.LoggingMessageWarning("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, "RESPONSE IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Encrypt(CryptoPayload cryptoPayload)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Response is null !"
                    });
                }
                return Ok(new
                {
                    response
                });
            }
            catch (Exception ex)
            {
                _logger.LoggingMessageError("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "" + " - Encrypt(CryptoPayload cryptoPayload)", ex, _hostEnvironment.ContentRootPath);
                return StatusCode(500, new
                {
                    Message = "Crypto String failed !"
                });
            }
        }

        [HttpPost("Decrypt")]
        public async Task<IActionResult> Decrypt(CryptoPayloadViewModel cryptoPayloadViewModel)
        {
            try
            {
                if (cryptoPayloadViewModel == null)
                {
                    _logger.LoggingMessageWarning("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, "CRYPTO PAYLOAD IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Decrypt(CryptoPayload cryptoPayload)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "cryptoPayloadViewModel received is null !"
                    });
                }
                var cryptoPayload = _mapper.Map<CryptoPayload>(cryptoPayloadViewModel);
                var response = await _cryptoService.DecryptAsync(cryptoPayload);
                if (response == null)
                {
                    _logger.LoggingMessageWarning("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, "RESPONSE IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Decrypt(CryptoPayload cryptoPayload)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Response is null !"
                    });
                }
                return Ok(new
                {
                    response
                });
            }
            catch (Exception ex)
            {
                _logger.LoggingMessageError("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "" + " - Decrypt(CryptoPayload cryptoPayload)", ex, _hostEnvironment.ContentRootPath);
                return StatusCode(500, new
                {
                    Message = "Crypto String failed !"
                });
            }
        }
    }
}
