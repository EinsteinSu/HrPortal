using System;
using System.Data;
using Supeng.Common.DataOperations;
using Supeng.Common.Entities;
using Supeng.Common.Types;

namespace HrPortal.Service.Core.Entities
{
  public class Links : EsuInfoBase
  {
    public string Group { get; set; }

    public string Name { get; set; }

    public string Link { get; set; }

    public string Version { get; set; }

    public DateTime UpgradeTime { get; set; }
  }

  public sealed class LinksCreator : IDataCreator<Links>
  {
    public Links CreateData(IDataReader reader)
    {
      var data = new Links();
      data.Group = reader["group"].ToString();
      data.Name = reader["name"].ToString();
      data.Link = reader["link"].ToString();
      data.Version = reader["version"].ToString();
      data.UpgradeTime = reader["upgradetime"].ToString().ConvertToDateTime();
      return data;
    }
  }
}