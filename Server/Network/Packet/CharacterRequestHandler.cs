using Server.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Network.Packet
{
    public class CharacterRequestHandler : PacketHandler<DtoUserCharacterData>
    {
        public CharacterRequestHandler(object data, EHandleType type) : base(data, type)
        {
        }

        protected override void OnFailed(DtoUserCharacterData data)
        {

        }

        protected override void OnSuccess(DtoUserCharacterData data)
        {
            ServerDebug.Log(LogType.Log, "캐릭터 데이터 썩세스");
            ServerDebug.Log(LogType.Log, "캐릭터");
            //ServerDebug.Log(LogType.Log, data.characters.name);
            //foreach (var character in data.characters)
            //{
            //}
        }
    }
}
