using Sirenix.OdinInspector;
using UnityEngine;

namespace Utility.Serialize.Reference
{
    /// <summary>
    /// 값자체를 Scriptable화 시킨 ValueAsset을 이용해 값을 여러 객체에서 연결해서 공유할 수 있게합니다.
    /// </summary>
    /// <typeparam name="TValue">공유하려는 값의 타입입니다.</typeparam>
    /// <typeparam name="TAsset">TValue 타입의 값을 소유하고 있는 ValueAsset입니다.</typeparam>
    [InlineProperty]
    [LabelWidth(75)]
    [System.Serializable]
    public abstract class ValueReference<TValue, TAsset> where TAsset : ValueAsset<TValue>
    {
        /// <summary>
        /// false면 서로 공유하는 레퍼런스 값, true면 이 객체에서만 수정할 수 있는 복사 값을 사용합니다.
        /// </summary>
        [HorizontalGroup("Reference", MaxWidth = 100)] [ValueDropdown("valueList")] [HideLabel] [SerializeField]
        protected bool useValue = true;

        /// <summary>
        /// 이 객체를 소유한 객체에서만 수정할 수 있는 복사 값입니다.
        /// </summary>
        [ShowIf("useValue", Animate = false)] [HorizontalGroup("Reference")] [HideLabel] [SerializeField]
        protected TValue _value;

        /// <summary>
        /// AssetValue 객체입니다.
        /// </summary>
        [HideIf("useValue", Animate = false)]
        [HorizontalGroup("Reference")]
        [OnValueChanged("UpdateAsset")]
        [HideLabel]
        [SerializeField]
        protected TAsset assetReference;

        /// <summary>
        /// AssetValue 값을 수정할 수 있는지 여부입니다.
        /// </summary>
        [ShowIf("@useValue == false")] [LabelWidth(100)] [SerializeField]
        protected bool editAsset;

        /// <summary>
        /// AssetValue 객체이지만 AssetValue 내부의 프로퍼티 값만 드러납니다.
        /// </summary>
        [ShowIf("@useValue == false")]
        [EnableIf("editAsset")]
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        [SerializeField]
        protected TAsset _assetReference;


        private static ValueDropdownList<bool> valueList = new ValueDropdownList<bool>()
        {
            { "Value", true },
            { "Reference", false },
        };

        public TValue value
        {
            get
            {
                if (assetReference == null || useValue)
                    return _value;
                else
                {
                    return assetReference.value;
                }
            }
        }

        protected void UpdateAsset()
        {
            _assetReference = assetReference;
        }

        public static implicit operator TValue(ValueReference<TValue, TAsset> valueRef)
        {
            return valueRef.value;
        }
    }
}