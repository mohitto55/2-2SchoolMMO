using System.Collections.Generic;
using UnityEngine;

namespace Utility.Condition
{
    /// <summary>
    /// 가지고 있는 컨디션들 중 하나라도 true면 true를 반환합니다.
    /// 가지고 있는 컨디션들이 모두 false일떄만 false를 반환합니다.
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
