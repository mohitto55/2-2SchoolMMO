using Arch.Core;
using Arch.Core.Extensions;

public class NPC : GameObject
{
    DtoNPCData _npcData;

    public NPC(Entity entity)
    {
        _npcData = new DtoNPCData();
        this.entity = entity;
    }

    public override void SaveEcsData()
    {
    }
}