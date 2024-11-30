using System.Collections.Generic;
using UnityEngine;

namespace Utility.Condition
{
    /// <summary>
    /// ������� �ϳ��� ������ ������Դϴ�.
    /// �������� ������� ������ true�� ��ȯ�մϴ�.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingleCondition : ConditionBase
    {
        public ICondition condition;

        public SingleCondition()
        {
            condition = null;
        }
        
        public SingleCondition(ICondition condition)
        {
            this.condition = condition;
        }
        
        public override bool Evaluate()
        {
            if (condition == null)
                return true;
            
            if (condition.Evaluate())
                return true;
            return false;
        }
    }
}