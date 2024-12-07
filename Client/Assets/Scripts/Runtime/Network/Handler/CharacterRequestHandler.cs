using UnityEngine;

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

    }
}
