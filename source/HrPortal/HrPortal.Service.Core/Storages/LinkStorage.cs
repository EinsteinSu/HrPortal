using System.Threading.Tasks;
using HrPortal.Service.Core.Entities;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Data;
using Supeng.Data.Sql;

namespace HrPortal.Service.Core.Storages
{
  public interface ILinkStorage
  {
    EsuInfoCollection<Links> GetLinks();
  }

  public class LinkStorage : SqlDataStorage, ILinkStorage
  {
    public LinkStorage()
      : base(Globals.ConnectionString, TaskScheduler.Current)
    {
    }

    public EsuInfoCollection<Links> GetLinks()
    {
      var creator = new SqlScriptCreator("Links");
      return ReadToCollection(creator.GetSqlScript("Order by orderID"), new LinksCreator());
    }
  }
}