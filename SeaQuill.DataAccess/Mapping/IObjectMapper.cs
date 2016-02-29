﻿using System.Collections.Generic;
using System.Data;

namespace SeaQuill.DataAccess.Mapping
{
    public interface IObjectMapper
    {
    }

    public interface IObjectMapper<T> : IObjectMapper
    {        
        T Map(IDataReader reader);
        ICollection<T> MapList(IDataReader reader);
    }
}
