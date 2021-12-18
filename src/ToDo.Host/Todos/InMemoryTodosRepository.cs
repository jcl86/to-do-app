namespace ToDo.Host;

public class InMemoryTodosRepository : ITodosRepository
{
    private readonly List<Todo> todos;

    public InMemoryTodosRepository()
    {
        this.todos = new List<Todo>();
    }

    public async Task<Todo> GetTodo(int todoId)
    {
        var result = todos.SingleOrDefault(x => x.Id == todoId);
        return await Task.FromResult(result);
    }

    public async Task<IEnumerable<Todo>> GetTodos()
    {
        return await Task.FromResult(todos);
    }

    public async Task<Todo> Save(Todo entity)
    {
        var searched = todos.SingleOrDefault(x => x.Id == entity.Id);
        if (searched is not null)
        {
            searched.UpdateContent(entity.Content);
            searched.UpdateStatus(entity.Done);
            return await Task.FromResult(entity);
        }

        entity = new Todo(entity.Content, entity.Done)
        {
            Id = todos.Any() ? todos.Max(x => x.Id) + 1 : 1,
        };
        todos.Add(entity);
        return await Task.FromResult(entity);
    }

    public async Task Delete(int id)
    {
        todos.RemoveAll(x => x.Id == id);
        await Task.CompletedTask;
    }
}

