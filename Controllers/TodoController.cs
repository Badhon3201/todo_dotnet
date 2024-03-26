using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class TodoController:ControllerBase{
    [HttpGet]
    public async Task<ActionResult<List<Todo>>> Get(){
        var todos = Database.Todos;
        return Ok(todos);
    } 

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Todo>> Get(int id){
        var todo = Database.Todos.FirstOrDefault(p=>p.Id==id);
        if(todo==null){
            return NotFound();
        }
        return Ok(todo);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Todo>> Post(Todo todo){
        var lastId = Database.Todos.Max(todo=>todo.Id);
        todo.Id = lastId+1;
        Database.Todos.Add(todo);
        return CreatedAtAction(nameof(Get),new {todo.Id},todo);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put(int id,Todo todo){
        if(todo.Id!=id){
            return BadRequest();
        }
         var existingTodo = Database.Todos.FirstOrDefault(p=>p.Id==id);
        
         if(existingTodo==null){
            return NotFound();
         }
         existingTodo.Title = todo.Title;
         existingTodo.Completed = todo.Completed;

         Database.Todos.Remove(existingTodo);
         Database.Todos.Add(existingTodo);
         return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var todo = Database.Todos.FirstOrDefault(p=>p.Id==id);
        if(todo==null){
            return NotFound();
        }
        Database.Todos.Remove(todo);
        return NoContent();
    }

    [HttpGet("completed")]
    public async Task<ActionResult<List<Todo>>> GetCompleted(){
        var completed = Database.Todos.Where(todo=>todo.Completed);
        return Ok(completed);
    }

    [HttpGet("incompleted")]
    public async Task<ActionResult<List<Todo>>> GetIncompelted(){
        var incompleted = Database.Todos.Where(todo=>!todo.Completed);
        return Ok(incompleted);
    }

}