using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TheWorld.Models {
    public class Helpers {
        public static string GetRDSConnectionString () {

            string dbname = Environment.GetEnvironmentVariable ("RDS_DB_NAME");

            if (string.IsNullOrEmpty (dbname)) return null;

            string username = Environment.GetEnvironmentVariable ("RDS_USERNAME");
            string password = Environment.GetEnvironmentVariable ("RDS_PASSWORD");
            string hostname = Environment.GetEnvironmentVariable ("RDS_HOSTNAME");
            string port = Environment.GetEnvironmentVariable ("RDS_PORT");

            return "Server=" + hostname + "," + port + ";Initial Catalog=" + dbname + ";User Id=" + username + ";Password=" + password + ";";
        }
    }
}