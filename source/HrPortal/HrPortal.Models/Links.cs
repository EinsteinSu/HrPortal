using System;
using Supeng.Silverlight.Common.Entities;

namespace HrPortal.Models
{
  public class Links : EsuInfoBase
  {
    public string Group { get; set; }

    public string Name { get; set; }

    public string Version { get; set; }

    public DateTime UpgradeTime { get; set; }

    public string Link { get; set; }
  }
}