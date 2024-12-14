using Server.Debug;
using Server.MySQL;
using System.Security.Principal;

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

    }
}
