using System.Collections.Generic;
using UnityEngine;

namespace Utility.Condition
{
    /// <summary>
    /// 보유중인 컨디션의 반대되는 결과를 반환합니다.
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