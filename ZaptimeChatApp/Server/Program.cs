using Blazored.Toast;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ZaptimeChatApp.Server;
using ZaptimeChatApp.Server.Data;
using ZaptimeChatApp.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(options => 
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = (context) =>
            {
                if (context.Request.Path.StartsWithSegments("/hubs/zaptime-chat"))
                {
                    var jwt = context.Request.Query["access_token"];
                    if(!string.IsNullOrWhiteSpace(jwt))
                    {
                        context.Token = jwt;
                    }
                }
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddTransient<TokenService>();
builder.Services.AddDbContext<ZaptimeChatDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("ZaptimeChatDB")));
builder.Services.AddSignalR();
builder.Services.AddBlazoredToast();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapHub<ZaptimeChatHub>("/hubs/zaptime-chat");
app.MapFallbackToFile("index.html");

app.Run();
