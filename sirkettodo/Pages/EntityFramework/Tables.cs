using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using sirkettodo.Pages.EntityFramework;

namespace sirkettodo.Pages.EntityFramework
{   
    public class Projects
    {   [Key]
        public int project_id { get; set; }
        public string project_name { get; set; }
        public string project_contents { get; set; }
        public int team_id { get; set; }
        public int project_status { get; set; }
        public string starting_date { get; set; }
        public string end_date { get; set;}
    }
    

    public class UsersData{
        [Key]
        public int user_id {get;set;}
        public string name {get;set;}
        public string lastname {get;set;}
        public string email {get;set;}
        public string password {get;set;}
        public string register_date {get;set;}
        public int role_id {get;set;}
        public int team_id {get;set;}
        public int confirmation_status {get;set;}
    }
    public class Tasks{
        [Key]
        public int task_id {get;set;}
        public int project_id {get;set;}
        public int tasks_status{get;set;}
        public int team_id { get; set; }
        public string task {get;set;}
    }
    public class Team{
        [Key]
        public int team_id {get;set;}
        public string team_name {get;set;}
    }
    public class Role{
        [Key]
        public int role_id {get;set;}
        public string role_name {get;set;}
    }

    public class TaskAssignment{
        [Key]
        public int task_assignment_id {get;set;}
        public int team_id {get;set;}
        public int task_id {get;set;}
        public int mission_status {get;set;}
        public int user_id {get;set;}
    }
}