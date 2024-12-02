using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System;


public class LoginView : UIView
{
    [SerializeField] private TMP_InputField _loginIdField;
    [SerializeField] private TMP_InputField _loginPasswordField;

    [SerializeField] private TMP_InputField _registerIdField;
    [SerializeField] private TMP_InputField _registerUserNameField;
    [SerializeField] private TMP_InputField _registerPasswordField;
    [SerializeField] private TMP_InputField _registerPasswordConfirmField;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("메시지 보내기");
            DtoAccount account = new DtoAccount("Mohitto", "jjs", "2024");
            NetworkManager.Instance.SendPacket(EHandleType.RegisterRequest, account);
        }
    }
    public void TryLogin()
    {
        string idString = _loginIdField.text;
        string passwordString = _loginPasswordField.text;

        //DtoAccount account = new DtoAccount(idString, passwordString);
        //NetworkManager.Instance.SendPacket(EHandleType.LoginRequest, account);
    }

    public void TryRegister()
    {
        string usernameString = _registerUserNameField.text;
        string idString = _registerIdField.text;
        string passwordString = _registerPasswordField.text;
        string passwordConfirmString = _registerPasswordConfirmField.text;

        DtoAccount account = new DtoAccount(usernameString, idString, passwordString);
        NetworkManager.Instance.SendPacket(EHandleType.RegisterRequest, account);
    }
    protected override void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {

    }
}
