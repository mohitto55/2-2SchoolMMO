
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    IAction<Vector2> _moveAction;
    IAction<Vector2> _positionSyncAction;



    Dictionary<string, ICondition> _conditions = new Dictionary<string, ICondition>();

    protected override void Awake()
    {
        _moveAction = new Action_Move();
    }


    /// <summary>
    /// ������� ���Խ�ŵ�ϴ�
    /// </summary>
    /// <param name="condition"></param>
    private void RegisterCondition(ICondition condition)
    {
        string key = condition.GetType().Name;

        if (!_conditions.TryAdd(key, condition))
        {
            Debug.LogError($"Already exist condition :: {key}");
        }
    }

    /// <summary>
    /// ������� ã���ϴ�
    /// </summary>
    /// <typeparam name="TReturn"></typeparam>
    /// <param name="conditionName">Condition_ �� ������ ������̸�</param>
    /// <returns></returns>
    public ICondition<TReturn> GetCondition<TReturn>(string conditionName)
    {
        if (_conditions.TryGetValue($"Condition_{conditionName}", out ICondition condition))
        {
            if (condition is ICondition<TReturn>)
            {
                return condition as ICondition<TReturn>;
            }
            else
            {
                Debug.LogError($"not match condition return value :: Condition_{conditionName} return {typeof(TReturn).Name}");
                return null;
            }
        }
        else
        {
            Debug.LogError($"didn't find :: Condition_{conditionName}");
            return null;
        }
    }
    /// <summary>
    /// ������� Ÿ������ ã���ϴ�
    /// </summary>
    /// <typeparam name="TCondition"></typeparam>
    /// <returns></returns>
    public TCondition GetCondition<TCondition>() where TCondition : class, ICondition
    {
        string key = typeof(TCondition).Name;

        if (_conditions.TryGetValue(key, out ICondition condition))
        {
            return condition as TCondition;
        }
        else
        {
            Debug.LogError($"didn't find :: {key}");
            return null;
        }
    }

}
