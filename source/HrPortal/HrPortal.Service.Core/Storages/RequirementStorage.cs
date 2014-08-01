using System.Threading.Tasks;
using HrPortal.Service.Core.Entities;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Data;
using Supeng.Data.Sql;

namespace HrPortal.Service.Core.Storages
{
  public interface IRequirementStorage
  {
    EsuInfoCollection<Requirement> GetRequirements(int status);
  }

  public class RequirementStorage : SqlDataStorage, IRequirementStorage
  {
    public RequirementStorage()
      : base(Globals.ConnectionString, TaskScheduler.Current)
    {
    }

    public EsuInfoCollection<Requirement> GetRequirements(int status)
    {
      var creator = new SqlScriptCreator("IssueView");
      return ReadToCollection(creator.GetSqlScript("Order by orderID"), new RequirementCreator());
    }
  }
}