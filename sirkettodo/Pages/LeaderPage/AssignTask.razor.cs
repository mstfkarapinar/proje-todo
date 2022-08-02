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
using System.Net.Mail;

namespace sirkettodo.Pages.LeaderPage
{
    public class SelectUser{
        public string user_id { get; set; }
    }
    public class AssignTaskCs:ComponentBase
    {
        public string projectid{ get; set; }="-1";
        public string taskid{ get; set; }
        public string task_contents{ get; set; }
        public ElementReference RefMulSelect;
        public List<Projects> projects { get; set; }
        public List<Projects> ProjectsTable=new List<Projects>();
        public List<UsersData> UserTable=new List<UsersData>();
        public List<Tasks> TasksTable=new List<Tasks>();
        public List<Tasks> TasksTableTemp=new List<Tasks>();
        public List<TaskAssignment> TaskAssignmentTable=new List<TaskAssignment>();
        public List<string> ChoosedMulItem { get; set; } = new List<string>();
        [Inject]
        public EntitySqlService service{get;set;}
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        protected override async Task OnInitializedAsync() 
        {
            if(UserData.Data.UserEmailData != "NULL" && UserData.Data.UserPasswordData != "NULL" && UserData.Data.UserRoleData == 1)
            {
                ProjectsTable= await Task.Run(() =>service.GetAllProjects());
                TasksTable= await Task.Run(() =>GetTasks());
                UserTable = await Task.Run(() =>GetUser());
                StateHasChanged();
            }
            else
            {
                JSRuntime.InvokeVoidAsync("gologin");
            }
        }

        public async Task<List<UsersData>> GetUser(){
            UserTable = await Task.Run(() =>service.GetAllUsers());
            return UserTable;
        }

        public async Task<List<Tasks>> GetTasks(){
            TasksTable = await Task.Run(() =>service.GetAllTasks());
            return TasksTable;
        }


        public void SetProjectID(ChangeEventArgs e)
        {
            projectid = e.Value.ToString();
            TasksTableTemp.Clear();
            GetTasks(projectid);
            InvokeAsync(StateHasChanged);
        }

        public void SetTaskID(ChangeEventArgs e)
        {
            taskid = e.Value.ToString();
            InvokeAsync(StateHasChanged);
        }

        public List<Tasks> GetTasks(string prid)
        {
            foreach(var item in TasksTable)
            {
                if(item.project_id.ToString()==prid)
                {
                    TasksTableTemp.Add(item);
                }
            }
            return TasksTableTemp;
        }

        public async void AddTasks()
        {
            Tasks task=new Tasks();
            task.task=task_contents;
            task.project_id=Int32.Parse(projectid);
            task.team_id=UserData.Data.UserTeamData;
            task.tasks_status=0;
            await service.InsertTasksAsync(task);
            projectid="-1";
            TasksTable.Add(task);
            await OnInitializedAsync();
        }

        public async void AddTaskAssignment()
        {   
            string projectname="";
            string taskname="";
            string useremail="";
            foreach(var item in ChoosedMulItem){
                TaskAssignment taskassign=new TaskAssignment();
                taskassign.team_id=UserData.Data.UserTeamData;
                taskassign.task_id=Int32.Parse(taskid);
                taskassign.mission_status=0;
                taskassign.user_id=Int32.Parse(item);
                taskassign.mission_status=0;
                await service.InsertTaskAssignmentAsync(taskassign);
                TaskAssignmentTable.Add(taskassign);
                foreach(var projectsname in ProjectsTable)
                {
                    if(projectsname.project_id.ToString()==projectid)
                    {
                        projectname=projectsname.project_name;
                    }
                }

                foreach(var taskg in TasksTableTemp)
                {
                    if(taskg.task_id==Int32.Parse(taskid))
                    {
                        taskname=taskg.task;
                    }
                }

                foreach(var users in UserTable)
                {
                    if(users.user_id==Int32.Parse(item))
                    {
                        useremail=users.email;
                    }
                }


                await SendMail(useremail,taskname,projectname);
                await OnInitializedAsync();
            }
        }

        public async void ChangedMulSelect()
        {
            ChoosedMulItem = await JSRuntime.InvokeAsync<List<string>>("mulselect", new object[] {RefMulSelect});
            foreach(var item in ChoosedMulItem)
            {
                Console.WriteLine(item);
            }
            await InvokeAsync(StateHasChanged);
        }

        public async void GetTaskID(string gettaskid,object checkedValue)
        {
            bool newIsDone=(bool)checkedValue?true:false;  
            
            if (newIsDone==true)
            {
                taskid=gettaskid;
            }
            else
            {
                taskid="-1";
            }
            Console.WriteLine(taskid);

        }


        public async void OpenAddTasks()
        {
            await JSRuntime.InvokeVoidAsync("OpenAddTasks");
        }
        
        public async void OpenAssignTask()
        {
            await JSRuntime.InvokeVoidAsync("OpenAssignTask");
        }

        public async Task UpdateTasks(Tasks user)  
        {  
            await service.UpdateTasksAsync(user);
        } 

        public string ResultMessage { get; set; } = ""; 
        public async Task SendMail(string email,string task,string project) 
        { 
            try
            {   
                    using (MailMessage mail = new MailMessage()) 
                    { 
                        mail.From = new MailAddress("ramiz058@hotmail.com"); 
                        mail.To.Add(email); 
                        mail.Subject = project+" Projesi"; 
                        mail.Body =project+" projesi için '"+task+"' görevine dahil edildiniz"; 
                        mail.IsBodyHtml = true; 
                        using (SmtpClient smtp = new SmtpClient("smtp.outlook.com", 587)) 
                        {
                            smtp.Credentials = new System.Net.NetworkCredential("ramiz058@hotmail.com", "tbmm200058");
                            smtp.EnableSsl =true;
                            smtp.Send(mail);
                            ResultMessage = "Mail Gitti";
                        }
                    }
            }

            catch(Exception e)
            {
                ResultMessage=e.Message;
            }
        }

        
    }
}
