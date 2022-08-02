using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.JSInterop;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Net.Http;
using System.Data.SqlClient;
using System.Data;
using sirkettodo.Pages.EntityFramework;
using static sirkettodo.Pages.UserData;

namespace sirkettodo.Pages.LeaderPage
{
    public class CreateProjectCs:ComponentBase
    {
    public DateTime tarih{get;set;}=DateTime.Now;
    public string project_name{get;set;}
    public string project_contents{get;set;}
    public string starih{get;set;}
    public List<Projects> projects { get; set; }
    public List<Projects> ProjectsTable=new List<Projects>();
    public List<Team> TeamTable=new List<Team>();
    [Inject]
    public EntitySqlService service{get;set;}
    [Inject]
    IJSRuntime JSRuntime{ get; set; }
 
    public async Task Sa(ChangeEventArgs args)
    {
        tarih = DateTime.Parse(args.Value.ToString());
        starih=tarih.ToString("dd/MM/yyyy");
    }

    protected override async Task OnInitializedAsync() 
    {
        if(UserData.Data.UserEmailData != "NULL" && UserData.Data.UserPasswordData != "NULL" && UserData.Data.UserRoleData == 1)
        {
            TeamTable.Clear();
            TeamTable=GetTeam();
            ProjectsTable.Clear();
            ProjectsTable = await Task.Run(() =>GetProject());
        }
        else
        {
            JSRuntime.InvokeVoidAsync("gologin");
        }
        
    }

    public List<Team> GetTeam()
    {
        TeamTable= service.GetAllTeams();
        return TeamTable;
    }
    
    public async Task CreateProject()
    {
            Projects project=new Projects(); 
            project.project_name=project_name;
            project.project_contents=project_contents;
            project.project_status=0;
            project.team_id=UserData.Data.UserRoleData;
            project.starting_date=DateTime.Now.ToString("dd/MM/yyyy");
            project.end_date=starih;

            await service.InserProjectsAsync(project);
            ProjectsTable.Add(project);
            await OnInitializedAsync();
    }

    public async Task<List<Projects>> GetProject(){
            ProjectsTable = await Task.Run(() =>service.GetAllProjects());
            return ProjectsTable;
    }

    public async void ChangeProjectStat(int ProjectID, object checkedValue)
    {
        bool newIsDone=(bool)checkedValue?true:false;  
        foreach(Projects project in ProjectsTable)
        {
            if(project.project_id == ProjectID)
            {
                if (newIsDone==true)
                {
                    project.project_status = 1;
                }
                else
                {
                    project.project_status = 0;
                }
                await UpdateProject(project);
                break;
            }
        }
    }

    protected  async Task UpdateProject(Projects project)  
    {  
        await service.UpdateProjectAsync(project);
    } 


    }
}