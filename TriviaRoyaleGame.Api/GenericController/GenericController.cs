using AutoMapper;
using TriviaRoyaleGame.Business.Helpers;
using TriviaRoyaleGame.Business.Helpers.LambdaManagement.Helper;
using TriviaRoyaleGame.Business.Helpers.LambdaManagement.Models;
using TriviaRoyaleGame.Business.Services.Interfaces;
using TriviaRoyaleGame.Business.Services.SendEmails.Interface;
using TriviaRoyaleGame.Business.Services.SendEmails.Models.Classes;
using TriviaRoyaleGame.Infrastructure.Models.Classes;
using TriviaRoyaleGame.Api.Extensions.Logging;
using TriviaRoyaleGame.Api.RealTime.Class;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace TriviaRoyaleGame.GenericController
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<TEntity, TEntityViewModel>(
        IGenericService<TEntity> genericService,
        IMapper mapper,
        ILogger<GenericController<TEntity, TEntityViewModel>> logger,
        IHostEnvironment hostEnvironment,
        IConfiguration configuration,
        IHubContext<RealTimeHub> hubContext,
        IMemoryCache cache,
        ISendMailService sendMailService) : ControllerBase
        where TEntity : Entity
        where TEntityViewModel : Entity
    {
        #region ATTRIBUTES
        protected readonly IGenericService<TEntity> _genericService = genericService ?? throw new ArgumentException(null, nameof(genericService));
        protected readonly IMapper _mapper = mapper ?? throw new ArgumentException(null, nameof(mapper));
        protected readonly ILogger<GenericController<TEntity, TEntityViewModel>> _logger = logger ?? throw new ArgumentException(null, nameof(logger));
        protected readonly IHostEnvironment _hostEnvironment = hostEnvironment ?? throw new ArgumentException(null, nameof(hostEnvironment));
        protected readonly IConfiguration _configuration = configuration ?? throw new ArgumentException(null, nameof(configuration));
        protected readonly IHubContext<RealTimeHub> _realTimeHub = hubContext ?? throw new ArgumentException(null, nameof(hubContext));
        protected readonly IMemoryCache _cache = cache ?? throw new ArgumentException(null, nameof(cache));
        protected readonly ISendMailService _sendMailService = sendMailService ?? throw new ArgumentException(null, nameof(sendMailService));
        #endregion

        #region READ
        [HttpGet]
        public IActionResult Get(string? includes = null)
        {
            try
            {
                var list = _genericService.GetEntitiesAsync(includes: includes).ToList();
                if (list == null)
                {
                    _logger.LoggingMessageWarning("TriviaRoyaleGame", (int)HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Get()", _hostEnvironment.ContentRootPath);
                    return NotFound(new
                    {
                        Message = "List not found !",
                        StatusCode = (int)HttpStatusCode.NotFound + " - " + HttpStatusCode.NotFound.ToString()
                    });
                }
                var mappedList = _mapper.Map<IList<TEntityViewModel>>(list).OrderByDescending(x => x.Id).ToList();
                return Ok(mappedList);
            }
            catch (Exception ex)
            {
                _logger.LoggingMessageError("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "" + " - Get()", ex, _hostEnvironment.ContentRootPath);
                return StatusCode(500, new
                {
                    Message = "Get failed !"
                });
            }
        }

        [HttpGet("{id:int}")]
        public virtual IActionResult Get(int id, string? includes = null)
        {
            try
            {
                var row = _genericService.GetEntitiesAsync(expression: x => x.Id == id, includes: includes).SingleOrDefault();
                if (row == null)
                {
                    _logger.LoggingMessageWarning("TriviaRoyaleGame", (int)HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Get(int id)", _hostEnvironment.ContentRootPath);
                    return NotFound(new
                    {
                        Message = "Item not found !",
                        StatusCode = (int)HttpStatusCode.NotFound + " - " + HttpStatusCode.NotFound.ToString()
                    });
                }
                var mappedRow = _mapper.Map<TEntityViewModel>(row);
                return Ok(mappedRow);
            }
            catch (Exception ex)
            {
                _logger.LoggingMessageError("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "" + " - Get(int id)", ex, _hostEnvironment.ContentRootPath);
                return StatusCode(500, new
                {
                    Message = "Get failed !"
                });
            }
        }

        // TODO : Make the same for orderBy like LambdaExpressionModel
        [HttpPost("filter")]
        public virtual IActionResult Get(FilterDataModel filterDataModel)
        {
            try
            {
                var lambdaExpression = ExpressionBuilder.BuildLambda<TEntity>(filterDataModel.LambdaExpressionModel);
                IList<TEntity>? filteredRows = null;
                if (lambdaExpression != null)
                {
                    filteredRows = _genericService.GetEntitiesAsync(lambdaExpression, includes: filterDataModel.Includes, splitChar: filterDataModel.SplitChar, disableTracking: filterDataModel.DisableTracking, take: filterDataModel.Take, offset: filterDataModel.Offset).ToList();
                }
                if (filteredRows == null)
                {
                    _logger.LoggingMessageWarning("TriviaRoyaleGame", (int)HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Post(filter)", _hostEnvironment.ContentRootPath);
                    return NotFound(new
                    {
                        Message = "List not found !",
                        StatusCode = (int)HttpStatusCode.NotFound + " - " + HttpStatusCode.NotFound.ToString()
                    });
                }
                var mappedFilteredRows = _mapper.Map<IList<TEntityViewModel>>(filteredRows).OrderByDescending(x => x.Id).ToList();
                return Ok(mappedFilteredRows);
            }
            catch (Exception ex)
            {
                _logger.LoggingMessageError("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "" + " - Post(filter)", ex, _hostEnvironment.ContentRootPath);
                return StatusCode(500, new
                {
                    Message = "Get failed !"
                });
            }
        }
        #endregion

        #region CREATE
        [HttpPost]
        public virtual async Task<IActionResult> Post(TEntityViewModel? entity)
        {
            try
            {
                if (entity == null)
                {
                    _logger.LoggingMessageWarning("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, "Entity IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Post(TEntityViewModel entity)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Entity received is null !"
                    });
                }
                var reverseMapEntity = _mapper.Map<TEntity>(entity);
                reverseMapEntity.CreateDate = DateTime.Now;
                var row = await _genericService.CreateAsync(reverseMapEntity);
                if (row == null)
                {
                    _logger.LoggingMessageWarning("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, "ROW IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Post(TEntityViewModel entity)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Added Row is null !"
                    });
                }
                HelperCache<TEntity>.AddCache(row, _cache);
                var mapperRow = _mapper.Map<TEntityViewModel>(row);
                await _realTimeHub.Clients.All.SendAsync("Row Created !", mapperRow);
                return Ok(mapperRow);
            }
            catch (Exception ex)
            {
                _logger.LoggingMessageError("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "" + " - Post(TEntityViewModel entity)", ex, _hostEnvironment.ContentRootPath);
                return StatusCode(500, new
                {
                    Message = "Add Row failed !"
                });
            }
        }

        [HttpPost("AddRange")]
        public virtual async Task<IActionResult> Post(IList<TEntityViewModel> entities)
        {
            try
            {
                if (entities == null)
                {
                    _logger.LoggingMessageWarning("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, "Entity IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Post(IList<TEntityViewModel> entities)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Entity received is null !"
                    });
                }
                var reverseMapEntities = _mapper.Map<IList<TEntity>>(entities);
                foreach (var reverseMapEntity in reverseMapEntities)
                {
                    reverseMapEntity.CreateDate = DateTime.Now;
                }
                var rows = await _genericService.CreateAsync(reverseMapEntities);
                if (rows == null)
                {
                    _logger.LoggingMessageWarning("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, "ROWS IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Post(IList<TEntityViewModel> entities)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Added Rows is null !"
                    });
                }
                HelperCache<TEntity>.AddCache(rows, _cache);
                var mapperRows = _mapper.Map<IList<TEntityViewModel>>(rows).OrderByDescending(x => x.Id).ToList();
                await _realTimeHub.Clients.All.SendAsync("Rows Created !", mapperRows);
                return Ok(mapperRows);
            }
            catch (Exception ex)
            {
                _logger.LoggingMessageError("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "" + " - Post(IList<TEntityViewModel> entities)", ex, _hostEnvironment.ContentRootPath);
                return StatusCode(500, new
                {
                    Message = "Add Rows failed !"
                });
            }
        }
        #endregion

        #region UPDATE
        [HttpPut]
        public virtual async Task<IActionResult> Put(TEntityViewModel? entity)
        {
            try
            {
                if (entity == null)
                {
                    _logger.LoggingMessageWarning("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, "Entity IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Put(TEntityViewModel entity, int id)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Entity received is null !"
                    });
                }
                var reverseMapEntity = _mapper.Map<TEntity>(entity);
                //HelperCache<TEntity>.DeleteCache(reverseMapEntity, _cache);
                reverseMapEntity.UpdateDate = DateTime.Now;
                var row = await _genericService.UpdateAsync(reverseMapEntity);
                if (row == null)
                {
                    HelperCache<TEntity>.AddCache(reverseMapEntity, _cache);
                    _logger.LoggingMessageWarning("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, "ROW IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Put(TEntityViewModel entity, int id)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Updated Row is null !"
                    });
                }
                HelperCache<TEntity>.AddCache(row, _cache);
                var mapperRow = _mapper.Map<TEntityViewModel>(row);
                await _realTimeHub.Clients.All.SendAsync("Row Updated !", mapperRow);
                return Ok(mapperRow);
            }
            catch (Exception ex)
            {
                _logger.LoggingMessageError("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "" + " - Put(TEntityViewModel entity, int id)", ex, _hostEnvironment.ContentRootPath);
                return StatusCode(500, new
                {
                    Message = "Update Row failed !"
                });
            }
        }

        [HttpPut("UpdateRange")]
        public virtual async Task<IActionResult> Put(IList<TEntityViewModel> entities)
        {
            try
            {
                if (entities == null)
                {
                    _logger.LoggingMessageWarning("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, "Entity IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Put(IList<TEntityViewModel> entities)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Entity received is null !"
                    });
                }
                var reverseMapEntities = _mapper.Map<IList<TEntity>>(entities);
                //HelperCache<TEntity>.DeleteCache(reverseMapEntities, _cache);
                foreach (var reverseMapEntity in reverseMapEntities)
                {
                    reverseMapEntity.UpdateDate = DateTime.Now;
                }
                var rows = await _genericService.UpdateAsync(reverseMapEntities);
                if (rows == null)
                {
                    HelperCache<TEntity>.AddCache(reverseMapEntities, _cache);
                    _logger.LoggingMessageWarning("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, "ROWS IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Put(IList<TEntityViewModel> entities)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Updated Rows is null !"
                    });
                }
                HelperCache<TEntity>.AddCache(rows, _cache);
                var mapperRows = _mapper.Map<IList<TEntityViewModel>>(rows).OrderByDescending(x => x.Id).ToList();
                await _realTimeHub.Clients.All.SendAsync("Rows Updated !", mapperRows);
                return Ok(mapperRows);
            }
            catch (Exception ex)
            {
                _logger.LoggingMessageError("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "" + " - Put(IList<TEntityViewModel> entities)", ex, _hostEnvironment.ContentRootPath);
                return StatusCode(500, new
                {
                    Message = "Update Rows failed !"
                });
            }
        }
        #endregion

        #region DELETE
        [HttpDelete]
        public virtual async Task<IActionResult> Delete(TEntityViewModel? entity)
        {
            try
            {
                if (entity == null)
                {
                    _logger.LoggingMessageWarning("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, "Entity IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Delete(TEntityViewModel entity, int id)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Entity received is null !"
                    });
                }
                var reverseMapEntity = _mapper.Map<TEntity>(entity);
                var row = await _genericService.DeleteAsync(reverseMapEntity);
                if (row == null)
                {
                    _logger.LoggingMessageWarning("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, "ROW IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Delete(TEntityViewModel entity, int id)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Deleted Row is null !"
                    });
                }
                HelperCache<TEntity>.DeleteCache(row, _cache);
                var mapperRow = _mapper.Map<TEntityViewModel>(row);
                await _realTimeHub.Clients.All.SendAsync("Row Deleted !", mapperRow);
                return Ok(mapperRow);
            }
            catch (Exception ex)
            {
                _logger.LoggingMessageError("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "" + " - Delete(TEntityViewModel entity, int id)", ex, _hostEnvironment.ContentRootPath);
                return StatusCode(500, new
                {
                    Message = "Delete Row failed !"
                });
            }
        }

        [HttpDelete("DeleteRange")]
        public virtual async Task<IActionResult> Delete(IList<TEntityViewModel> entities)
        {
            try
            {
                if (entities == null)
                {
                    _logger.LoggingMessageWarning("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, "Entity IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Delete(IList<TEntityViewModel> entities)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Entity received is null !"
                    });
                }
                var reverseMapEntities = _mapper.Map<IList<TEntity>>(entities);
                var rows = await _genericService.DeleteAsync(reverseMapEntities);
                if (rows == null)
                {
                    _logger.LoggingMessageWarning("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, "ROWS IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Delete(IList<TEntityViewModel> entities)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Deleted Rows is null !"
                    });
                }
                HelperCache<TEntity>.DeleteCache(rows, _cache);
                var mapperRows = _mapper.Map<IList<TEntityViewModel>>(rows).OrderByDescending(x => x.Id).ToList();
                await _realTimeHub.Clients.All.SendAsync("Rows Deleted !", mapperRows);
                return Ok(mapperRows);
            }
            catch (Exception ex)
            {
                _logger.LoggingMessageError("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "" + " - Delete(IList<TEntityViewModel> entities)", ex, _hostEnvironment.ContentRootPath);
                return StatusCode(500, new
                {
                    Message = "Delete Rows failed !"
                });
            }
        }
        #endregion

        #region TRUNCATE
        [HttpGet("Truncate")]
        public virtual async Task<IActionResult> Truncate(bool isSqlite = false)
        {
            try
            {
                var ConnectionType = _configuration.GetSection("ConnectionType").Value;
                if (!string.IsNullOrEmpty(ConnectionType))
                {
                    var sql = string.Empty;
                    if (ConnectionType == "SQLITE")
                    {
                        sql = "DELETE FROM [tableName]";
                    }
                    else
                    {
                        sql = "TRUNCATE TABLE [tableName]";
                    }
                    var list = _genericService.GetEntitiesAsync(includes: null).ToList();
                    if (list == null)
                    {
                        _logger.LoggingMessageWarning("TriviaRoyaleGame", (int)HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Truncate(bool isSqlite = false)", _hostEnvironment.ContentRootPath);
                        return NotFound(new
                        {
                            Message = "List not found !",
                            StatusCode = (int)HttpStatusCode.NotFound + " - " + HttpStatusCode.NotFound.ToString()
                        });
                    }
                    await _genericService.TruncateAsync(sql, isSqlite);
                    HelperCache<TEntity>.DeleteCache(list!, _cache);
                    return Ok("Table truncated successfully !");
                }
                _logger.LoggingMessageWarning("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, "No database configured !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Truncate()", _hostEnvironment.ContentRootPath);
                return StatusCode(500,
                new
                {
                    Message = "Truncate failed !"
                });
            }
            catch (Exception ex)
            {
                _logger.LoggingMessageError("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "" + " - Truncate()", ex, _hostEnvironment.ContentRootPath);
                return StatusCode(500, new
                {
                    Message = "Truncate failed !"
                });
            }
        }
        #endregion

        #region CACHE DATA

        [HttpGet("CacheData")]
        public IActionResult Get()
        {
            try
            {
                var list = _genericService.GetEntitiesAsync().ToList();
                if (list == null)
                {
                    _logger.LoggingMessageWarning("TriviaRoyaleGame", (int)HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Get() Cache Data", _hostEnvironment.ContentRootPath);
                    return NotFound(new
                    {
                        Message = "List not found !",
                        StatusCode = (int)HttpStatusCode.NotFound + " - " + HttpStatusCode.NotFound.ToString()
                    });
                }
                var mappedList = _mapper.Map<IList<TEntityViewModel>>(list).OrderByDescending(x => x.Id).ToList();
                return Ok(mappedList);
            }
            catch (Exception ex)
            {
                _logger.LoggingMessageError("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "" + " - Get() Cache Data", ex, _hostEnvironment.ContentRootPath);
                return StatusCode(500, new
                {
                    Message = "Cache Data failed !"
                });
            }
        }
        #endregion

        #region SEND EMAILS
        [HttpPost("SendEmails")]
        public virtual async Task<IActionResult> SendEmails(EmailMessage emailMessage)
        {
            try
            {
                if (emailMessage == null)
                {
                    _logger.LoggingMessageWarning("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, "emailMessage IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - SendEmails(EmailMessage emailMessage)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "emailMessage received is null !"
                    });
                }
                var response = await _sendMailService.Send(emailMessage);
                if (response == null)
                {
                    _logger.LoggingMessageWarning("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, "RESPONSE IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - SendEmails(EmailMessage emailMessage)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Response is null !"
                    });
                }
                return Ok(new
                {
                    Message = response
                });
            }
            catch (Exception ex)
            {
                _logger.LoggingMessageError("TriviaRoyaleGame", (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "" + " - SendEmails(EmailMessage emailMessage)", ex, _hostEnvironment.ContentRootPath);
                return StatusCode(500, new
                {
                    Message = "Send Email failed !"
                });
            }
        }
        #endregion
    }
}
