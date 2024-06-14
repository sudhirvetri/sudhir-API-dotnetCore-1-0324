using Microsoft.EntityFrameworkCore;

namespace testapiproject.Data
{
    public class CollegeDBContext : DbContext
    {
        DbSet<Student> students { get; set; }

    }

}