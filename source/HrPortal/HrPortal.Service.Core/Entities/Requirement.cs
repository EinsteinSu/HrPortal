using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HrPortal.Service.Core.Entities.Parameters;
using Supeng.Common.DataOperations;
using Supeng.Common.Entities;
using Supeng.Common.Types;

namespace HrPortal.Service.Core.Entities
{
  public class Requirement : EsuInfoBase
  {
    private AppType app;
    private Category category;
    private List<Comment> comments;
    private string createUserID;
    private string createUserName;
    private int id1;
    private string number;
    private Priority priority;
    private bool risk;
    private Status status;
    private string subject;
    private int workload;
    private DateTime updateTime;

    public string Number
    {
      get { return number; }
      set
      {
        if (value == number) return;
        number = value;
        NotifyOfPropertyChange(() => Number);
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

    public List<Comment> Comments
    {
      get { return comments; }
      set
      {
        if (Equals(value, comments)) return;
        comments = value;
        NotifyOfPropertyChange(() => Comments);
      }
    }

    #region properties

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

    public Category Category
    {
      get { return category; }
      set
      {
        if (value == category) return;
        category = value;
        NotifyOfPropertyChange(() => Category);
      }
    }

    public string Subject
    {
      get { return subject; }
      set
      {
        if (value == subject) return;
        subject = value;
        NotifyOfPropertyChange(() => Subject);
      }
    }

    public Priority Priority
    {
      get { return priority; }
      set
      {
        if (value == priority) return;
        priority = value;
        NotifyOfPropertyChange(() => Priority);
      }
    }

    public bool Risk
    {
      get { return risk; }
      set
      {
        if (value.Equals(risk)) return;
        risk = value;
        NotifyOfPropertyChange(() => Risk);
      }
    }

    public Status Status
    {
      get { return status; }
      set
      {
        if (value == status) return;
        status = value;
        NotifyOfPropertyChange(() => Status);
      }
    }

    public int Workload
    {
      get { return workload; }
      set
      {
        if (value == workload) return;
        workload = value;
        NotifyOfPropertyChange(() => Workload);
      }
    }

    public AppType App
    {
      get { return app; }
      set
      {
        if (value == app) return;
        app = value;
        NotifyOfPropertyChange(() => App);
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

    #endregion
  }

  public sealed class RequirementCreator : IDataCreator<Requirement>
  {
    public Requirement CreateData(IDataReader reader)
    {
      var data = new Requirement();
      data.Id = reader["id"].ToString().ConvertData<int>();
      data.Number = reader["num"].ToString();
      data.Category = (Category) reader["category"].ToString().ConvertData<int>();
      data.Subject = reader["subject"].ToString();
      data.Description = reader["description"].ToString();
      data.Priority = (Priority) reader["priority"].ToString().ConvertData<int>();
      data.Risk = reader["risk"].ToString().Equals("True", StringComparison.InvariantCultureIgnoreCase);
      data.Status = (Status) reader["status"].ToString().ConvertData<int>();
      data.Workload = reader["workload"].ToString().ConvertData<int>();
      data.CreateUserID = reader["createuserid"].ToString();
      data.CreateUserName = reader["createUserName"].ToString();
      data.UpdateTime = reader["updateTime"].ToString().ConvertToDateTime();
      data.App = (AppType) reader["app"].ToString().ConvertData<int>();
      return data;
    }
  }

  public sealed class RequirementSave : IDataSave<Requirement>
  {
    public IDataParameter[] MappingParameters(Requirement data)
    {
      var parameters = new IDataParameter[10];
      parameters[0] = new SqlParameter("@category", data.Category);
      parameters[1] = new SqlParameter("@subject", data.Subject);
      parameters[2] = new SqlParameter("@description", data.Description);
      parameters[3] = new SqlParameter("@priority", data.Priority);
      parameters[4] = new SqlParameter("@risk", data.Risk);
      parameters[5] = new SqlParameter("@status", data.Status);
      parameters[6] = new SqlParameter("@workload", data.Workload);
      parameters[7] = new SqlParameter("@createuserid", data.CreateUserID);
      parameters[8] = new SqlParameter("@app", data.App);
      parameters[9] = new SqlParameter("@id", data.Id);
      return parameters;
    }

    public string InsertSqlScript()
    {
      return
        "Insert into Issue(category,subject,description,priority,risk,status,workload,createuserid,app,updatetime) values(@category,@subject,@description,@priority,@risk,@status,@workload,@createuserid,@app,getdate())";
    }

    public string UpdateSqlScript()
    {
      return
        "Update Issue set category = @category,subject = @subject,description = @description,priority = @priority,risk = @risk,status = @status,workload = @workload,app = @app,updatetime = getdate() Where ID = @id";
    }

    public string DeleteSqlScript()
    {
      return "Delete from Issue Where ID = @id";
    }
  }
}