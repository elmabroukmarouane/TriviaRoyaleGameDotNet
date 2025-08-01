﻿using TriviaRoyaleGame.Client.Domain.Models;
using TriviaRoyaleGame.Client.Domain.Models.LambdaManagement.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using TriviaRoyaleGame.Client.Domain.Models.Settings;
using System.Net;
using TriviaRoyaleGame.Client.Business.Extensions.Logging;
using TriviaRoyaleGame.Client.Business.Services.GenericService.Interface;
using TriviaRoyaleGame.Client.Business.Providers.Interfaces;

namespace TriviaRoyaleGame.Client.Business.Services.GenericService.Class
{
    public class GenericService<TEntityViewModel>(HttpClient httpClient, BaseSettingsApp? baseSettingsApp, ISourceAppProvider? SourceAppProvider) : IGenericService<TEntityViewModel> where TEntityViewModel : Entity
    {
        #region ATTRIBUTES
        protected readonly HttpClient _httpClient = httpClient ?? throw new ArgumentException(null, nameof(httpClient));
        protected readonly BaseSettingsApp? _baseSettingsApp = baseSettingsApp ?? throw new ArgumentException(null, nameof(baseSettingsApp));
        protected readonly ISourceAppProvider? _SourceAppProvider = SourceAppProvider ?? throw new ArgumentException(null, nameof(SourceAppProvider));
        #endregion

