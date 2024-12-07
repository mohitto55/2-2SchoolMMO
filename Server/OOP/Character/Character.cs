using Arch.Core;
using Arch.Core.Extensions;

public class Character : GameObject
{
    public Character(Entity entity)
    {
        _characterData = new DtoUserCharacterData();
    }

    DtoUserCharacterData _characterData;
    Entity _entity;

    public override void SaveEcsData()
    {
    }
}