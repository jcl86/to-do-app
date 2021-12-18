namespace ToDo.Host;

public record Todo 
{
    public int Id { get; init; }
    public string Content { get; private set; }
    public bool Done { get; private set; }

    private Todo() { }
    public Todo(string content, bool done = false)
    {
        Content = content;
        Done = done;
    }

    public void MarkAsDone() => Done = true;
    public void UpdateContent(string content) => Content = content;
    public void UpdateStatus(bool done) => Done = done;

    public Model.Todo AsModel()
    {
        return new Model.Todo()
        {
            Content = Content,
            Done = Done,
            Id = Id
        };
    }
}
