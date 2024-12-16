using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Utility.Serialize.Reference
{
    [InlineProperty]
    [LabelWidth(75)]
    [System.Serializable]
    public class ValueOnlyReference<TAsset>
    {
        [OnInspectorGUI("UpdateAsset")]
        [OnValueChanged("UpdateAsset")]
        [SerializeReference, SerializeField]
        protected TAsset assetReference;

        protected void UpdateAsset()
        {
            
            //if (assetReference == null)
            //{
            //    assetReference = ScriptableObject.CreateInstance<ValueAsset<TAsset>>();
            //}
        }
    }
}