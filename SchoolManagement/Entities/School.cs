namespace SchoolManagement.Entities
{
    public class School : BaseEntity
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public ICollection<Student> Students { get; set; }  
    }
}
