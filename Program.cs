using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using booklend.Database;
using booklend.Application.Services;
using booklend.Application.Services.Token;
using booklend.Application.Interfaces;
using booklend.Repository;
using booklend.Repository.Interfaces;

// ────────────────────────────────────────────────────────────────
// 1. Carrega as variáveis do .env *antes* de criar o builder
// ────────────────────────────────────────────────────────────────
Env.Load();                // procura um .env na pasta raiz do projeto
// Se o arquivo estiver em outro lugar: Env.Load("caminho/do/arquivo.env");

var builder = WebApplication.CreateBuilder(args);

// ────────────────────────────────────────────────────────────────
// 2. Injeta no IConfiguration o que veio do .env
//    (DotNetEnv coloca as variáveis no processo, mas sem "__",
//     então mapeamos manualmente para a árvore do IConfiguration)
// ────────────────────────────────────────────────────────────────
var connString =
    $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
    $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
    $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
    $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
    $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}";

builder.Configuration["ConnectionStrings:DefaultConnection"] = connString;
builder.Configuration["Jwt:Key"]        = Environment.GetEnvironmentVariable("JWT_KEY");
builder.Configuration["Jwt:Issuer"]     = Environment.GetEnvironmentVariable("JWT_ISSUER");
builder.Configuration["Jwt:Audience"]   = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
builder.Configuration["Jwt:ExpiresInDays"] =
    Environment.GetEnvironmentVariable("JWT_EXPIRES_IN_DAYS");

// ────────────────────────────────────────────────────────────────
// 3. Serviços de infraestrutura
// ────────────────────────────────────────────────────────────────
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        var jwt = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer              = jwt.Issuer,
            ValidAudience            = jwt.Audience,
            IssuerSigningKey         = new SymmetricSecurityKey(
                                           Encoding.UTF8.GetBytes(jwt.Key))
        };
    });

// ────────────────────────────────────────────────────────────────
// 4. Demais serviços (repositórios, aplicações, etc.)
// ────────────────────────────────────────────────────────────────
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookstoreRepository, BookstoreRepository>();
builder.Services.AddScoped<IBookstoreBookRepository, BookstoreBookRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<BookstoreService>();
builder.Services.AddScoped<BookstoreBookService>();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<RentalService>();
builder.Services.AddScoped<RoleService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ────────────────────────────────────────────────────────────────
// 5. Pipeline HTTP
// ────────────────────────────────────────────────────────────────
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();   //  ← Faltava para validar o JWT
app.UseAuthorization();

app.MapControllers();
app.Run();
