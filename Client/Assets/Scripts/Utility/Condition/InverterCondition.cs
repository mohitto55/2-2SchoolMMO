using System.Collections.Generic;
using UnityEngine;

namespace Utility.Condition
{
    /// <summary>
    /// �������� ������� �ݴ�Ǵ� ����� ��ȯ�մϴ�.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InverterCondition<T> : SingleCondition
    {
        public InverterCondition() : base()
        {
        }
        
        public InverterCondition(ICondition condition) : base(condition)
        {
            this.condition = condition;
        }
        
        public override bool Evaluate()
        {
            return !base.Evaluate();
        }
    }
}