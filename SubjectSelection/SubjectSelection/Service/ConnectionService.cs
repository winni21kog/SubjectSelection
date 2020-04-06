using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SubjectSelection.Service
{
    public class ConnectionService
    {
        /// <summary>
        /// 建立Connection
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateConnection(string name="default")
        {
            switch (name)
            {
                case "default":
                    {
                        return new SqlConnection(ConfigurationManager.ConnectionStrings["DapperConnection"].ConnectionString);
                    }
                default:
                    {
                        throw new Exception($"name:{name} 不存在");
                    }
            }
            
        }
    }
}