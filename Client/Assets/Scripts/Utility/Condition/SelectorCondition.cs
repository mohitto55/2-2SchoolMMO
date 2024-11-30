using System.Collections.Generic;
using UnityEngine;

namespace Utility.Condition
{
    /// <summary>
    /// ������ �ִ� ����ǵ� �� �ϳ��� true�� true�� ��ȯ�մϴ�.
    /// ������ �ִ� ����ǵ��� ��� false�ϋ��� false�� ��ȯ�մϴ�.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SelectorCondition : ConditionBase
    {
        public HashSet<ICondition> conditions;

        public SelectorCondition()
        {
            this.conditions = new HashSet<ICondition>();
        }
        
        public SelectorCondition(ICondition condition)
        {
            this.conditions = new HashSet<ICondition>();
            this.conditions.Add(condition);
        }
        public SelectorCondition(HashSet<ICondition> conditions)
        {
            this.conditions = conditions;
        }
        public override bool Evaluate()
        {
            foreach (ICondition condition in conditions)
            {
                if (!condition.Evaluate())
                    return false;
            }
            return true;
        }
    }
}