        #region READ
        public async Task<IList<TEntityViewModel>?> GetEntitiesAsync(string uri, string? token, string? includes = null)
        {
            try
            {
                SetTokenToHeader(token);
                var EntitiesResponse = await _httpClient.GetAsync(uri);
                var Entities = await EntitiesResponse.Content.ReadFromJsonAsync<IList<TEntityViewModel>>();
                if (Entities is not null)
                {
                    foreach (var entity in Entities)
                    {
                        entity.StatusCode = EntitiesResponse.StatusCode;
                    }
                }
                return Entities;
            }
            catch (Exception ex)
            {
                var log = LoggingMessaging.LoggingMessageError(
                    nameSpaceName: "TriviaRoyaleGame.Client.Business",
                    statusCodeInt: (int)HttpStatusCode.InternalServerError,
                    statusCode: HttpStatusCode.InternalServerError.ToString(),
                    actionName: "Services.Class.GenericService - GetEntitiesAsync()",
                    exception: ex
                );
                await _httpClient.PostAsJsonAsync(baseSettingsApp?.BaseUrlApiWebHttp + "Log", new ClientAppLogViewModel()
                {
                    Level = "Error",
                    Message = log,
                    Source = _SourceAppProvider?.GetSourceApp(),
                });
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<TEntityViewModel?> GetEntitiesAsync(string uri, string? token, int id, string? includes = null)
        {
            try
            {
                SetTokenToHeader(token);
                var EntitiesResponse = await _httpClient.GetAsync(uri);
                var Entity = await EntitiesResponse.Content.ReadFromJsonAsync<TEntityViewModel>();
                if (Entity is not null) Entity.StatusCode = EntitiesResponse?.StatusCode ?? HttpStatusCode.InternalServerError;
                return Entity;
            }
            catch (Exception ex)
            {
                var log = LoggingMessaging.LoggingMessageError(
                    nameSpaceName: "TriviaRoyaleGame.Client.Business",
                    statusCodeInt: (int)HttpStatusCode.InternalServerError,
                    statusCode: HttpStatusCode.InternalServerError.ToString(),
                    actionName: "Services.Class.GenericService - GetEntitiesAsync()",
                    exception: ex
                );
                await _httpClient.PostAsJsonAsync(baseSettingsApp?.BaseUrlApiWebHttp + "Log", new ClientAppLogViewModel()
                {
                    Level = "Error",
                    Message = log,
                    Source = _SourceAppProvider?.GetSourceApp(),
                });
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IList<TEntityViewModel>?> GetEntitiesAsync(string uri, string? token, FilterDataModel filterDataModel)
        {
            try
            {
                SetTokenToHeader(token);
                var response = await _httpClient.PostAsJsonAsync(uri, filterDataModel);
                var entitiesResponse = await response.Content.ReadFromJsonAsync<IList<TEntityViewModel>>();
                if (entitiesResponse is not null)
                {
                    foreach (var entity in entitiesResponse)
                    {
                        entity.StatusCode = response.StatusCode;
                    }
                }
                return entitiesResponse;
            }
            catch (Exception ex)
            {
                var log = LoggingMessaging.LoggingMessageError(
                    nameSpaceName: "TriviaRoyaleGame.Client.Business",
                    statusCodeInt: (int)HttpStatusCode.InternalServerError,
                    statusCode: HttpStatusCode.InternalServerError.ToString(),
                    actionName: "Services.Class.GenericService - GetEntitiesAsync()",
                    exception: ex
                );
                await _httpClient.PostAsJsonAsync(baseSettingsApp?.BaseUrlApiWebHttp + "Log", new ClientAppLogViewModel()
                {
                    Level = "Error",
                    Message = log,
                    Source = _SourceAppProvider?.GetSourceApp(),
                });
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region CREATE
        public async Task<TEntityViewModel?> CreateAsync(string uri, string? token, TEntityViewModel entity)
        {
            try
            {
                SetTokenToHeader(token);
                var response = await _httpClient.PostAsJsonAsync(uri, entity);
                var entityResponse = await response.Content.ReadFromJsonAsync<TEntityViewModel>();
                if (entityResponse is not null) entityResponse.StatusCode = response?.StatusCode ?? HttpStatusCode.InternalServerError;
                return entityResponse;
            }
            catch (Exception ex)
            {
                var log = LoggingMessaging.LoggingMessageError(
                    nameSpaceName: "TriviaRoyaleGame.Client.Business",
                    statusCodeInt: (int)HttpStatusCode.InternalServerError,
                    statusCode: HttpStatusCode.InternalServerError.ToString(),
                    actionName: "Services.Class.GenericService - CreateAsync()",
                    exception: ex
                );
                await _httpClient.PostAsJsonAsync(baseSettingsApp?.BaseUrlApiWebHttp + "Log", new ClientAppLogViewModel()
                {
                    Level = "Error",
                    Message = log,
                    Source = _SourceAppProvider?.GetSourceApp(),
                });
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IList<TEntityViewModel>?> CreateAsync(string uri, string? token, IList<TEntityViewModel> entities)
        {
            try
            {
                SetTokenToHeader(token);
                var response = await _httpClient.PostAsJsonAsync(uri, entities);
                var entitiesResponse = await response.Content.ReadFromJsonAsync<IList<TEntityViewModel>>();
                if (entitiesResponse is not null)
                {
                    foreach (var entity in entitiesResponse)
                    {
                        entity.StatusCode = response.StatusCode;
                    }
                }
                return entitiesResponse;
            }
            catch (Exception ex)
            {
                var log = LoggingMessaging.LoggingMessageError(
                    nameSpaceName: "TriviaRoyaleGame.Client.Business",
                    statusCodeInt: (int)HttpStatusCode.InternalServerError,
                    statusCode: HttpStatusCode.InternalServerError.ToString(),
                    actionName: "Services.Class.GenericService - CreateAsync()",
                    exception: ex
                );
                await _httpClient.PostAsJsonAsync(baseSettingsApp?.BaseUrlApiWebHttp + "Log", new ClientAppLogViewModel()
                {
                    Level = "Error",
                    Message = log,
                    Source = _SourceAppProvider?.GetSourceApp(),
                });
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region UPDATE
        public async Task<TEntityViewModel?> UpdateAsync(string uri, string? token, TEntityViewModel entity)
        {
            try
            {
                SetTokenToHeader(token);
                var response = await _httpClient.PutAsJsonAsync(uri, entity);
                var entityResponse = await response.Content.ReadFromJsonAsync<TEntityViewModel>();
                if (entityResponse is not null) entityResponse.StatusCode = response?.StatusCode ?? HttpStatusCode.InternalServerError;
                return entityResponse;
            }
            catch (Exception ex)
            {
                var log = LoggingMessaging.LoggingMessageError(
                    nameSpaceName: "TriviaRoyaleGame.Client.Business",
                    statusCodeInt: (int)HttpStatusCode.InternalServerError,
                    statusCode: HttpStatusCode.InternalServerError.ToString(),
                    actionName: "Services.Class.GenericService - UpdateAsync()",
                    exception: ex
                );
                await _httpClient.PostAsJsonAsync(baseSettingsApp?.BaseUrlApiWebHttp + "Log", new ClientAppLogViewModel()
                {
                    Level = "Error",
                    Message = log,
                    Source = _SourceAppProvider?.GetSourceApp(),
                });
                throw new Exception(ex.Message, ex);
            }

        }

        public async Task<IList<TEntityViewModel>?> UpdateAsync(string uri, string? token, IList<TEntityViewModel> entities)
        {
            try
            {
                SetTokenToHeader(token);
                var response = await _httpClient.PutAsJsonAsync(uri, entities);
                var entitiesResponse = await response.Content.ReadFromJsonAsync<IList<TEntityViewModel>>();
                if (entitiesResponse is not null)
                {
                    foreach (var entity in entitiesResponse)
                    {
                        entity.StatusCode = response.StatusCode;
                    }
                }
                return entitiesResponse;
            }
            catch (Exception ex)
            {
                var log = LoggingMessaging.LoggingMessageError(
                    nameSpaceName: "TriviaRoyaleGame.Client.Business",
                    statusCodeInt: (int)HttpStatusCode.InternalServerError,
                    statusCode: HttpStatusCode.InternalServerError.ToString(),
                    actionName: "Services.Class.GenericService - UpdateAsync()",
                    exception: ex
                );
                await _httpClient.PostAsJsonAsync(baseSettingsApp?.BaseUrlApiWebHttp + "Log", new ClientAppLogViewModel()
                {
                    Level = "Error",
                    Message = log,
                    Source = _SourceAppProvider?.GetSourceApp(),
                });
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region DELETE
        public async Task<TEntityViewModel?> DeleteAsync(string uri, string? token, TEntityViewModel entity)
        {
            try
            {
                SetTokenToHeader(token);
                var entityJsonSerialize = JsonSerializer.Serialize(entity);
                var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, uri)
                {
                    Content = new StringContent(entityJsonSerialize, Encoding.UTF8, "application/json")
                });
                var entityResponse = await response.Content.ReadFromJsonAsync<TEntityViewModel>();
                if (entityResponse is not null) entityResponse.StatusCode = response?.StatusCode ?? HttpStatusCode.InternalServerError;
                return entityResponse;
            }
            catch (Exception ex)
            {
                var log = LoggingMessaging.LoggingMessageError(
                    nameSpaceName: "TriviaRoyaleGame.Client.Business",
                    statusCodeInt: (int)HttpStatusCode.InternalServerError,
                    statusCode: HttpStatusCode.InternalServerError.ToString(),
                    actionName: "Services.Class.GenericService - DeleteAsync()",
                    exception: ex
                );
                await _httpClient.PostAsJsonAsync(baseSettingsApp?.BaseUrlApiWebHttp + "Log", new ClientAppLogViewModel()
                {
                    Level = "Error",
                    Message = log,
                    Source = _SourceAppProvider?.GetSourceApp(),
                });
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IList<TEntityViewModel>?> DeleteAsync(string uri, string? token, IList<TEntityViewModel> entities)
        {
            try
            {
                SetTokenToHeader(token);
                var entitiesJsonSerialize = JsonSerializer.Serialize(entities);
                var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, uri)
                {
                    Content = new StringContent(entitiesJsonSerialize, Encoding.UTF8, "application/json")
                });
                var entitiesResponse = await response.Content.ReadFromJsonAsync<IList<TEntityViewModel>>();
                if (entitiesResponse is not null)
                {
                    foreach (var entity in entitiesResponse)
                    {
                        entity.StatusCode = response.StatusCode;
                    }
                }
                return entitiesResponse;
            }
            catch (Exception ex)
            {
                var log = LoggingMessaging.LoggingMessageError(
                    nameSpaceName: "TriviaRoyaleGame.Client.Business",
                    statusCodeInt: (int)HttpStatusCode.InternalServerError,
                    statusCode: HttpStatusCode.InternalServerError.ToString(),
                    actionName: "Services.Class.GenericService - DeleteAsync()",
                    exception: ex
                );
                await _httpClient.PostAsJsonAsync(baseSettingsApp?.BaseUrlApiWebHttp + "Log", new ClientAppLogViewModel()
                {
                    Level = "Error",
                    Message = log,
                    Source = _SourceAppProvider?.GetSourceApp(),
                });
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region FILTER
        public bool FilterFunc(TEntityViewModel? entity, string? tableSearchString)
        {
            if (entity == null) return false;
            if (string.IsNullOrWhiteSpace(tableSearchString))
                return true;

            foreach (var prop in entity.GetType().GetProperties())
            {
                var value = prop.GetValue(entity)?.ToString();
                if (!string.IsNullOrWhiteSpace(value) && value.ToLower().Contains(tableSearchString.ToLower(), StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region ADDTOKEN
        private void SetTokenToHeader(string? token)
        {
            if (token != null)
            {
                if (!_httpClient.DefaultRequestHeaders.Contains("Authorization"))
                {
                    _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                }
            }
        }
        #endregion
    }
}
