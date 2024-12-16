using Arch.Core;
using Arch.Core.Extensions;

public class Character : GameObject
{
    public Character(Entity entity)
    {
        _characterData = new DtoUserCharacterData();
        this.entity = entity;
    }

    DtoUserCharacterData _characterData;

    public override void SaveEcsData()
    {
    }
}