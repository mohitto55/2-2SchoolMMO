using System;
using System.Collections.Generic;
public class Transition : ITransition
{
    Action _transitionEvent;

    public Transition()
    {

    }
    public Transition(Action transitionAction)
    {
        _transitionEvent = transitionAction;
    }

    class ContainerCondition
    {
        public ContainerCondition(ICondition<bool> condition, bool equal)
        {
            _condition = condition;
            _equal = equal;
        }
        ICondition<bool> _condition;
        bool _equal;

        public bool IsEqual()
        {
            return _condition.GetCondition() == _equal;
        }
        public string GetConditionTypeString()
        {
            return _condition.GetType().Name;
        }

    }

    List<ContainerCondition> _conditions = new List<ContainerCondition>();

    /// <summary>
    /// Ʈ������ �ɶ� ������ �׼��Դϴ�.
    /// </summary>
    /// <param name="transitionAction"></param>
    public void SetTransitionAction(Action transitionAction)
    {
        _transitionEvent = transitionAction;
    }

    /// <summary>
    /// Ʈ�������� �����ϴٸ� Ʈ������ �մϴ�
    /// </summary>
    /// <returns></returns>
    public void TryTransition()
    {
        if(!_conditions.Exists(_ => !_.IsEqual()))
        {
            _transitionEvent?.Invoke();
        }
    }

    /// <summary>
    /// ������ �߰��մϴ�
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="equal"></param>
    public Transition AddCondition(ICondition<bool> condition, bool equal = true)
    {
        var conditionName = condition.GetType().Name;
        // ���� ���� Ŭ������ �����Ѵٸ�.
        if (_conditions.Exists(_ => _.GetConditionTypeString() == conditionName))
        {
            Debug.LogError($"Already exist condition type :: {conditionName} in {GetType().Name}");
        }
        else
        {
            _conditions.Add(new ContainerCondition(condition, equal));
        }
        return this;
    }

}