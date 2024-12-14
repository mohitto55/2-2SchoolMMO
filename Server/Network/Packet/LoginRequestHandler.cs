using Arch.Core;
using Server.Debug;
using Server.MySQL;
using System.Security.Principal;
using static Server.MySQL.DatabaseManager;

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
        LoginResult loginResult = DatabaseManager.UserLogin(data.id, data.password);
        string sendMessage = loginResult.ToString();

        var packetHandler = PacketHandlerPoolManager.GetPacketHandler(EHandleType.LoginResponse);
        DtoMessage message = new DtoMessage();
        message.message = sendMessage;
        packetHandler.Init(message, m_id);
        IOCPServer.SendClient(m_id, packetHandler);


        // 플레이어 캐릭터 생성
        var client = IOCPServer.ChangeClientID(m_id, $"{data.id}:{data.password}");
        client.m_id = $"{data.id}:{data.password}";
        ServerDebug.Log(LogType.Log, $"{data.id}:{data.password} : Success Create Player!");
        var gameObject = ObjectManager.CreatePlayer();
        client.characterObject = gameObject;

        IOCPServer.SendAllClient(EHandleType.Transform, new DtoTransform()
        {

            dtoPosition = new DtoVector()
            {
                x = 0,
                y = 0
            }
        });
    }
}
