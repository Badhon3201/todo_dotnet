public class Todo{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool Completed { get; set; }


    public int PersonId { get; set; }
    public Person Person { get; set; }
}