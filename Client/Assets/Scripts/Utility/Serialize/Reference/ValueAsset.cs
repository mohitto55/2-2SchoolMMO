using Sirenix.OdinInspector;
using UnityEngine;
using Utility.Serialize.Reference;

namespace Utility.Serialize.Reference
{ 
    /// <summary>
    /// �� ��ü�� ScroptableObjectȭ ���Ѽ� ����ȭ�ϱ� �����ϰ� �ϴ� Ŭ�����Դϴ�.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [System.Serializable]
    public class ValueAsset<T> : ScriptableObject
    {
        [ShowInInspector]
        public virtual T value { get; set; }
    }
}