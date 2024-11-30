using System.Collections.Generic;
using UnityEngine;

namespace Utility.Condition
{
    /// <summary>
    /// 컨디션을 하나만 가지는 컨디션입니다.
    /// 보유중인 컨디션이 없으면 true를 반환합니다.
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