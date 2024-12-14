using Sirenix.OdinInspector;
using UnityEngine;
using Utility.Serialize.Reference;

namespace Utility.Serialize.Reference
{ 
    /// <summary>
    /// 값 자체를 ScroptableObject화 시켜서 직렬화하기 수월하게 하는 클래스입니다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [System.Serializable]
    public class ValueAsset<T> : ScriptableObject
    {
        [ShowInInspector]
        public virtual T value { get; set; }
    }
}