
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class StateMachine<TTarget> : MonoBehaviour
{
    // ������Ʈ�� ����� Ÿ��
    protected TTarget _target;

    // ������Ʈ Ǯ
    Dictionary<string, State_Base<TTarget>> _states = new Dictionary<string, State_Base<TTarget>>();

    string _currentState;
   
    /// <summary>
    /// ������Ʈ�� �����մϴ�.
    /// </summary>
    protected void ChangeState<TState>() where TState : State_Base<TTarget> { }
 
    /// <summary>
    /// ������Ʈ�� �߰��մϴ�.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <returns></returns>
    protected TState AddState<TState>() where TState : State_Base<TTarget>, new()
    {
        string key = typeof(TState).Name;

        if (_states.ContainsKey(key))
        {
            Debug.LogError($"Aready register state :: {key}");
            return null;
        }

        var state = new TState(); 
        _states.Add(key, state);

        return state;
    }

    public void SetTarget(TTarget target)
    {
        _target = target;
    }
    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        // ���� ù��° ���¸� ���ۻ��·� ����
        _currentState = _states.GetEnumerator().Current.Key;
    }
    protected virtual void Update()
    {
        _states[_currentState]?.Idle(_target);
    }
    protected virtual void FixedUpdate()
    {
        _states[_currentState]?.FixedIdle(_target);
    }
}