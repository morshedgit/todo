using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Manages Todo items.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private static List<Todo> todos = new List<Todo>();

    // GET api/todo
    /// <summary>
    /// Retrieves all Todo items.
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<Todo>> Get()
    {
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
        var todo = todos.FirstOrDefault(t=>t.Id == id);
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
        todo.Id = todos.Count + 1;
        todos.Add(todo);
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
        var existingTodo = todos.FirstOrDefault(t=>t.Id == id);
        if(existingTodo == null)
        {
            return NotFound();
        }
        existingTodo.Title = todo.Title;
        existingTodo.IsComplete = todo.IsComplete;
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
        var todo = todos.FirstOrDefault(t=>t.Id == id);
        if(todo == null)
        {
            return NotFound();
        }
        todos.Remove(todo);
        return NoContent();
    }
}