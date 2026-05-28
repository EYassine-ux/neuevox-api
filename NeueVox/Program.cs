using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using NeueVox.Model.NeuevoxModel.Context;
using NeueVox.Repository;
using NeueVox.Repository.MongoDbRepository;
using NeueVox.Service;
using NeueVox.Service.MongoDbService;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
  options.AddPolicy(name: myAllowSpecificOrigins,
      policy =>
      {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
      });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var mongoConnectionString = builder.Configuration["MongoDbSettings:ConnectionString"];
var mongoDbName = builder.Configuration["MongoDbSettings:DatabaseName"];

var mongoClient = new MongoClient(mongoConnectionString);

builder.Services.AddDbContext<MongoDbContext>(options =>
  options.UseMongoDB(mongoClient, mongoDbName!));

builder.Services.AddDbContext<NeueVoxContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddControllers()
  .AddJsonOptions(options =>
  {
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());

    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
  });

// authorize Bearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
    options.TokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuer = true,
      ValidIssuer = builder.Configuration["AppSettings:Issuer"],
      ValidateAudience = true,
      ValidAudience = builder.Configuration["AppSettings:Audience"],
      ValidateLifetime = true,
      IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),
      ValidateIssuerSigningKey = true,
    };
  });

// Adding the scope for the repositories
// For postgres
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IEvaluationRepository, EvaluationRepository>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<IGradeRepository, GradeRepository>();
builder.Services.AddScoped<IStudentSubmissionRepository, StudentSubmissionRepository>();
builder.Services.AddScoped<IProgramRepository, ProgramRepository>();
builder.Services.AddScoped<IStudentClassRepository, StudentClassRepository>();

//for mongodb
builder.Services.AddScoped<IQuoteRepository, QuoteRepository>();
builder.Services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();

//adding the scope for the services
//for postgres
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<IProfessorService, ProfessorService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IEvaluationService, EvaluationService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<IGradeService, GradeService>();
builder.Services.AddScoped<IStudentSubmissionService, StudentSubmissionService>();
builder.Services.AddScoped<IProgramService, ProgramService>();
builder.Services.AddScoped<IStudentClassService, StudentClassService>();

//for mongodb
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
builder.Services.AddScoped<IQuoteService, QuoteService>();


builder.Services.AddRouting(options => options.LowercaseUrls = true);


var app = builder.Build();

//allow migration on docker
using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;
  try
  {
    var context = services.GetRequiredService<NeueVoxContext>();
    context.Database.Migrate();
  }
  catch (Exception ex)
  {
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred seeding the DB.");
  }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.MapScalarApiReference(options =>
    {
      options.Servers = new[]
      {
        new ScalarServer("http://localhost:8080", "Local Docker"),
        new ScalarServer("https://api.neuevox.net/", "Render Production"),
        new ScalarServer("https://localhost:7242", "local windows")
      };
    }
    );
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(myAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

