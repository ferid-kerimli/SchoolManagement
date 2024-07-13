namespace SchoolManagement.Entities
{
    public class Student : BaseEntity
    {
        public string FullName { get; set; }
        public School School { get; set; }
        public int SchoolID { get; set; }   
    }
}
