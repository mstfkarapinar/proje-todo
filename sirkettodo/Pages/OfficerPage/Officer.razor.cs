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


namespace sirkettodo.Pages.OfficerPage
{
    public class OfficerCs:ComponentBase
    {
        [Inject]
        public EntitySqlService service{get;set;}
        public List<TaskAssignment> MissionTable=new List<TaskAssignment>();
        public List<Tasks> TaskTable=new List<Tasks>();
        public List<Projects> ProjectsTable=new List<Projects>();
        public List<UsersData> UserTable=new List<UsersData>();
        [Inject]
        IJSRuntime JSRuntime { get; set; }
        protected override async Task OnInitializedAsync() 
        {
            if(UserData.Data.UserEmailData != "NULL" && UserData.Data.UserPasswordData != "NULL" && UserData.Data.UserRoleData == 2)
            {
                ProjectsTable= await Task.Run(() =>service.GetAllProjects());
                TaskTable.Clear();
                TaskTable=await Task.Run(() =>service.GetAllTasks());;
                MissionTable.Clear();
                MissionTable=await Task.Run(() =>service.GetAllMissions());
            }
            else
            {
                JSRuntime.InvokeVoidAsync("gologin");
            }
        }


        public async void ChangeMissionStat(int MissionID, object checkedValue)
        {
            bool newIsDone=(bool)checkedValue?true:false;  
            foreach(TaskAssignment mission in MissionTable)
            {
                if(mission.task_assignment_id == MissionID)
                {
                    if (newIsDone==true)
                    {
                        mission.mission_status = 1;
                    }
                    else
                    {
                        mission.mission_status = 0;
                    }
                    await UpdateMission(mission);
                    break;
                }
            }
            await OnInitializedAsync();
        }
        protected  async Task UpdateMission(TaskAssignment mission)  
        {  
            await service.UpdateTaskAssignmentAsync(mission);
        } 
    }
}