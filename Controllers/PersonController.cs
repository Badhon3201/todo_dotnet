using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("[controller]")]
[ApiController]
public class PersonController:ControllerBase{
        private readonly ApplicationDbContext context;
    public PersonController(ApplicationDbContext context)
    {
            this.context = context;
        
    }

    [HttpGet]
    public async Task<ActionResult<List<Person>>> Get(){
        var persons = await context.Persons.ToListAsync();
        return Ok(persons);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Person>> Get(int id){
        var person = await context.Persons.FirstOrDefaultAsync(p=>p.Id==id);
        if(person==null){
            return NotFound();
        }
        return Ok(person);
    }

    [HttpPost]
    public async Task<ActionResult<Person>> Post(Person person){
        context.Persons.Add(person);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get),person.Id,person);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id,Person person){
        if(person.Id!=id){
            return BadRequest();
        }
         var esistingPerson = await context.Persons.FirstOrDefaultAsync(p=>p.Id==id);
        if(esistingPerson==null){
            return NotFound();
        }
        esistingPerson.Name = person.Name;
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id){
        var esistingPerson = await context.Persons.FirstOrDefaultAsync(p=>p.Id==id);
        if(esistingPerson==null){
            return NotFound();
        }
        context.Persons.Remove(esistingPerson);
        await context.SaveChangesAsync();
        return NoContent();
    }

}