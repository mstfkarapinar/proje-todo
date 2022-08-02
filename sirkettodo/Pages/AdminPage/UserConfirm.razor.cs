using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations;
using sirkettodo.Pages.EntityFramework;
using static sirkettodo.Pages.UserData;
namespace sirkettodo.Pages.AdminPage 
{
    public class AdminUserConfirm:ComponentBase
    {
        public static string database = @"workstation id=sirkettodo.mssql.somee.com;packet size=4096;user id=Ramiz058_SQLLogin_1;pwd=rs91p7j2w2;data source=sirkettodo.mssql.somee.com;persist security info=False;initial catalog=sirkettodo";    
        public static SqlConnection connection = new SqlConnection(database);
        public static SqlCommand command = connection.CreateCommand();
        
        public string name{get;set;}
        public string lastname{get;set;}
        public string email{get;set;}
        public string password{get;set;}
        public string callback{get;set;}="Default";
        public string team_id{get;set;}
        public string team_name{get;set;}
        //public List<Users> UserTable=new List<Users>();
        public List<Role> RoleTable=new List<Role>();
        public List<Team> TeamTable=new List<Team>();
        public List<UsersData> UserTable { get; set; }
        [Inject]
        public EntitySqlService service{get;set;}
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        IJSRuntime JSRuntime { get; set; }

   
        protected override async Task OnInitializedAsync() 
        {
            if(UserData.Data.UserEmailData != "NULL" && UserData.Data.UserPasswordData != "NULL" && UserData.Data.UserRoleData == 0)
            {
                TeamTable = await Task.Run(() =>GetTeam());
                RoleTable = await Task.Run(() =>GetRole());
                UserTable = await Task.Run(() =>GetUser());
                StateHasChanged();
            }
            else
            {
                JSRuntime.InvokeVoidAsync("gologin");
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
            string useremail=await JSRuntime.InvokeAsync<string>("useremail");
            string userpassword=await JSRuntime.InvokeAsync<string>("userpassword");
            string userrole=await JSRuntime.InvokeAsync<string>("userrole");
            Console.WriteLine(useremail+" "+userpassword+" "+userrole);
            }
        }

        public async Task<List<UsersData>> GetUser(){
            UserTable = await Task.Run(() =>service.GetAllUsers());
            return UserTable;
        }

        public async Task<List<Role>> GetRole(){
            RoleTable = await Task.Run(() =>service.GetAllRoles());
            return RoleTable;
        }

        public async Task<List<Team>> GetTeam(){
            TeamTable = await Task.Run(() =>service.GetAllTeams());
            return TeamTable;
        }

        public void ChangeRole(ChangeEventArgs e,int? user_id,string elementid)
        {
            string role_id = e.Value.ToString();
            command.CommandText ="update users set role_id="+role_id+" where user_id="+user_id+"";
            Console.WriteLine(command.CommandText);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            JSRuntime.InvokeVoidAsync("ChangeColor",elementid,role_id);
            StateHasChanged();
        }

        public void ChangeTeam(ChangeEventArgs e,int? user_id)
        {
            string team_id = e.Value.ToString();
            command.CommandText ="update users set team_id="+team_id+" where user_id="+user_id+"";
            Console.WriteLine(command.CommandText);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            StateHasChanged();
        }  

        public async void ChangeConfirmation(string UserEmail, object checkedValue)
        {
            bool newIsDone=(bool)checkedValue?true:false;  
            Console.WriteLine(newIsDone);
            foreach(UsersData user in UserTable){
            if(user.email == UserEmail){
                if (newIsDone==true)
                {
                    user.confirmation_status = 1;
                }
                else
                {
                    user.confirmation_status = 0;
                }
                    await UpdateUsers(user);
                    break;
            }
        }
        }

        protected  async Task UpdateUsers(UsersData user)  
        {  
            await service.UpdateUsersAsync(user);
        } 

        protected void UpdateUsers1(UsersData user)  
        {  
            service.UpdateUsersAsync1(user);
        } 
    }
}