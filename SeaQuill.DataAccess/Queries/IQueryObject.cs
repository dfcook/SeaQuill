using SeaQuill.DataAccess.Mapping;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SeaQuill.DataAccess.Queries
{
    public interface IQueryObject
    {
        IQueryObject AddParameter<T>(string parameterName, T value);
        IQueryObject AddInputOutputParameter<T>(string parameterName, T value);
        IQueryObject AddOutputParameter<T>(string parameterName);
        IQueryObject AddTableParameter(string parameterName, DataTable table);

        T GetParameterValue<T>(string parameterName);

        IDataReader ExecuteReader();

        IEnumerable<T> ExecuteList<T>(IObjectMapper<T> mapper);        

        IEnumerable<T> ExecuteList<T>() where T : new();

        Task<IEnumerable<T>> ExecuteListAsync<T>(IObjectMapper<T> mapper);

        Task<IEnumerable<T>> ExecuteListAsync<T>() where T : new();

        T ExecuteObject<T>(IObjectMapper<T> mapper);

        T ExecuteObject<T>() where T : new();

        Task<T> ExecuteObjectAsync<T>(IObjectMapper<T> mapper);

        Task<T> ExecuteObjectAsync<T>() where T : new();        

        int Execute();

        T ExecuteScalar<T>();
    }
}
