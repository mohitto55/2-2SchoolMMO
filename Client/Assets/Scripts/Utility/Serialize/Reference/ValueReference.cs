using Sirenix.OdinInspector;
using UnityEngine;

namespace Utility.Serialize.Reference
{
    /// <summary>
    /// ����ü�� Scriptableȭ ��Ų ValueAsset�� �̿��� ���� ���� ��ü���� �����ؼ� ������ �� �ְ��մϴ�.
    /// </summary>
    /// <typeparam name="TValue">�����Ϸ��� ���� Ÿ���Դϴ�.</typeparam>
    /// <typeparam name="TAsset">TValue Ÿ���� ���� �����ϰ� �ִ� ValueAsset�Դϴ�.</typeparam>
    [InlineProperty]
    [LabelWidth(75)]
    [System.Serializable]
    public abstract class ValueReference<TValue, TAsset> where TAsset : ValueAsset<TValue>
    {
        /// <summary>
        /// false�� ���� �����ϴ� ���۷��� ��, true�� �� ��ü������ ������ �� �ִ� ���� ���� ����մϴ�.
        /// </summary>
        [HorizontalGroup("Reference", MaxWidth = 100)] [ValueDropdown("valueList")] [HideLabel] [SerializeField]
        protected bool useValue = true;

        /// <summary>
        /// �� ��ü�� ������ ��ü������ ������ �� �ִ� ���� ���Դϴ�.
        /// </summary>
        [ShowIf("useValue", Animate = false)] [HorizontalGroup("Reference")] [HideLabel] [SerializeField]
        protected TValue _value;

        /// <summary>
        /// AssetValue ��ü�Դϴ�.
        /// </summary>
        [HideIf("useValue", Animate = false)]
        [HorizontalGroup("Reference")]
        [OnValueChanged("UpdateAsset")]
        [HideLabel]
        [SerializeField]
        protected TAsset assetReference;

        /// <summary>
        /// AssetValue ���� ������ �� �ִ��� �����Դϴ�.
        /// </summary>
        [ShowIf("@useValue == false")] [LabelWidth(100)] [SerializeField]
        protected bool editAsset;

        /// <summary>
        /// AssetValue ��ü������ AssetValue ������ ������Ƽ ���� �巯���ϴ�.
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