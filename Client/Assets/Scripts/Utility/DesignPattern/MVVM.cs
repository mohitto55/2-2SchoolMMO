using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Runtime.DB.Model;
using UnityEngine;
using UnityEngine.Events;



/// <summary>
/// Based Window Model
/// </summary>
public abstract class UIWindowModel : IModel
{
    bool m_active;
}


/// <summary>
/// MVVM Pattern
/// Based Window ViewModel (Non Generic)
/// -> Use UIWindowViewModel<Model> (Generic)
/// </summary>
public abstract class ViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] String propertyName = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

}
/// <summary>
/// MVVM Pattern
/// Based Window ViewModel
/// </summary>
public abstract class ViewModel<TModel> : ViewModel
{
    public ViewModel(TModel model)
    {
        m_model = model;
    }
    [SerializeField]
    protected TModel m_model;
}

public abstract class View : MonoBehaviour
{
    protected abstract void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e);

    /// <summary>
    /// ViewModel Binding
    /// </summary>
    public virtual void RegisterViewModel(ViewModel vm)
    {
        vm.PropertyChanged += OnViewModelPropertyChanged;
    }
}
[RequireComponent(typeof(CanvasGroup))]
public abstract class UIView : View
{
    /*==========================[ STATIC ]===============================*/
    [SerializeField]
    protected string m_viewID;
    public string ViewID { get => m_viewID;  }

    protected virtual void Awake()
    {
        AddUIView(m_viewID, this);
    }
    static Dictionary<string, UIView> g_uiWindowsDic = new Dictionary<string, UIView>();
    public static Stack<UIView> g_popupView = new Stack<UIView>();
    public static Stack<IControllable> g_controllable = new Stack<IControllable>();
    
    public static bool TryGetView<T>(string viewID, out T view) where T : UIView
    {
        UIView retval;
        if (g_uiWindowsDic.TryGetValue(viewID, out retval))
        {
            view = (T)retval;
            return true;
        }
        view = null;
        return false;
    }
    private void AddUIView(string viewID, UIView view)
    {
        if (string.IsNullOrEmpty(viewID))
        {
            Debug.LogWarning($"Invalid view ID. {gameObject.name}");
            viewID = gameObject.name;
        }
        if (!g_uiWindowsDic.ContainsKey(viewID))
        {
            g_uiWindowsDic.Add(viewID, view);
        }
        else
        {
            g_uiWindowsDic[viewID] = view;
        }
    }
    CanvasGroup m_canvasGroup;
    public CanvasGroup canvasGroup
    {
        get
        {
            if (!m_canvasGroup)
            {
                m_canvasGroup = GetComponent<CanvasGroup>();
            }
            return m_canvasGroup;
        }
    }

    public virtual void Show()
    {
        SetCanvasGroup(true);
    }

    public virtual void Hide()
    {
        SetCanvasGroup(false);
    }
    protected void SetCanvasGroup(bool isActive, bool blocksRaycast = true)
    {
        canvasGroup.alpha = (isActive ? 1 : 0);
        canvasGroup.interactable = isActive;
        canvasGroup.blocksRaycasts = isActive && blocksRaycast;
    }



    public bool IsActive()
    {
        return canvasGroup.alpha != 0;
    }
}

public interface IControllable
{
    public UnityEvent onUpArrow { get; set; }
    public UnityEvent onDownArrow { get; set; }
    public UnityEvent onRightArrow { get; set; }
    public UnityEvent onLeftArrow { get; set; }
    public UnityEvent onReturn { get; set; }
}
public abstract class UIPopup : UIView, IControllable
{
    public UnityEvent onUpArrow { get; set; }
    public UnityEvent onDownArrow { get; set; }
    public UnityEvent onRightArrow { get; set; }
    public UnityEvent onLeftArrow { get; set; }
    public UnityEvent onReturn { get; set; }

    public override void Show()
    {
        base.Show();
        g_popupView.Push(this);
        g_controllable.Push(this);
    }
    public override void Hide()
    {
        base.Hide();
        while (true)
        {
            var view = g_popupView.Pop();
            g_controllable.Pop();

            if (view.ViewID == ViewID) return;

           view.Hide();
        }
    }

}