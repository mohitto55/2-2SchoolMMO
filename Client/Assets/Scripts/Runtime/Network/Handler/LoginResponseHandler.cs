using UnityEngine.SceneManagement;

public class LoginResponseHandler : PacketHandler<DtoMessage>
{
    public LoginResponseHandler(object data, EHandleType type) : base(data, type)
    {

    }

    protected override void OnFailed(DtoMessage data)
    {

    }

    protected override void OnSuccess(DtoMessage data)
    {
        if(!NetworkManager.Instance.AutoLoginTry)
            SceneManager.LoadScene("InGameScene");

        NetworkManager.Instance.SendPacket(EHandleType.PlayerObjectIDRequest, new DtoObjectInfo());
    }
}
