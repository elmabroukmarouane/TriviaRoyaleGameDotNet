using AutoMapper;
using TriviaRoyaleGame.Business.Services.Interfaces;
using TriviaRoyaleGame.Business.Services.SendEmails.Interface;
using TriviaRoyaleGame.Infrastructure.Models.Classes;
using TriviaRoyaleGame.Api.DtoModel.Models;
using TriviaRoyaleGame.GenericController;
using TriviaRoyaleGame.Api.RealTime.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace TriviaRoyaleGame.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController(
        IGenericService<ClientAppLog> genericService,
        IMapper mapper, ILogger<GenericController<ClientAppLog, ClientAppLogViewModel>> logger,
        IHostEnvironment hostEnvironment,
        IConfiguration configuration,
        IHubContext<RealTimeHub> hubContext,
        IMemoryCache cache,
        ISendMailService sendMailService) : GenericController<ClientAppLog, ClientAppLogViewModel>(genericService, mapper, logger, hostEnvironment, configuration, hubContext, cache, sendMailService) { }
}
