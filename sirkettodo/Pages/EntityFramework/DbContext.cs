using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sirkettodo.Pages.EntityFramework
{   
  public class MyConnection : DbContext
    {
  
        public MyConnection(DbContextOptions<MyConnection> options) : base(options)  
        {  
  
        }
        public  DbSet<Projects> projects { get; set; }
        public  DbSet<UsersData> users { get; set; }
        public  DbSet<Tasks> tasks { get; set; }
        public  DbSet<Team> team { get; set; }
        public  DbSet<Role> role { get; set; }
        public  DbSet<TaskAssignment> task_assignment { get; set; }
    
    }
}