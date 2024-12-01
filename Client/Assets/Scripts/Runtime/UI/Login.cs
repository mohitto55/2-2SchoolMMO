using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;

public class LoginView : UIView
{
    [SerializeField] private TMP_InputField _loginIdField;
    [SerializeField] private TMP_InputField _loginPasswordField;

    [SerializeField] private TMP_InputField _registerIdField;
    [SerializeField] private TMP_InputField _registerPasswordField;
    [SerializeField] private TMP_InputField _registerPasswordConfirmField;
    public void TryLogin()
    {
        string idString = _loginIdField.text;
        string passwordString = _loginPasswordField.text;

        DtoAccount account = new DtoAccount(idString, passwordString);
        NetworkManager.Instance.SendPacket(EHandleType.LoginRequest, account);
    }

    public void TryRegister()
    {
        string idString = _registerIdField.text;
        string passwordString = _registerPasswordField.text;
        string passwordConfirmString = _registerPasswordConfirmField.text;

        DtoAccount account = new DtoAccount(idString, passwordString);
        NetworkManager.Instance.SendPacket(EHandleType.RegisterRequest, account);
    }

    protected override void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {

    }
}
