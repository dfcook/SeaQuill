using System;
using System.Runtime.Serialization;

namespace SeaQuill.DataAccess.Exceptions
{
    [Serializable]
    public sealed class ColumnMissingException : Exception
    {
        public ColumnMissingException(SerializationInfo info, StreamingContext context) : 
            base(info, context)
        {            
            ColumnName = info.GetString("columnName");
        }

        public string ColumnName { get; }

        public ColumnMissingException(string columnName) : 
            base(string.Format("Unable to find column {0} in the resultset", columnName))
        {
            ColumnName = columnName;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("columnName", ColumnName);
        }
    }
}
