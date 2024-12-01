using R3;
using System;
using UnityEngine;
using UnityEngine.Events;


public class View : MonoBehaviour
{
    private IViewModel _model;

    public ReactiveProperty<int> a = new ReactiveProperty<int>();
    public UnityEvent<object> b = new UnityEvent<object>();
    protected virtual void Start()
    {

    }
    public void RegisterViewModelProperty(IObservable<string> property)
    {
         
    }
}
