using System.Collections.Generic;
using System.Data.SqlClient;

namespace HrPortal.Service.Core
{
  public class Globals
  {
    private static string connectionString;

    public static string ConnectionString
    {
      get
      {
        if (connectionString == null)
        {
          var builder = new SqlConnectionStringBuilder
          {
            InitialCatalog = "Requierments",
            DataSource = "182.247.229.116",
            UserID = "sa",
            Password = "hrmaster"
          };
          connectionString = builder.ConnectionString;
        }
        return connectionString;
      }
    }

    public static List<string> CCGroups
    {
      get
      {
        var list = new List<string> { "235542143@qq.com", "196013914@qq.com", "178839288@qq.com", "28135487@qq.com" };
        return list;
      }
    }

    public static string Master
    {
      get { return "76507593@qq.com"; }
    }
  }
}