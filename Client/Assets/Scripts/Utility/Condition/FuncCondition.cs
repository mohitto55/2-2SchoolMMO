using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.Condition
{
    /// <summary>
    /// �׽�Ʈ������ ����� ������Դϴ�.
    /// ���ٸ� ��������� ����� �� �ֽ��ϴ�.
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