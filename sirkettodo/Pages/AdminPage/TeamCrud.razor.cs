using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Data.SqlClient;
using System.Data;
using sirkettodo.Pages.EntityFramework;
using static sirkettodo.Pages.UserData;
namespace sirkettodo.Pages.AdminPage 
{
    public class AdminTeamCrud:ComponentBase
    {
        public string team_id{get;set;}
        public string team_name{get;set;}
        public AdminUserConfirm UserConfirms;
        public List<Team> TeamTable=new List<Team>();
        [Inject]
        public EntitySqlService service{get;set;}
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        protected override async Task OnInitializedAsync() 
        {
            if(UserData.Data.UserEmailData != "NULL" && UserData.Data.UserPasswordData != "NULL" && UserData.Data.UserRoleData == 0)
            {
                TeamTable.Clear();
                TeamTable=GetTeam();
                StateHasChanged();
            }
            else
            {
                JSRuntime.InvokeVoidAsync("gologin");
            }
        }

        public List<Team> GetTeam(){
            TeamTable= service.GetAllTeams();
            return TeamTable;
        }

        public void OpenMenu(ChangeEventArgs e)
        {
            string value=e.Value.ToString();
            
            if(value=="0")
            {
                JSRuntime.InvokeVoidAsync("openaddteam");
                Console.WriteLine(value);
            }
            else if(value=="1")
            {
                JSRuntime.InvokeVoidAsync("openupdateteam");
                Console.WriteLine(value);
            }
            else if(value=="2")
            {
                JSRuntime.InvokeVoidAsync("opendeleteteam");
                Console.WriteLine(value);
            }
        }

        public void AddTeam()
        {
            Team team=new Team();
            team.team_name=team_name;
            service.InsertTeamsAsync(team);
            team.team_id=TeamTable.Last().team_id+1;
            TeamTable.Add(team);
        }

        public async void UpdateTeam()
        {
            Team team = await Task.Run(() => service.GetTeamAsync(Convert.ToInt32(team_id)));
            team.team_name=team_name;
            await service.UpdateTeamAsync(team);
            await InvokeAsync(StateHasChanged);
        }

        public async void SearchTeam()
        {
            Team team = await Task.Run(() => service.GetTeamAsync(Convert.ToInt32(team_id)));
            if(team!=null)
            {
                DeleteTeam(team);
                TeamTable.Remove(team);
            }
        }

        protected async void DeleteTeam(Team team)
        {
            await  service.DeleteTeamAsync(team);
            await InvokeAsync(StateHasChanged);
        }


    }
}