public class LoginResponseHandler : PacketHandler<DtoAccount>
{
    public LoginResponseHandler(object data, EHandleType type) : base(data, type)
    {

    }

    protected override void OnFailed(DtoAccount data)
    {

    }

    protected override void OnSuccess(DtoAccount data)
    {

    }
}
