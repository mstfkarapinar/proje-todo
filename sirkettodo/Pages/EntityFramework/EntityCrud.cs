using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Net.Http;
using Microsoft.Data.SqlClient;
using sirkettodo.Pages.EntityFramework;
namespace sirkettodo.Pages.EntityFramework
{   

    public class EntitySqlService
    {
         
        private readonly MyConnection _appDBContext;

        public EntitySqlService(MyConnection appDBContext)  
        {  
            _appDBContext = appDBContext;  
        }

        public async Task<List<Projects>> GetAllProjects()  
        {  
            return await _appDBContext.projects.ToListAsync();  
        }

        public async Task<bool> InserProjectsAsync(Projects project)
        {
            _appDBContext.projects.Add(project);
            await _appDBContext.SaveChangesAsync();
            return true;
        } 

        public async Task<bool> UpdateProjectAsync(Projects project)
        {
             _appDBContext.projects.Update(project);
            await _appDBContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<UsersData>> GetAllUsers()  
        {  
            return await _appDBContext.users.ToListAsync();  
        }

        public async Task<List<Tasks>> GetAllTasks()  
        {  
            return await _appDBContext.tasks.ToListAsync();  
        }

        public async Task<bool> InsertTasksAsync(Tasks task)
        {
            _appDBContext.tasks.Add(task);
            await _appDBContext.SaveChangesAsync();
            return true;
        } 

        public async Task<bool> UpdateTasksAsync(Tasks task)
        {
             _appDBContext.tasks.Update(task);
            await _appDBContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> InsertTaskAssignmentAsync(TaskAssignment taskassign)
        {
            _appDBContext.task_assignment.Add(taskassign);
            await _appDBContext.SaveChangesAsync();
            return true;
        } 

        public async Task<bool> UpdateTaskAssignmentAsync(TaskAssignment taskassign)
        {
             _appDBContext.task_assignment.Update(taskassign);
            await _appDBContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUsersAsync(UsersData user)
        {
             _appDBContext.users.Update(user);
            await _appDBContext.SaveChangesAsync();
            return true;
        }

        public void UpdateUsersAsync1(UsersData user)
        {
            _appDBContext.users.Update(user);
            _appDBContext.SaveChangesAsync();
        }
        public List<Role> GetAllRoles()  
        {  
            return _appDBContext.role.ToList();  
        }

        public List<Team> GetAllTeams()  
        {  
            return _appDBContext.team.ToList();  
        }

        public async Task<Team> GetTeamAsync(int Id)
        {
            Team team = await _appDBContext.team.FirstOrDefaultAsync(c => c.team_id.Equals(Id));
            return team;
        }
        public async Task<bool> InsertTeamsAsync(Team team)
        {
            _appDBContext.team.Add(team);
            await _appDBContext.SaveChangesAsync();
            return true;
        } 

        public async Task<bool> DeleteTeamAsync(Team team)
        {
            _appDBContext.Remove(team);
            await _appDBContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateTeamAsync(Team team)
        {
            _appDBContext.team.Update(team);
            await _appDBContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<TaskAssignment>> GetAllMissions()  
        { 
            return await _appDBContext.task_assignment.ToListAsync();  
        }


    }
}