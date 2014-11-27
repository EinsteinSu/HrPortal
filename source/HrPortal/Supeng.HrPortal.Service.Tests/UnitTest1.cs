using System;
using System.Collections.Generic;
using HrPortal.Service.Core.Storages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Supeng.HrPortal.Service.Tests
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void TestStorage()
    {
      var ccList = new List<string>();
      var toUserList = new List<string>();
      var storage = new RequirementStorage();
      //var data = storage.GetEmailContent(4, out ccList, out toUserList);
    }
  }
}
