using System;
using System.Data;
using System.Data.SqlClient;
using Supeng.Common.DataOperations;
using Supeng.Common.Entities;
using Supeng.Common.Types;

namespace HrPortal.Service.Core.Entities
{
  public class Comment : EsuInfoBase
  {
    private string createUserID;
    private string createUserName;
    private int id1;
    private DateTime updateTime;

    public int Id
    {
      get { return id1; }
      set
      {
        if (value == id1) return;
        id1 = value;
        NotifyOfPropertyChange(() => Id);
      }
    }

    public DateTime UpdateTime
    {
      get { return updateTime; }
      set
      {
        if (value.Equals(updateTime)) return;
        updateTime = value;
        NotifyOfPropertyChange(() => UpdateTime);
      }
    }

    #region properties

    public string CreateUserName
    {
      get { return createUserName; }
      set
      {
        if (value == createUserName) return;
        createUserName = value;
        NotifyOfPropertyChange(() => CreateUserName);
      }
    }

    public string CreateUserID
    {
      get { return createUserID; }
      set
      {
        if (value == createUserID) return;
        createUserID = value;
        NotifyOfPropertyChange(() => CreateUserID);
      }
    }

    #endregion
  }

  public sealed class CommentCreator : IDataCreator<Comment>
  {
    public Comment CreateData(IDataReader reader)
    {
      var data = new Comment();
      data.Id = reader["id"].ToString().ConvertData<int>();
      data.Description = reader["description"].ToString();
      data.CreateUserID = reader["createUserID"].ToString();
      data.CreateUserName = reader["createUserName"].ToString();
      data.UpdateTime = reader["updateTime"].ToString().ConvertToDateTime();
      return data;
    }
  }

  public sealed class CommentSave : IDataSave<Comment>
  {
    private readonly int issueID;

    public CommentSave(int issueID)
    {
      this.issueID = issueID;
    }

    public IDataParameter[] MappingParameters(Comment data)
    {
      var parameters = new IDataParameter[4];
      parameters[0] = new SqlParameter("@id", data.Id);
      parameters[1] = new SqlParameter("@issueID", issueID);
      parameters[2] = new SqlParameter("@description", data.Description);
      parameters[3] = new SqlParameter("@createUserID", data.CreateUserID);
      return parameters;
    }

    public string InsertSqlScript()
    {
      return "Insert into comment(issueid,description,createuserid,updatetime) values(@issueid,@description,@createuserID,getdate())";
    }

    public string UpdateSqlScript()
    {
      return "Update comment set description = @description,updatetime = getdate() where id = @id";
    }

    public string DeleteSqlScript()
    {
      return "Delete comment where id = @id";
    }
  }
}