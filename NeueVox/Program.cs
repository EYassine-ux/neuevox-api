    using System.Text;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using NeueVox.Model.NeuevoxModel.Context;
    using NeueVox.Repository;
    using NeueVox.Service;
    using Scalar.AspNetCore;

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();

    var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

    builder.Services.AddCors(options =>{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });

    });

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddDbContext<NeueVoxContext>(options => options.UseNpgsql(connectionString));
    builder.Services.AddControllers()
      .AddJsonOptions(options =>
      {
        // This line tells .NET to show the name of the Enum in JSON, not the number
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
      });


    //authorize Bearer
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




    //adding the scope for the repositories
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IStudentRepository, StudentRepository>();
    builder.Services.AddScoped<IClassRepository, ClassRepository>();
    builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();
    builder.Services.AddScoped<ICourseRepository, CourseRepository>();
    builder.Services.AddScoped<IEvaluationRepository, EvaluationRepository>();
    builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();

    //adding the scope for the services
    builder.Services.AddScoped<IUserService,UserService>();
    builder.Services.AddScoped<IStudentService,StudentService>();
    builder.Services.AddScoped<IClassService,ClassService>();
    builder.Services.AddScoped<IProfessorService,ProfessorService>();
    builder.Services.AddScoped<ICourseService,CourseService>();
    builder.Services.AddScoped<IEvaluationService, EvaluationService>();
    builder.Services.AddScoped<IDocumentService, DocumentService>();
    builder.Services.AddScoped<IAuthService, AuthService>();

    //adding the controllers
    builder.Services.AddControllers();
    builder.Services.AddRouting(options => options.LowercaseUrls = true);
    builder.Services.AddControllers().AddJsonOptions(options =>
    {
      options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

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
              new Scalar.AspNetCore.ScalarServer("https://localhost:7242", "Local Docker"),
              new Scalar.AspNetCore.ScalarServer("https://api.neuevox.net/", "Render Production")
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

