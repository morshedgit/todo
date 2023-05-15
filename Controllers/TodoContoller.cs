using Dapper;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Manages Todo items.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    // private static List<Todo> todos = new List<Todo>();

    private readonly DatabaseContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="TodoController"/> class.
    /// </summary>
    public TodoController(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET api/todo
    /// <summary>
    /// Retrieves all Todo items.
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<Todo>> Get()
    {
        using var connection = _dbContext.GetConnection();
        var todos = connection.Query<Todo>("SElECT * FROM Todo");
        return Ok(todos);
    }

    // GET api/todo/{id}
    /// <summary>
    /// Retrieves a Todo item by ID.
    /// </summary>
    /// <param name="id">The ID of the todo item.</param>
    [HttpGet("{id}")]
    public ActionResult<Todo> GetById(int id)
    {
        using var connection = _dbContext.GetConnection();
        var todo = connection.QueryFirstOrDefault<Todo>("SElECT * FROM Todo WHERE Id = @Id",new {Id=id});
        if(todo == null)
        {
            return NotFound();
        }
        return Ok(todo);
    }

    // POST api/todo
    /// <summary>
    /// Creates a new todo item.
    /// </summary>
    /// <param name="todo">The Todo item to create. </param>
    [HttpPost]
    public ActionResult<Todo> Create(Todo todo)
    {        
        using var connection = _dbContext.GetConnection();
        const string insertQuery = "INSERT INTO Todo (Title, IsCompleted) VALUES (@Title,@IsCompleted); SELECT last_insert_rowid();";
        var id = connection.QuerySingle<int>(insertQuery,todo);
        todo.Id = id;
        return CreatedAtAction(nameof(GetById), new { id = todo.Id},todo);
    }

    // PUT api/todo/{id}
    /// <summary>
    /// Updates an existing todo item.
    /// </summary>
    /// <param name="id">The id of the todo item to update.</param>
    /// <param name="todo">The todo item to update.</param>
    [HttpPut("{id}")]
    public IActionResult Update(int id, Todo todo)
    {
        using var connection = _dbContext.GetConnection();
        const string updateQuery = "UPDATE Todo SET Title = @Title, IsCompleted = @IsCompleted WHERE Id = @Id";
        todo.Id = id;
        var affectedRows = connection.Execute(updateQuery,todo);
        if(affectedRows == 0)
        {
            return NotFound();
        }
        return NoContent();
    }

    // DELETE api/todo/{id}
    /// <summary>
    /// Deletes the todo item by ID.
    /// </summary>
    /// <param name="id">The ID of the todo item to delete.</param>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        using var connection = _dbContext.GetConnection();
        const string deleteQuery = "DELETE FROM Todo WHERE Id = @Id";
        var affectedRows = connection.Execute(deleteQuery, new { Id = id });
        if(affectedRows == 0)
        {
            return NotFound();
        }
        return NoContent();
    }
}