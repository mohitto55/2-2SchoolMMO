using System.Collections.Generic;
using UnityEngine;

namespace Utility.Condition
{
    /// <summary>
    /// ������ �ִ� ����ǵ� �� �ϳ��� false�� false�� ��ȯ�մϴ�.
    /// ������ �ִ� ����ǵ��� ��� true�ϋ��� true�� ��ȯ�մϴ�.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SequenceCondition : ConditionBase
    {
        public HashSet<ICondition> conditions;

        public SequenceCondition()
        {
            this.conditions = new HashSet<ICondition>();
        }
        
        public SequenceCondition(ICondition condition)
        {
            this.conditions = new HashSet<ICondition>();
            this.conditions.Add(condition);
        }
        public SequenceCondition(HashSet<ICondition> conditions)
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