using System.ComponentModel.DataAnnotations;
/// <summary>
/// Todo item.
/// </summary>
public class Todo
{
    /// <summary>
    /// Id of the todo item.
    /// </summary>
    public int Id {get;set;}

    /// <summary>
    /// Name of the todo item.
    /// </summary>
    [Required]
    public string? Title {get;set;}

    /// <summary>
    /// State of the todo item.
    /// </summary>
    public bool IsCompleted {get;set;}
}