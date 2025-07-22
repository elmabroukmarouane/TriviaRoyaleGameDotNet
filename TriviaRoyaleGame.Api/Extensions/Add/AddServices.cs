using TriviaRoyaleGame.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore;
using TriviaRoyaleGame.Business.Redis.Interface;
using TriviaRoyaleGame.Business.Redis.Class;
using TriviaRoyaleGame.Business.Services.Interfaces;
using TriviaRoyaleGame.Business.Services.Classes;
using TriviaRoyaleGame.Infrastructure.DatabaseContext.DbContextTriviaRoyaleGame;
using TriviaRoyaleGame.UnitOfWork.UnitOfWork.Interface;
using TriviaRoyaleGame.UnitOfWork.UnitOfWork.Class;
using TriviaRoyaleGame.Business.Cqrs.Commands.Interfaces;
using TriviaRoyaleGame.Business.Cqrs.Commands.Classes;
using TriviaRoyaleGame.Business.Cqrs.Queries.Classes;
using TriviaRoyaleGame.Business.Cqrs.Queries.Interfaces;
using TriviaRoyaleGame.Api.RealTime.Class;
using TriviaRoyaleGame.Business.Services.SendEmails.Classe;
using TriviaRoyaleGame.Business.Services.SendEmails.Interface;
using MailKit.Net.Smtp;

namespace TriviaRoyaleGame.Api.Extensions.Add;
public static class AddServices
{
    public static void AddSERVICES(this IServiceCollection self, IConfiguration configuration, IHostEnvironment hostEnvironment)
    {
        self.AddSingleton(configuration);
        self.AddSingleton(hostEnvironment);

        self.AddTransient<IUnitOfWork<DbContextTriviaRoyaleGame>, UnitOfWork<DbContextTriviaRoyaleGame>>();

        self.AddTransient<IGenericCreateCommand<Question>, GenericCreateCommand<Question>>();
        self.AddTransient<IGenericUpdateCommand<Question>, GenericUpdateCommand<Question>>();
        self.AddTransient<IGenericGetEntitiesQuery<Question>, GenericGetEntitiesQuery<Question>>();
        self.AddTransient<IGenericDeleteQuery<Question>, GenericDeleteQuery<Question>>();
        self.AddTransient<IGenericTruncateQuery<Question>, GenericTruncateQuery<Question>>();
        self.AddTransient<IGenericService<Question>, GenericService<Question>>();

        self.AddTransient<IGenericCreateCommand<Member>, GenericCreateCommand<Member>>();
        self.AddTransient<IGenericUpdateCommand<Member>, GenericUpdateCommand<Member>>();
        self.AddTransient<IGenericGetEntitiesQuery<Member>, GenericGetEntitiesQuery<Member>>();
        self.AddTransient<IGenericDeleteQuery<Member>, GenericDeleteQuery<Member>>();
        self.AddTransient<IGenericTruncateQuery<Member>, GenericTruncateQuery<Member>>();
        self.AddTransient<IGenericService<Member>, GenericService<Member>>();

        self.AddTransient<IGenericCreateCommand<ClientAppLog>, GenericCreateCommand<ClientAppLog>>();
        self.AddTransient<IGenericUpdateCommand<ClientAppLog>, GenericUpdateCommand<ClientAppLog>>();
        self.AddTransient<IGenericGetEntitiesQuery<ClientAppLog>, GenericGetEntitiesQuery<ClientAppLog>>();
        self.AddTransient<IGenericDeleteQuery<ClientAppLog>, GenericDeleteQuery<ClientAppLog>>();
        self.AddTransient<IGenericTruncateQuery<ClientAppLog>, GenericTruncateQuery<ClientAppLog>>();
        self.AddTransient<IGenericService<ClientAppLog>, GenericService<ClientAppLog>>();

        self.AddTransient<IUserCreateCommand, UserCreateCommand>();
        self.AddTransient<IUserUpdateCommand, UserUpdateCommand>();
        self.AddTransient<IGenericGetEntitiesQuery<User>, GenericGetEntitiesQuery<User>>();
        self.AddTransient<IGenericDeleteQuery<User>, GenericDeleteQuery<User>>();
        self.AddTransient<IUserService, UserService>();

        self.AddTransient<ISmtpClient, SmtpClient>();
        self.AddTransient<ISendMailService, SendMailService>();

        self.AddTransient<IRedisService, RedisService>();

        self.AddTransient<ICryptoService, CryptoService>();

        self.AddTransient<RealTimeHub>();

        self.AddSingleton<IRedisConnectionFactory>(new RedisConnectionFactory(configuration.GetConnectionString("RedisConnection")!));
    }
}
