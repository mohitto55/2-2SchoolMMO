using UnityEngine;


/// <summary>
/// 
/// </summary>
namespace Utility.Action
{
    public interface IAction
    {
        void ActionExecute();
    }
    
    public interface IAction<TTarget>
    {
        void ActionExecute(TTarget target);
    }
}