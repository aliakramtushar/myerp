using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MERP.Engine.GlobalGateway
{
    public class CommonGateway
    {
        private string ConnectionString = "Server=DESKTOP-DORUQO5;Database=MERP;User ID='';password='';Integrated Security=true";  // Local Home PC

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
