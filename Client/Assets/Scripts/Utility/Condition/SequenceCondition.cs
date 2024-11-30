using System.Collections.Generic;
using UnityEngine;

namespace Utility.Condition
{
    /// <summary>
    /// 가지고 있는 컨디션들 중 하나라도 false면 false를 반환합니다.
    /// 가지고 있는 컨디션들이 모두 true일떄만 true를 반환합니다.
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