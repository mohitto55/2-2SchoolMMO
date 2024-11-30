using UnityEngine;

namespace Utility.Condition{
    public interface ICondition
    {
        bool Evaluate();
    }

    public interface ICondition<TTarget>
    {
        bool Evaluate(TTarget target);
    }
}