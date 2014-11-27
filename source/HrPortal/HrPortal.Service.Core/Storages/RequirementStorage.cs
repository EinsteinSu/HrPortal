using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrPortal.Service.Core.Entities;
using Supeng.Common.DataOperations;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Common.Exceptions;
using Supeng.Common.Interfaces;
using Supeng.Data;
using Supeng.Data.Sql;

namespace HrPortal.Service.Core.Storages
{
  public interface IRequirementStorage : ICrudStorage<Requirement>, ICrudWithParameterStorage<Comment, int>
  {
    Entities.Requirement GetRequirement(int issueID);

    Entities.Requirement GetNewestRequirement();

    EsuInfoCollection<Requirement> GetRequirements(int status);

    EsuInfoCollection<Comment> GetComments(int issueID, bool asc = false);

    string GetEmailContent(int issueID, out string subject, out List<string> ccList, out List<string> toUserList);
  }

  public class RequirementStorage : SqlDataStorage, IRequirementStorage
  {
    public RequirementStorage()
      : base(Globals.ConnectionString, TaskScheduler.Current)
    {
    }

    public Requirement GetRequirement(int issueID)
    {
      var creator = new SqlScriptCreator("IssueView", new[] { string.Format("id = {0}", issueID) });
      return ReadSingleRecord(creator.GetSqlScript(), new RequirementCreator());
    }

    public Requirement GetNewestRequirement()
    {
      var creator = new SqlScriptCreator("IssueView");
      return ReadSingleRecord(creator.GetSqlScript("Order by orderID desc"), new RequirementCreator());
    }

    public EsuInfoCollection<Requirement> GetRequirements(int status)
    {
      var creator = new SqlScriptCreator("IssueView");
      return ReadToCollection(creator.GetSqlScript("Order by orderID desc"), new RequirementCreator());
    }

    public EsuInfoCollection<Comment> GetComments(int issueID, bool asc = false)
    {
      var creator = new SqlScriptCreator("CommentView", new[] { string.Format("issueid = {0}", issueID) });
      string order = "Order by orderID desc";
      if (asc)
        order = "Order by orderID asc";
      return ReadToCollection(creator.GetSqlScript(order), new CommentCreator());
    }

    public string GetEmailContent(int issueID, out string subject, out List<string> ccList, out List<string> toUserList)
    {
      ccList = new List<string>();
      ccList.AddRange(Globals.CCGroups);
      toUserList = new List<string> { Globals.Master };
      var issue = GetRequirement(issueID);
      subject = issue.Subject;
      if (string.IsNullOrEmpty(issue.Subject))
        return string.Empty;

      #region process users
      var userStorage = new UserStorage();
      var toEmail = userStorage.GetUserEmail(issue.CreateUserID);
      if (toEmail != Globals.Master)
        toUserList.Add(toEmail);
      ccList.Remove(toEmail);
      #endregion

      try
      {
        #region create email
        var sb = new StringBuilder();
        sb.AppendLine("<html>");
        sb.AppendLine("<head>");
        sb.AppendLine("<style>");
        sb.AppendLine("table{border-collapse:collapse;}table,th, td{border: 1px solid black;}");
        sb.AppendLine("</style>");
        sb.AppendLine("</head>");
        sb.AppendLine("<body>");
        sb.AppendLine(string.Format("<h3>应用平台：{0} </h3>", issue.App));
        sb.AppendLine(string.Format("<h3> {0} </h3>", issue.Subject));
        sb.AppendLine(string.Format("<p> {0} </p>", issue.Description));
        sb.AppendLine("<table width='600'>");
        sb.AppendLine(string.Format("<tr><td>类型：{0}</td><td>创建人：{1}</td><tr>", issue.Category, issue.CreateUserName));
        sb.AppendLine(string.Format("<tr><td>优先级：{0}</td><td>存在风险：{1}</td><tr>", issue.Priority, issue.Risk));
        sb.AppendLine(string.Format("<tr><td>预计处理时间：{0}</td><td>当前状态：{1}</td><tr>", issue.Workload, issue.Status));

        var comments = GetComments(issueID, true);
        if (comments.Any())
        {
          sb.AppendLine("<table width='600'>");
          sb.AppendLine(string.Format("<tr><td>评论及意见</td><td>评论人</td><td>创建时间</td></tr>"));
          foreach (var comment in comments)
          {
            sb.AppendLine(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>", comment.Description, comment.CreateUserName, comment.UpdateTime));
          }
          sb.AppendLine("</table>");
        }
        sb.AppendLine("</table>");
        sb.AppendLine("</body>");
        sb.AppendLine("</html>");
        #endregion

        return sb.ToString();
      }
      catch
      {
        return string.Empty;
      }
    }

    #region requirement crud

    public string Insert(Requirement data)
    {
      return ExceptionExtensions.ProcessWithException<Exception>(() => data.Insert(this, new RequirementSave()));
    }

    public string Update(Requirement data)
    {
      return ExceptionExtensions.ProcessWithException<Exception>(() => data.Update(this, new RequirementSave()));
    }

    public string Delete(Requirement data)
    {
      return ExceptionExtensions.ProcessWithException<Exception>(() => data.Delete(this, new RequirementSave()));
    }

    #endregion

    #region comment crud
    public string Insert(Comment data, int id)
    {
      return ExceptionExtensions.ProcessWithException<Exception>(() => data.Insert(this, new CommentSave(id)));
    }

    public string Update(Comment data, int id)
    {
      return ExceptionExtensions.ProcessWithException<Exception>(() => data.Update(this, new CommentSave(id)));
    }

    public string Delete(Comment data, int id)
    {
      return ExceptionExtensions.ProcessWithException<Exception>(() => data.Delete(this, new CommentSave(id)));
    }
    #endregion

  }
}