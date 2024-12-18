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
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (!NetworkManager.Instance.AutoLoginTry)
            SceneManager.LoadScene("InGameScene");

        
    }

    protected void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        NetworkManager.Instance.SendPacket(EHandleType.PlayerObjectIDRequest, new DtoObjectInfo());
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
