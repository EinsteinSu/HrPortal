using System.ServiceModel;
using HrPortal.Service.Core.Storages;

namespace HrPortal.Service
{
  [ServiceContract]
  public interface IReadService
  {
    [OperationContract]
    string GetRequirements(int status);

    [OperationContract]
    string Login(string userName, string password);

    [OperationContract]
    string GetDownloads();
  }

  public class ReadService : IReadService
  {
    public string GetRequirements(int status)
    {
      var storage = new RequirementStorage();
      return storage.GetRequirements(status).ToString();
    }

    public string Login(string userName, string password)
    {
      var storage = new SecurityStorage();
      return storage.Login(userName, password).ToString();
    }

    public string GetDownloads()
    {
      var storage = new LinkStorage();
      return storage.GetLinks().ToString();
    }
  }
}