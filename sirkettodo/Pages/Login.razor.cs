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


namespace sirkettodo.Pages 
{

    public partial class UserData {
        public class Data {
            public static string UserEmailData{ get; set; }="null";
            public static string UserPasswordData{ get; set; }="null";
            public static int UserRoleData{ get; set; }=-1;
            public static int UserTeamData{ get; set; }=-1;
            public static int UserIDData{ get; set; }=-1;
        }
    }
    public partial class Login
    {
        public string email{get;set;}
        public string password{get;set;}
        public string callback{get;set;}="";
    
        [Inject]
        IJSRuntime JSRuntime { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync() 
        {
            if(UserData.Data.UserEmailData!="NULL")
            {
                 JSRuntime.InvokeVoidAsync("gologin");
            }
            UserData.Data.UserEmailData="NULL";
            UserData.Data.UserPasswordData="NULL";
            UserData.Data.UserRoleData=-1;
            UserData.Data.UserRoleData=-1;
            UserData.Data.UserTeamData=-1;
           
        }

        public void LoginButton()
        {
            string database = @"workstation id=sirkettodo.mssql.somee.com;packet size=4096;user id=Ramiz058_SQLLogin_1;pwd=rs91p7j2w2;data source=sirkettodo.mssql.somee.com;persist security info=False;initial catalog=sirkettodo";    
            SqlConnection connection = new SqlConnection(database);
            SqlCommand command = connection.CreateCommand();
            command.CommandText ="exec login'"+email+"','"+password+"',@callback OUTPUT,@callback_role_id OUTPUT,@callback_team_id OUTPUT,@callback_user_id OUTPUT"; 
                SqlParameter callback_output = command.Parameters.Add("@callback", SqlDbType.Int);
                SqlParameter callback_role_output = command.Parameters.Add("@callback_role_id", SqlDbType.Int);
                SqlParameter callback_team_output = command.Parameters.Add("@callback_team_id", SqlDbType.Int);
                SqlParameter callback_user_id_output = command.Parameters.Add("@callback_user_id", SqlDbType.Int);
                callback_output.Direction = ParameterDirection.Output;
                callback_role_output.Direction = ParameterDirection.Output;
                callback_team_output.Direction = ParameterDirection.Output;
                callback_user_id_output.Direction = ParameterDirection.Output;
                connection.Open();
                command.ExecuteNonQuery();
                if(Int32.Parse(callback_output.Value.ToString())==1)
                {
                    callback="Kullanıcı bilgileri yanlış.";
                }
                else if(Int32.Parse(callback_output.Value.ToString())==2)
                {
                    callback="Bu kullanıcı onay bekliyor.";
                }
                else if(Int32.Parse(callback_output.Value.ToString())==3)
                {
                    JSRuntime.InvokeVoidAsync("login",email,password,callback_role_output.Value.ToString(),callback_team_output.Value.ToString());
                    
                    callback="Giriş Başarılı.";
                    UserData.Data.UserEmailData=email;
                    UserData.Data.UserPasswordData=password;
                    UserData.Data.UserRoleData=Int32.Parse(callback_role_output.Value.ToString());
                    UserData.Data.UserTeamData=Int32.Parse(callback_team_output.Value.ToString());
                    UserData.Data.UserIDData=Int32.Parse(callback_user_id_output.Value.ToString());
                    int role_id=Int32.Parse(callback_role_output.Value.ToString());
                    if(role_id==0)
                    {
                        StateHasChanged();
                        //NavigationManager.NavigateTo("/userconfirm");
                        JSRuntime.InvokeVoidAsync("goadmin");
                    }
                    else if(role_id==1)
                    {
                        Console.WriteLine(role_id);
                        JSRuntime.InvokeVoidAsync("goleader");
                    }
                    else if(role_id==2)
                    {
                        Console.WriteLine(role_id);
                        JSRuntime.InvokeVoidAsync("goofficer");
                    }
                }
                connection.Close();
        }
    }
}