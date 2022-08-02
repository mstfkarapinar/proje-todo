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
using static sirkettodo.Pages.UserData;
namespace sirkettodo.Pages 
{
    public partial class Register
    {
        public string name{get;set;}
        public string lastname{get;set;}
        public string email{get;set;}
        public string password{get;set;}
        public string callback{get;set;}

        public void RegisterButton()
        {
            string database = @"workstation id=sirkettodo.mssql.somee.com;packet size=4096;user id=Ramiz058_SQLLogin_1;pwd=rs91p7j2w2;data source=sirkettodo.mssql.somee.com;persist security info=False;initial catalog=sirkettodo";    
            SqlConnection connection = new SqlConnection(database);
            SqlCommand command = connection.CreateCommand();
            command.CommandText ="exec register '"+name+"','"+lastname+"','"+
            email+"','"+password+"',@callback OUTPUT"; 
            SqlParameter callback_output = command.Parameters.Add("@callback", SqlDbType.Int);
            callback_output.Direction = ParameterDirection.Output;
            connection.Open();
            command.ExecuteScalar();
            if(Int32.Parse(callback_output.Value.ToString())==1)
            {
                callback="Bu kullanıcı zaten var";
            }
            else if(Int32.Parse(callback_output.Value.ToString())==2)
            {
                callback="Başarıyla kayıt olundu.Onay verilmesini bekleyin.";
            }
            Console.WriteLine(callback);
            connection.Close();
        }
    }
}