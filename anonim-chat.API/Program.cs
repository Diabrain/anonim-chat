using anonim_chat.API.Context;
using anonim_chat.API.Extensions;
using anonim_chat.API.Managers;
using anonim_chat.API.Middleware;
using anonim_chat.API.Models;
using anonim_chat.API.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenJwt();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSnakeCaseNamingConvention()
        .UseInMemoryDatabase("chats");

});



builder.Services.AddJwt(builder.Configuration);

builder.Services.AddScoped<IValidator<CreateUserModel>, CreateUserValidator>();
builder.Services.AddScoped<IChatManager, ChatManager>();
builder.Services.AddScoped<IUserProvider, UserProvider>();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IJwtTokenManager, JwtTokenManager>();
builder.Services.AddHttpContextAccessor();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseErrorHandlerMiddleware();
app.MapControllers();
app.Run();