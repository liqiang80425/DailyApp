using Microsoft.EntityFrameworkCore;
using Study.API.DataModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(m=>{ 
    // xml文件路径
    string path=AppContext.BaseDirectory+ "Study.API.xml";
    //显示注释
    m.IncludeXmlComments(path,true);
});

builder.Services.AddDbContext<StudyDbContext>(m => m.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
