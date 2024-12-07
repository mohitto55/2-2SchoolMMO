using UnityEngine;

namespace Utility.Serilize
{
    public abstract class DataExporter<T>
    {
        public abstract string DataType { get; }

        public abstract string Export(T data);
    }

    public abstract class DataExporter<T1, T2>
    {
        public abstract string DataType { get; }

        public abstract string Export(T1 data, T2 param);
    }
}