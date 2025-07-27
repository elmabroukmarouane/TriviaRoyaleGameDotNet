using AutoMapper;
using TriviaRoyaleGame.Business.Services.Interfaces;
using TriviaRoyaleGame.Business.Services.SendEmails.Interface;
using TriviaRoyaleGame.GenericController;
using TriviaRoyaleGame.Api.RealTime.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using TriviaRoyaleGame.Infrastructure.Models.Classes;
using TriviaRoyaleGame.Api.DtoModel.Models;
using Microsoft.AspNetCore.Authorization;

namespace TriviaRoyaleGame.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(
        IGenericService<Category> genericService,
        IMapper mapper, ILogger<GenericController<Category, CategoryViewModel>> logger,
        IHostEnvironment hostEnvironment,
        IConfiguration configuration,
        IHubContext<RealTimeHub> hubContext,
        IMemoryCache cache,
        ISendMailService sendMailService) : GenericController<Category, CategoryViewModel>(genericService, mapper, logger, hostEnvironment, configuration, hubContext, cache, sendMailService)
    { }
}