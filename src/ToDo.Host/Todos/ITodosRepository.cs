namespace ToDo.Host;

public interface ITodosRepository
{
    Task<IEnumerable<Todo>> GetTodos();
    Task<Todo> GetTodo(int todoId);
    Task<Todo> Save(Todo entity);
    Task Delete(int id);
}

