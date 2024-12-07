using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System;
using System.Diagnostics;
using System.Security.Principal;


public class LoginView : UIView
{
    [SerializeField] private TMP_InputField _loginIdField;
    [SerializeField] private TMP_InputField _loginPasswordField;

    [SerializeField] private TMP_InputField _registerIdField;
    [SerializeField] private TMP_InputField _registerUserNameField;
    [SerializeField] private TMP_InputField _registerPasswordField;
    [SerializeField] private TMP_InputField _registerPasswordConfirmField;

    [SerializeField] private TextMeshProUGUI _messageText;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("메시지 보내기");
            //DtoAccount loginAccount = new DtoAccount("", "jjs", "20242");
            //NetworkManager.Instance.RegisterResponseCallback(EHandleType.LoginResponse, LoginCallback);
            //NetworkManager.Instance.SendPacket(EHandleType.LoginRequest, loginAccount);

            //DtoAccount registerAccount = new DtoAccount("Mohitto", "jjs", "2024");
            //NetworkManager.Instance.RegisterResponseCallback(EHandleType.RegisterResponse, RegisterCallback);
            //NetworkManager.Instance.SendPacket(EHandleType.RegisterRequest, registerAccount);
            DtoUserCharacterData userCharacterData = new DtoUserCharacterData();
            //userCharacterData.characters = new DtoCharacter[2];
            //userCharacterData.characters = new DtoCharacter();
            //userCharacterData.characters.name = "테스트이름1";
            //userCharacterData.characters[1] = new DtoCharacter();
            //userCharacterData.characters[1].name = "테스트이름2";

            NetworkManager.Instance.SendPacket(EHandleType.CharacterRequest, userCharacterData);
        }
    }
    public void TryLogin()
    {
        string idString = _loginIdField.text;
        string passwordString = _loginPasswordField.text;

        DtoAccount loginAccount = new DtoAccount("", idString, passwordString);
        NetworkManager.Instance.RegisterResponseCallback(EHandleType.LoginResponse, LoginCallback);
        NetworkManager.Instance.SendPacket(EHandleType.LoginRequest, loginAccount);
    }

    public void TryRegister()
    {
        string usernameString = _registerUserNameField.text;
        string idString = _registerIdField.text;
        string passwordString = _registerPasswordField.text;
        string passwordConfirmString = _registerPasswordConfirmField.text;

        if(passwordString.Length == 0 || passwordConfirmString.Length == 0)
        {
            _messageText.text = "Write a password";
            return;
        }

        if (passwordString.Length > 0 && passwordConfirmString.Length > 0 && passwordString != passwordConfirmString)
        {
            _messageText.text = "Password Does Not Match";
            return;
        }
        DtoAccount account = new DtoAccount(usernameString, idString, passwordString);
        NetworkManager.Instance.RegisterResponseCallback(EHandleType.RegisterResponse, RegisterCallback);
        NetworkManager.Instance.SendPacket(EHandleType.RegisterRequest, account);




        //DtoUserCharacterData userCharacterData = new DtoUserCharacterData();
        ////userCharacterData.characters = new DtoCharacter[2];
        ////userCharacterData.characters = new DtoCharacter();
        ////userCharacterData.characters.name = "테스트이름1";
        ////userCharacterData.characters[1] = new DtoCharacter();
        ////userCharacterData.characters[1].name = "테스트이름2";

        //NetworkManager.Instance.SendPacket(EHandleType.CharacterRequest, account);
    }
    protected override void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
    }

    protected void LoginCallback(object data)
    {
        DtoMessage message = (DtoMessage)data;
        switch (message.message)
        {
            case "Success":
                _messageText.text = "Register Success!";
                break;
            case "NonexistentId":
                _messageText.text = "It's a non existent ID";
                break;
            case "PasswordDoesNotMatch":
                _messageText.text = "Password Does Not Match";
                break;
        }
    }
    protected void RegisterCallback(object data)
    {
        DtoMessage message = (DtoMessage)data;
        switch (message.message)
        {
            case "Success":
                _messageText.text = "Register Success!";
                break;
            case "Faild":
                _messageText.text = "Register Faild...";
                break;
            case "NotValidUsername":
                _messageText.text = "User name is not valid.";
                break;
            case "AlreadyRegister":
                _messageText.text = "This name is already registered.";
                break;
        }
    }
}
