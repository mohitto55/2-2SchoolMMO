using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.Condition
{
    /// <summary>
    /// 조건을 검사하고 알맞는 값을 반환하는 클래스입니다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ConditionBase : ICondition
    {
        public ConditionBase() {}

        public virtual bool Evaluate()
        {
            return true;
        }
    }
}