using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using HrPortal.Service.Core.Entities;
using HrPortal.Service.Core.Storages;
using Supeng.Common.Strings;
using Supeng.Common.Threads;
using Supeng.Email;

namespace HrPortal.Service
{
  [ServiceContract]
  public interface IWriteService
  {
    [OperationContract]
    string UpdateEnterpriseUser(string userJson);

    [OperationContract]
    string InsertRequirement(string requirement);

    [OperationContract]
    string UpdateRequirement(string requirement, string comment);

    [OperationContract]
    string DeleteRequirement(string requirement);

    [OperationContract]
    string PasswordChange(string userID, string password);

    [OperationContract]
    string InsertComment(string comment, int issueID);

    [OperationContract]
    string UpdateComment(string comment, int issueID);

    [OperationContract]
    string DeleteComment(string comment, int issueID);
  }

  public class WriteService : IWriteService
  {
    private const string AdminGuid = "bff497c2-48f4-494d-a7cb-ecb51dd0619f";

    private void SendEmail(int issueID)
    {
      ThreadHelper.DoTaskWithoutResult(() =>
      {
        string subject;
        List<string> ccList;
        List<string> toUserList;
        var storage = new RequirementStorage();
        var body = storage.GetEmailContent(issueID, out subject, out ccList, out toUserList);
        if (!string.IsNullOrEmpty(body))
        {
          var sender = new EmailSender("ynhengrui@163.com", "云南恒锐消防咨询", "ynhengrui", "hengrui", "smtp.163.com");
          sender.SendEmail(subject, toUserList, ccList, body);
        }
      }, () => { }, exceptions => { }, TaskScheduler.Current);
    }

    public string UpdateEnterpriseUser(string userJson)
    {
      var user = userJson.Load<EnterpriseUser>();
      var storage = new UserStorage();
      return storage.UpdateEnterpriseUser(user);
    }

    public string InsertRequirement(string requirement)
    {
      var r = requirement.Load<Requirement>();
      var storage = new RequirementStorage();
      var result = storage.Insert(r);
      var rnew = storage.GetNewestRequirement();
      SendEmail(rnew.Id);
      return result;
    }

    public string UpdateRequirement(string requirement, string comment)
    {
      string message = string.Empty;
      var r = requirement.Load<Requirement>();
      var storage = new RequirementStorage();
      message += storage.Update(r);
      if (comment != null)
      {
        var c = new Comment { CreateUserID = AdminGuid, Description = comment };
        message += storage.Insert(c, r.Id);
      }
      SendEmail(r.Id);
      return message;
    }

    public string DeleteRequirement(string requirement)
    {
      var r = requirement.Load<Requirement>();
      var storage = new RequirementStorage();
      return storage.Delete(r);
    }

    public string PasswordChange(string userID, string password)
    {
      var storage = new SecurityStorage();
      return storage.PasswordChange(userID, password);
    }

    public string InsertComment(string comment, int issueID)
    {
      var c = comment.Load<Comment>();
      var storage = new RequirementStorage();
      var result = storage.Insert(c, issueID);
      SendEmail(issueID);
      return result;
    }

    public string UpdateComment(string comment, int issueID)
    {
      var c = comment.Load<Comment>();
      var storage = new RequirementStorage();
      var result = storage.Update(c, issueID);
      SendEmail(issueID);
      return result;
    }

    public string DeleteComment(string comment, int issueID)
    {
      var c = comment.Load<Comment>();
      var storage = new RequirementStorage();
      return storage.Delete(c, issueID);
    }
  }
}