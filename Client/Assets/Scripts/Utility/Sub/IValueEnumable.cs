using System.Collections.Generic;
using UnityEngine;

namespace Utility.Sub
{
    /// <summary>
    /// 오딘의 ValueDropdown을 사용할때 다른 객체의 리스트 값을 가져올 수 있습니다.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IValueEnumerable<TResult>
    {
        IEnumerable<ISingleValue<TResult>> GetValues();
    }
}