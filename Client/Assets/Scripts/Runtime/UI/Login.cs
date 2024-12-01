using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField] private InputField _idField;
    [SerializeField] private InputField _passwordField;
    public void TryLogin()
    {
        string loginString = _idField.text;
        string passwordString = _passwordField.text;

        DtoAccount account = new DtoAccount(loginString, passwordString);
        NetworkManager.Instance.SendPacket(EHandleType.LoginRequest, account);
    }
}
