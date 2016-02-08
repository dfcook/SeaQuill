using SeaQuill.DataAccess.Extensions;
using SeaQuill.DataAccess.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SeaQuill.DataAccess.Queries
{
    public abstract class AdoNetQueryObject : IQueryObject
    {
        private const int DefaultTimeout = 5000;

        protected abstract IDbConnection GetConnection();

        protected abstract IDbDataParameter CreateParameter();

        protected IDictionary<string, IDbDataParameter> Parameters { get; }

        protected string ConnectionString { get; }
        protected CommandType CommandType { get; }
        protected string CommandText { get; }
        
        private int Timeout { get; set; }

        public AdoNetQueryObject(string connectionString, string commandText, CommandType commandType, int timeout)
            : this(connectionString, commandText, commandType)
        {
            Timeout = timeout;            
        }

        public AdoNetQueryObject(string connectionString, string commandText, CommandType commandType)
        {
            Timeout = DefaultTimeout;
            ConnectionString = connectionString;
            CommandText = commandText;
            CommandType = commandType;
            Parameters = new Dictionary<string, IDbDataParameter>();
        }

        public IQueryObject AddInputOutputParameter<T>(string parameterName, T value)
        {
            var parameter = CreateParameter();

            parameter.Direction = ParameterDirection.InputOutput;
            parameter.ParameterName = parameterName;
            parameter.DbType = GetDbType(typeof(T));

            Parameters.Add(parameterName, parameter);

            return this;
        }

        public IQueryObject AddOutputParameter<T>(string parameterName)
        {
            var parameter = CreateParameter();

            parameter.Direction = ParameterDirection.Output;
            parameter.ParameterName = parameterName;
            parameter.DbType = GetDbType(typeof(T));            

            Parameters.Add(parameterName, parameter);

            return this;
        }

        public IQueryObject AddParameter<T>(string parameterName, T value)
        {
            var parameter = CreateParameter();
            
            parameter.Direction = ParameterDirection.Input;
            parameter.ParameterName = parameterName;
            parameter.DbType = GetDbType(value);
            parameter.Value = GetDbValue(value);

            Parameters.Add(parameterName, parameter);

            return this;
        }

        public abstract IQueryObject AddTableParameter(string parameterName, DataTable table);

        public int Execute()
        {
            using (var cn = GetConnection())
            {
                cn.Open();

                using (var cmd = GetCommand(cn))
                {
                    return cmd.ExecuteNonQuery();
                }
            }            
        }

        public IEnumerable<T> ExecuteList<T>() where T : new()
        {
            using (var reader = ExecuteReader())
            {
                return reader.MapList<T>();
            }
        }                                     

        public IEnumerable<T> ExecuteList<T>(IObjectMapper<T> mapper)
        {
            using (var reader = ExecuteReader())
            {
                return reader.MapList(mapper);
            }
        }

        public T ExecuteObject<T>() where T : new()
        {
            using (var reader = ExecuteReader())
            {
                return reader.Read() ? reader.Map<T>() : default(T);                      
            }
        }

        public T ExecuteObject<T>(IObjectMapper<T> mapper)
        {
            using (var reader = ExecuteReader())
            {
                return reader.Read() ? reader.Map(mapper) : default(T);
            }
        }

        public T ExecuteScalar<T>()
        {
            using (var reader = ExecuteReader())
            {
                return reader.Read() ? reader.Read<T>(0) : default(T);
            }
        }        

        public T GetParameterValue<T>(string parameterName)
        {
            var value = Parameters[parameterName].Value;
            return value == DBNull.Value ? default(T) : (T)value;
        }

        private static object GetDbValue(object o) => o ?? DBNull.Value;        

        private static DbType GetDbType(object value)
        {
            if (value == null)
                return DbType.String;

            return GetDbType(value.GetType());
        }

        private static DbType GetDbType(Type type)
        {            
            if (type.IsGenericType)
                return GetDbType(type.GetGenericArguments()[0]);

            if (type == typeof(DBNull))
                return DbType.String;

            if (type.IsEnum)
                return DbType.Int32;

            try
            {
                return (DbType)Enum.Parse(typeof(DbType), type.Name);                
            }
            catch
            {
                throw new Exception("Unknown type: " + type.Name);
            }
        }

        private IDbCommand GetCommand(IDbConnection connection)
        {
            var cmd = connection.CreateCommand();

            cmd.CommandText = CommandText;
            cmd.CommandType = CommandType;
            cmd.CommandTimeout = Timeout;
            cmd.Prepare();

            return AddCommandParameters(cmd);
        }

        private IDbCommand AddCommandParameters(IDbCommand cmd)
        {
            foreach (var param in Parameters.Values)
            {
                cmd.Parameters.Add(param);
            }
            
            return cmd;
        }

        public IDataReader ExecuteReader()
        {
            var cn = GetConnection();
            var cmd = GetCommand(cn);

            cn.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public Task<IEnumerable<T>> ExecuteListAsync<T>(IObjectMapper<T> mapper) =>
            Task.Factory.StartNew(() => ExecuteList(mapper));
        

        public Task<IEnumerable<T>> ExecuteListAsync<T>() where T : new() =>
            Task.Factory.StartNew(() => ExecuteList<T>());

        public Task<T> ExecuteObjectAsync<T>(IObjectMapper<T> mapper) =>
            Task.Factory.StartNew(() => ExecuteObject(mapper));        

        public Task<T> ExecuteObjectAsync<T>() where T : new() =>
            Task.Factory.StartNew(() => ExecuteObject<T>());                              
    }
}
