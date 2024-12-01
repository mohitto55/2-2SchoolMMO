using UnityEngine;
using UnityEngine.UI;

public class TestViewButton : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] View _view;

    public void Awake()
    {
        _button.onClick.AddListener(() =>
        {
            _view.gameObject.SetActive(false);
        });
    }
}
