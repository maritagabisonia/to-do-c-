using System.Data.SqlClient;
using System.Data;
using ToDoApi.Models;
using System.Data;
using ToDoApi.Packages;


namespace ToDoApi.Packages
{

    public interface IPKG_TO_DO
    {
        public List<Todo>get_tasks();
        public void add_task(Todo toDo);
        public void update_task(Todo toDo);
        public void done_task(Todo toDo);
        public void delete_task(int id);
    }
    public class PKG_TO_DO : PKG_BASE, IPKG_TO_DO
    {
        IConfiguration Configuration;

        public PKG_TO_DO(IConfiguration configuration) : base(configuration)
        {

        }

        public List<Todo>get_tasks()
        {
            List<Todo> toDos = new List<Todo>();
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = Connstr;

            conn.Open();

            SqlCommand cmd = conn.CreateCommand();

            cmd.Connection = conn;
            cmd.CommandText = "[Tamo\\MSSQLSERVER02].[ToDo].[dbo].[get_tasks]";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.ExecuteNonQuery();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Todo toDo = new Todo();
                toDo.Id = int.Parse(reader["Id"].ToString());
                toDo.task = reader["Task"].ToString();
                toDo.Status = int.Parse(reader["Status"].ToString())== 1;
                toDos.Add(toDo);

            }
            conn.Close();

            return toDos;
        }

        public void add_task(Todo toDo)
        {
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = Connstr;

            conn.Open();

            SqlCommand cmd = conn.CreateCommand();

            cmd.Connection = conn;
            cmd.CommandText = "[Tamo\\MSSQLSERVER02].[ToDo].[dbo].[add_task]";
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameter parameter_task = new SqlParameter("@p_task", SqlDbType.NVarChar, 30);
            IDataParameter parameter_status = new SqlParameter("@p_status", SqlDbType.NChar);
            cmd.Parameters.Add(parameter_task);
            cmd.Parameters.Add(parameter_status);
            parameter_task.Value = toDo.task;
            parameter_status.Value =Convert.ToInt32(toDo.Status);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void delete_task(int id) {
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = Connstr;

            conn.Open();

            SqlCommand cmd = conn.CreateCommand();

            cmd.Connection = conn;
            cmd.CommandText = "[Tamo\\MSSQLSERVER02].[ToDo].[dbo].[delete_task]";
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameter parameter = new SqlParameter("@p_id", SqlDbType.NChar);
            cmd.Parameters.Add(parameter);
            parameter.Value = id;
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void update_task(Todo toDo) 
        {
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = Connstr;

            conn.Open();

            SqlCommand cmd = conn.CreateCommand();

            cmd.Connection = conn;
            cmd.CommandText = "[Tamo\\MSSQLSERVER02].[ToDo].[dbo].[update_task]";
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameter parameter_id = new SqlParameter("@p_id", SqlDbType.NChar);
            IDataParameter parameter_task = new SqlParameter("@p_task", SqlDbType.NVarChar, 30);
            IDataParameter parameter_status = new SqlParameter("@p_status", SqlDbType.NChar);
            cmd.Parameters.Add(parameter_id);
            cmd.Parameters.Add(parameter_task);
            cmd.Parameters.Add(parameter_status);
            parameter_id.Value = toDo.Id;
            parameter_task.Value = toDo.task;
            parameter_status.Value =Convert.ToInt32(toDo.Status);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void done_task(Todo toDo) {
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = Connstr;

            conn.Open();

            SqlCommand cmd = conn.CreateCommand();

            cmd.Connection = conn;
            cmd.CommandText = "[Tamo\\MSSQLSERVER02].[ToDo].[dbo].[done_task]";
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameter parameter_id = new SqlParameter("@p_id", SqlDbType.NChar);
            IDataParameter parameter_status = new SqlParameter("@p_status", SqlDbType.NChar);
            cmd.Parameters.Add(parameter_id);
            cmd.Parameters.Add(parameter_status);
            parameter_id.Value = toDo.Id;
            parameter_status.Value =Convert.ToInt32(toDo.Status);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
