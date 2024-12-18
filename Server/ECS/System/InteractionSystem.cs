using Arch.Core;
using Arch.Core.Extensions;
using Server.Debug;
using Server.MySQL;

public class InteractionSystem : ComponentSystem
{
    public override void Tick(World world, double dt)
    {
        var query = new QueryDescription().WithAll<BoundColliderComponent, InteractionComponent>();

        world.Query(in query, (Entity entityA,
         ref BoundColliderComponent boundA,
         ref InteractionComponent interactionA) =>
        {
            if(interactionA.useDisableTimer)
            {
                interactionA.lastDisableTimer = Math.Max(0.0, interactionA.lastDisableTimer - dt);
                interactionA.canAction = interactionA.lastDisableTimer > 0 ? true : false;
            }

            if (!interactionA.canAction)
                return;

            if (boundA.contactsEntity.Count <= 0)
                return;

            if (interactionA.type == EEntityType.Player)
                return;

            for (int i = 0; i < boundA.contactsEntity.Count; i++)
            {
                GameObject? contactObject = ObjectManager.GetGameObject(boundA.contactsEntity[i].Id);
                ref InteractionComponent interComB = ref contactObject.entity.Get<InteractionComponent>();
                if(interComB.type == EEntityType.Player && interComB.canAction)
                {
                    interComB.canAction = false;
                    interComB.lastDisableTimer = 0;
                    string npcID = interactionA.actionID;
                    string playerNetID = interComB.actionID;
                    /// K키를 눌러서 물체와 상호작용은 NPC말고는 없다.
                    /// 플레이어의 Client ID는 string인데 NPC의 UID는 int다;;
                    DtoNPC? npc = NPCManager.GetNPCData(npcID);
                    if (npc == null)
                    {
                        continue;
                    }
                    NPCManager.Interaction(playerNetID, npc.Value.npcUID);
                }
            }
            /// 이거 콘택트 엔티티 초기화 시켜줘야 할텐데 TransformPacket SenderSystem에서 해야할까?
            boundA.contactsEntity = new List<Entity>();
        });
    }
}