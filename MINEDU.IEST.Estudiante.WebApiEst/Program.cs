using IDCL.AVGUST.SIP.Manager.MappingDto;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using MINEDU.IEST.Estudiante.Inf_Apis.Extension;
using MINEDU.IEST.Estudiante.Inf_Utils.Dtos;
using MINEDU.IEST.Estudiante.Inf_Utils.Filters;
using MINEDU.IEST.Estudiante.Inf_Utils.Helpers.EmailSender;
using MINEDU.IEST.Estudiante.Inf_Utils.Helpers.FileManager;
using MINEDU.IEST.Estudiante.WebApiEst.Helpers;
using Newtonsoft.Json.Serialization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

var configuration = builder.Configuration;
var CurrentEnvironment = builder.Environment;

//Configuracion de correo-------------------------------------/
var emailConfig = configuration.GetSection("MailSettings").Get<MailSettings>(opt => opt.BindNonPublicProperties = true);
builder.Services.AddSingleton(emailConfig);
builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

/*------------------------------------------------------------*/

//Configuracion ruta de los recursos-------------------------------------/
var recursos = configuration.GetSection("ResourceDto").Get<ResourceDto>(opt => opt.BindNonPublicProperties = true);
builder.Services.AddSingleton(recursos);

/*------------------------------------------------------------*/

//Configuracion para el BackEnd-------------------------------------/
BackEndConfig backEndConfig = configuration.GetSection("BackEndConfig").Get<BackEndConfig>();

//Habilitando Authorize
//builder.Services.AddControllers(o => o.Filters.Add(new AuthorizeFilter()));

//Auto Mapper
builder.Services.AddAutoMapper(typeof(AutoMapperHelper).GetTypeInfo().Assembly);


//EF Core - Inyeccion de Dependencia.
builder.Services.AddRepositories(opt => opt.ConnectionString = backEndConfig.BdSqlServer);
builder.Services.AddRepositoriesCalculator(opt => opt.ConnectionString = backEndConfig.BdSqlServer);
builder.Services.AddSecurityApi(opt => opt.ConnectionString = backEndConfig.BdSqlServer);
builder.Services.AddManager();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddStorageManager(opt => opt.Type = StorageType.FileStorage);


//Servicio de acceso al contexto
builder.Services.AddHttpContextAccessor();


//Inyectando CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(backEndConfig.NombrePoliticaCors, b =>
    {
        b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

//Filters
builder.Services.AddScoped<ModelValidationAttribute>();

builder.Services.AddControllers()
.AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
})
.ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Config Swagger
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Web Api Gestion Estudiantes IETS - MINEDU",
        Version = "v1",
        Description = "Recurso de Web API para la Gestión del Estudiante de las IETS",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "MINEDU",
            Email = "danielitolozano85@gmail.com",
            Url = new Uri("https://twitter.com/IsraelCamoens"),
        },
        License = new OpenApiLicense
        {
            Name = "Gestion de Estudiante API LICX",
            Url = new Uri("https://example.com/license"),
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opt.IncludeXmlComments(xmlPath);

});


// Configure the HTTP request pipeline.
var app = builder.Build();


app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), $"{CurrentEnvironment.WebRootPath}/swagger-ui")),
    RequestPath = "/swagger-ui"
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("../swagger/v1/swagger.json", "Web Api Avgust v1");
        c.InjectStylesheet("/swagger-ui/custom.css");
    });
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/ws-core/swagger/v1/swagger.json", "Web Api Avgust v1"));
}


app.ConfigureCustomExceptionMiddleware();
app.UseRouting();
app.UseCors(backEndConfig.NombrePoliticaCors);
//app.UseAuthentication();
//app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
