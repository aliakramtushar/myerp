using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MERP.Engine.GlobalGateway
{
    public class CommonGateway
    {
        //private string ConnectionString = "Server=204.93.178.181;Database=smsbd_smartmanagementsystem;User ID=smsbd_akram;password=welt12#;Integrated Security=false";
        //private string ConnectionString = "Server=DESKTOP-DORUQO5;Database=MERP;User ID='';password='';Integrated Security=true";  // Local Home PC
        //private string ConnectionString = "Server=DESKTOP-DORUQO5;Database=CSMDB;User ID=aliakramtushar;password=!@34QWer;Integrated Security=true";  // Local Home PC
        //private string ConnectionString = "Server=209.151.194.144,8494;Database=MERP;User ID='MERPsa';password='MERPsa123';Integrated Security=false";  // Alpha Net Hosting
        //private string ConnectionString = "Server=209.151.194.144,8494;Database=MERP;User ID='aliakramtushar';password='mycomonpass';Integrated Security=false";  // Alpha Net Hosting
        //private string ConnectionString = "Server=EG-04006043262;Database=MERPDemo;User ID='ali.akram';password='@ALIakram';Integrated Security=false";  // MERP Working Demo Laptop Local

        //private string ConnectionString = "Server=APPSERVERMOBI;Database=CSMDB;User ID='ali.akram';password='@ALIakram';Integrated Security=false";  // Mobicare App Server
        //private string ConnectionString = "Server=EG-CSMDB-01;Database=CSMDB;User ID='ali.akram';password='!@34QWer';Integrated Security=false";  // CSMDB Live
        //private string ConnectionString = "Server=EG-04006043262;Database=CSMDB;User ID='ali.akram';password='@ALIakram';Integrated Security=false";  // CSMDB Backup Laptop Local
        //private string ConnectionString = "Server=EG-CSMDB-03;Database=MERP;User ID='ali.akram';password='!@34QWer';Integrated Security=false";  // MERP Demo Server 3
        private string ConnectionString = "Server=EG-CSMDB-03;Database=MERP;User ID='merpappuser';password='Merp@321';Integrated Security=false";  // MERP Demo Server 3


        public SqlConnection Connection { get; set; }
        public SqlCommand Command { get; set; }
        public CommonGateway()
        {
            Connection = new SqlConnection(ConnectionString);
            Command = new SqlCommand();
            Command.Connection = Connection;
        }
    }
}