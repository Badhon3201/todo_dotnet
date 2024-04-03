public class Person{
    public int Id { get; set; }
    public string Name { get; set; }

    public int JoinCount { get; set; }


   
    public List<Todo> Todos { get; set; } = [];
}