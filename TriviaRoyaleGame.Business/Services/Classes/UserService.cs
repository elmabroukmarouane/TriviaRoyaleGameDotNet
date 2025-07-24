﻿using TriviaRoyaleGame.Business.Cqrs.Queries.Interfaces;
using TriviaRoyaleGame.Business.Helpers;
using TriviaRoyaleGame.Business.Services.Interfaces;
using TriviaRoyaleGame.Infrastructure.Models.Classes;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using TriviaRoyaleGame.Business.Cqrs.Commands.Interfaces;

namespace TriviaRoyaleGame.Business.Services.Classes
{
    public class UserService : IUserService
    {
        #region ATTRIBUTES
        protected readonly IGenericGetEntitiesQuery<User> _genericGetEntitiesQuery;
        protected readonly IUserCreateCommand _userCreateCommand;
        protected readonly IUserUpdateCommand _userUpdateCommand;
        protected readonly IGenericDeleteQuery<User> _genericDeleteQuery;
        #endregion

        #region CONSTRUCTOR
        public UserService(
            IUserCreateCommand userCreateCommand,
            IUserUpdateCommand userUpdateCommand,
            IGenericGetEntitiesQuery<User> genericGetEntitiesQuery,
            IGenericDeleteQuery<User> genericDeleteQuery)
        {
            _userCreateCommand = userCreateCommand ?? throw new ArgumentException(null, nameof(userCreateCommand));
            _userUpdateCommand = userUpdateCommand ?? throw new ArgumentException(null, nameof(userUpdateCommand));
            _genericGetEntitiesQuery = genericGetEntitiesQuery ?? throw new ArgumentException(null, nameof(genericGetEntitiesQuery));
            _genericDeleteQuery = genericDeleteQuery ?? throw new ArgumentException(null, nameof(genericDeleteQuery));
        }
        #endregion

        #region READ
        public IQueryable<User> GetEntitiesAsync(Expression<Func<User, bool>>? expression = null, Func<IQueryable<User>, IOrderedQueryable<User>>? orberBy = null, string? includes = null, string splitChar = ",", bool disableTracking = true, int take = 0, int offset = 0, bool inDatabase = false)
        {
            return _genericGetEntitiesQuery.Handle(expression, orberBy, includes, splitChar, disableTracking, take, offset, inDatabase);
        }

        public async Task<User?> GetEntitiesAsync(User entity)
        {
            return await _genericGetEntitiesQuery.Handle(entity);
        }
        #endregion

        #region CREATE
        public async Task<User?> CreateAsync(User entity)
        {
            return await _userCreateCommand.Handle(entity);
        }

        public async Task<IList<User>?> CreateAsync(IList<User> entities)
        {
            return await _userCreateCommand.Handle(entities);
        }
        #endregion

        #region UPDATE
        public async Task<User?> UpdateAsync(User entity)
        {
            return await _userUpdateCommand.Handle(entity);
        }

        public async Task<IList<User>?> UpdateAsync(IList<User> entities)
        {
            return await _userUpdateCommand.Handle(entities);
        }
        #endregion

        #region DELETE
        public async Task<User?> DeleteAsync(User entity)
        {
            return await _genericDeleteQuery.Handle(entity);
        }

        public async Task<IList<User>?> DeleteAsync(IList<User> entities)
        {
            return await _genericDeleteQuery.Handle(entities);
        }
        #endregion

        #region AUTHENTICATION
        public async Task<User?> Authenticate(User user, bool inDatabase = false)
        {
            if (user is null) return null;
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password)) return null;
            var attempingUser = _genericGetEntitiesQuery.Handle(expression: x => x.Email!.ToLower().Trim() == user.Email, inDatabase: inDatabase).SingleOrDefault();
            if (attempingUser is null) return null;
            user = Helper.EncryptPassword(user);
            if (attempingUser.Email == user.Email && attempingUser.Password == user.Password)
            {
                attempingUser.IsOnLine = true;
                await _userUpdateCommand.Handle(attempingUser, true);
                return attempingUser;
            }
            return null;
        }

        public async Task<bool> Logout(User user, bool inDatabase = false)
        {
            if (user is null) return false;
            var loggedUser = _genericGetEntitiesQuery.Handle(expression: x => x.Id == user.Id && x.Email == user.Email, inDatabase: inDatabase).SingleOrDefault();
            if (loggedUser is null) return false;
            loggedUser.IsOnLine = false;
            await _userUpdateCommand.Handle(loggedUser, true);
            return true;
        }
        #endregion

        #region TOKEN
        public string? CreateToken(object user, string role, string keyString, string issuerString, string audienceString, int expireTokenDays = 1)
        {
            if (user != null && keyString != null && issuerString != null && audienceString != null)
            {
                var claims = new[] {
                    new Claim("user", JsonSerializer.Serialize(user)),
                    new Claim(ClaimTypes.Role, role)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
                var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var token = new JwtSecurityToken(
                    issuer: issuerString,
                    audience: audienceString,
                    claims: claims,
                    expires: DateTime.Now.AddDays(expireTokenDays),
                    signingCredentials: signingCredentials);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return null;
        }
        #endregion
    }
}