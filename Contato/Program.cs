using Contato.Contexto;
using Contato.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

//Conectar com o banco de dados
builder.Services.AddDbContext<Contexto>
    (options => options.UseSqlServer(
        "Server=tcp:contato.database.windows.net,1433;Initial Catalog=Contatos;Persist Security Info=False;User ID=rubens;Password=Ru@19051997;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseSwagger();

//Adicionar contato da pessoa
app.MapPost("AdicionaContato", async (Pessoa pessoa, Contexto contexto) =>
{
    contexto.Pessoa.Add(pessoa);
    await contexto.SaveChangesAsync();
});

//Excluir contato da pessoa
app.MapDelete("ExcluirContato/{id}", async (int id , Contexto contexto) =>
{
    var contatoExcluido = await contexto.Pessoa.FirstOrDefaultAsync(p => p.Id == id);
    if (contatoExcluido != null)
    {
        contexto.Pessoa.Remove(contatoExcluido);
        await contexto.SaveChangesAsync();
    }
  
});

//Lista contato de pessoa
app.MapPost("ListaContatos", async (Contexto contexto) =>
{
   return await contexto.Pessoa.ToListAsync();   
});

//Pesquisa contato
app.MapGet("ObterContato/{id}", async (int id, Contexto contexto) =>
{
    return await contexto.Pessoa.FirstOrDefaultAsync(p => p.Id == id);
});
app.UseSwaggerUI();
app.Run();
