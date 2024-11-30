using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.Condition
{
    /// <summary>
    /// ������ �˻��ϰ� �˸´� ���� ��ȯ�ϴ� Ŭ�����Դϴ�.
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