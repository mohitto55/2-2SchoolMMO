using System.Security.Principal;

public class RegisterResponseHandler : PacketHandler<DtoMessage>
{
    public RegisterResponseHandler(object data, EHandleType type) : base(data, type)
    {
    }

    protected override void OnFailed(DtoMessage data)
    {
    }

    protected override void OnSuccess(DtoMessage data)
    {
    }
}
