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
            DataSource = "116.54.125.122",
            UserID = "sa",
            Password = "hrmaster"
          };
          connectionString = builder.ConnectionString;
        }
        return connectionString;
      }
    }
  }
}