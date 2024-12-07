using Server.Debug;
using Server.MySQL;
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
        DtoAccount dtoAccount = (DtoAccount)data;
        if (dtoAccount != null)
        {
            DatabaseManager.RegisterResult result = DatabaseManager.RegisterPlayer(dtoAccount);
            var handler = PacketHandlerPoolManager.GetPacketHandler(EHandleType.RegisterResponse);
            DtoMessage message = new DtoMessage();
            message.message = result.ToString();
            handler.Init(message, m_id);
            IOCPServer.SendClient(m_id, handler);
        }
    }
}
