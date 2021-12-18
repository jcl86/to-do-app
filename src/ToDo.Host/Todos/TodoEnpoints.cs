namespace ToDo.Host;

using Microsoft.AspNetCore.Mvc;
using System.Linq;

public static class TodoEnpoints
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder endpoints)
    {

        endpoints.MapGet("/todos", async ([FromServices] ITodosRepository repository) =>
        {
            var result = (await repository.GetTodos()).Select(x => x.AsModel());
            return result;
        });

        endpoints.MapGet("/todos/{todoId:int}", async (int todoId, [FromServices] ITodosRepository repository) =>
        {
            var entity = await repository.GetTodo(todoId);
            if (entity is null)
            {
                return Results.NotFound("Todo not found");
            }
            return Results.Ok(entity.AsModel());
        });

        endpoints.MapPost("/todos", async (Model.CreateTodo dto, [FromServices] ITodosRepository repository) =>
        {
            var todo = new Todo(dto.Content);
            var result = (await repository.Save(todo)).AsModel();
            return Results.Created($"/todos/{todo.Id}", result);
        });

        endpoints.MapPost("/todos/{todoId:int}/complete", async (int todoId, [FromServices] ITodosRepository repository) =>
        {
            var todo = await repository.GetTodo(todoId);
            todo.MarkAsDone();
            await repository.Save(todo);
            return Results.NoContent();
        });

        endpoints.MapPut("/todos/{todoId:int}", async (int todoId, Model.UpdateTodo dto, [FromServices] ITodosRepository repository) =>
        {
            var todo = await repository.GetTodo(todoId);
            todo.UpdateContent(dto.Content);
            await repository.Save(todo);
            return Results.NoContent();
        });

        endpoints.MapDelete("/todos/{todoId:int}", async (int todoId, [FromServices] ITodosRepository repository) =>
        {
            await repository.Delete(todoId);
            return Results.NoContent();
        });

        return endpoints;
    }

}