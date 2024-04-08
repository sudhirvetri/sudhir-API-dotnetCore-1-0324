namespace testapiproject.Models
{
    public static class CollegeRepository
    {
        public static List<Student> students = new List<Student>(){
            new Student{ID=1,Name="Sudhir",Email="studen1@gmail.com",Phone=123456789},
            new Student{ID=2,Name="kiran",Email="studen2@gmail.com",Phone=123456789},
            new Student{ID=3,Name="Santhosh",Email="studen3@gmail.com",Phone=123456789}
        };
    }
}