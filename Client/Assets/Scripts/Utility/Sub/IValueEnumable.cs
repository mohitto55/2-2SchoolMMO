using System.Collections.Generic;
using UnityEngine;

namespace Utility.Sub
{
    /// <summary>
    /// ������ ValueDropdown�� ����Ҷ� �ٸ� ��ü�� ����Ʈ ���� ������ �� �ֽ��ϴ�.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IValueEnumerable<TResult>
    {
        IEnumerable<ISingleValue<TResult>> GetValues();
    }
}