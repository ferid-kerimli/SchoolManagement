using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Abstraction;
using SchoolManagement.Abstraction.Repositories.SchoolRepository;
using SchoolManagement.Abstraction.Repositories.StudentRepository;
using SchoolManagement.Abstraction.Services;
using SchoolManagement.Context;
using SchoolManagement.Entities.Identity;
using SchoolManagement.Extentions;
using SchoolManagement.Implementation;
using SchoolManagement.Implementation.Repositories.EntitiesRepositories;
using SchoolManagement.Implementation.Service;
using SchoolManagement.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>(op =>
{
    op.UseSqlServer(builder.Configuration.GetConnectionString("SchoolDB"));
});

builder.Services.AddIdentity<User, Role>(options =>
{

}).AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders(); 

builder.Services.AddAutoMapper(typeof(MapperProfile));

//services
builder.Services.AddScoped<ISchoolService, SchoolService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//repositories
builder.Services.AddScoped<ISchoolRepository, SchoolRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.ConfigureExceptionHandler();
app.MapControllers();

app.Run();