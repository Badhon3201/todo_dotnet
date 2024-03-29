using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("[controller]")]
[ApiController]
public class TodoController:ControllerBase{
        public ApplicationDbContext Context { get; }
    public TodoController(ApplicationDbContext context)
    {
            this.Context = context;
        
    }
    [HttpGet]
    public async Task<ActionResult<List<Todo>>> Get(){
        var todos = await Context.Todos.ToListAsync();
        return Ok(todos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Todo>> Get(int id){
        var todo = await Context.Todos.Include(t=>t.Person).FirstOrDefaultAsync(t=>t.Id==id);
        if(todo==null){
            return NotFound();
        }
        return Ok(todo);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Todo>> Post(Todo todo){
        Context.Todos.Add(todo);
        await Context.SaveChangesAsync();
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
         var existingTodo = await Context.Todos.FirstOrDefaultAsync(t=>t.Id==id);
        
         if(existingTodo==null){
            return NotFound();
         }
         existingTodo.Title = todo.Title;
         existingTodo.Completed = todo.Completed;

         await Context.SaveChangesAsync();
         return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var todo = await Context.Todos.FirstOrDefaultAsync(t=>t.Id==id);
        if(todo==null){
            return NotFound();
        }
        Context.Todos.Remove(todo);
        await Context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("completed")]
    public async Task<ActionResult<List<Todo>>> GetCompleted(){
        var completed = await Context.Todos.Where(todo=>todo.Completed).ToListAsync();
        return Ok(completed);
    }

    [HttpGet("incompleted")]
    public async Task<ActionResult<List<Todo>>> GetIncompelted(){
        var incompleted = await Context.Todos.Where(todo=>!todo.Completed).ToListAsync();
        return Ok(incompleted);
    }

}