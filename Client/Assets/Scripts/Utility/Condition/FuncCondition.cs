using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.Condition
{
    /// <summary>
    /// 테스트용으로 사용할 컨디션입니다.
    /// 람다를 컨디션으로 사용할 수 있습니다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FuncCondition : ConditionBase
    {
        protected Func<bool> _condition;
        public FuncCondition() : base()
        {
            _condition = null;
        }
        
        public FuncCondition(Func<bool> func)
        {
            this._condition = func;
        }
        
        public override bool Evaluate()
        {
            if (_condition != null)
                return _condition.Invoke();
            return true;
        }
    }
}