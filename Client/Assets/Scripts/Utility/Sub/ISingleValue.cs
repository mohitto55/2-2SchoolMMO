using UnityEngine;

namespace Utility.Sub
{
    public interface ISingleValue<TResult>
    {
        TResult GetSingleValue();
    }
}
