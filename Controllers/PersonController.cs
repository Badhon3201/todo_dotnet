using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("[controller]")]
[ApiController]
// [Authorize]
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public async Task<ActionResult<Person>> Get(int id){
        var person = await context.Persons.FirstOrDefaultAsync(p=>p.Id==id);
        if(person==null){
            return NotFound();
        }
        return Ok(person);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]

    public async Task<ActionResult<Person>> Post(Person person){
        context.Persons.Add(person);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get),person.Id,person);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id){
        var esistingPerson = await context.Persons.FirstOrDefaultAsync(p=>p.Id==id);
        if(esistingPerson==null){
            return NotFound();
        }
        context.Persons.Remove(esistingPerson);
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("join/{id}")]
    
    public async Task<IActionResult> Post(int id){
        var person = await context.Persons.FirstOrDefaultAsync(p=>p.Id==id);
        person.JoinCount = person.JoinCount+1;
        await context.SaveChangesAsync();
        return NoContent();
    }


    [HttpGet("my-profile")]
    [Authorize]
    public async Task<ActionResult<Person>> MyProfile(){
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
         var person = await context.Persons.FirstOrDefaultAsync(p=>p.Id==userId);
         if(person==null){
            return NotFound();
         }
         return Ok(person);
    }
}