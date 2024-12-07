using System.Security.Principal;

public class RegisterRequestHandler : PacketHandler<DtoAccount>
{
    public RegisterRequestHandler(object data, EHandleType type) : base(data, type)
    {
    }

    protected override void OnFailed(DtoAccount data)
    {
    }

    protected override void OnSuccess(DtoAccount data)
    {
    }
}
