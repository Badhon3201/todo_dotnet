public class EntityNotFoundException:Exception{
    public EntityNotFoundException():base("Entity Not Found")
    {
        
    }
    public EntityNotFoundException(string message):base(message)
    {
        
    }
    public EntityNotFoundException(int id):base($"Entity with id {id} not found")
    {
        
    }
}