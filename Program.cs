var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Mesmos dados da Aula 1: uma lista simples em memória.
var tarefas = new List<Tarefa>
{
    new(1, "Instalar o Docker", true),
    new(2, "Escrever o Dockerfile da API", false),
    new(3, "Escrever o Dockerfile do site", false),
    new(4, "Subir tudo com docker compose up", false)
};

app.MapGet("/api/tarefas", () => tarefas);

app.MapPost("/api/tarefas", (NovaTarefa nova) =>
{
    var tarefa = new Tarefa(tarefas.Count + 1, nova.Titulo, false);
    tarefas.Add(tarefa);
    return Results.Created($"/api/tarefas/{tarefa.Id}", tarefa);
});

// Mostra de onde a resposta veio: prova que o ambiente é o do container.
app.MapGet("/api/info", () => new
{
    maquina = Environment.MachineName,
    dotnet = Environment.Version.ToString(),
    ambiente = app.Environment.EnvironmentName
});

//teste

app.MapGet("/health", () => Results.Ok("ok"));

app.Run();

record Tarefa(int Id, string Titulo, bool Concluida);
record NovaTarefa(string Titulo);
