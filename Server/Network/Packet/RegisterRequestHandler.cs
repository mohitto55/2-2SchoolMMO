using System.Security.Principal;

public class LoginRequestHandler : PacketHandler<DtoAccount>
{
    public LoginRequestHandler(object data, EHandleType type) : base(data, type)
    {
        
    }

    protected override void OnFailed(DtoAccount data)
    {
    }

    protected override void OnSuccess(DtoAccount data)
    {

    }
}
