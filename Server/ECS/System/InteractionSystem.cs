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
                    /// KŰ�� ������ ��ü�� ��ȣ�ۿ��� NPC����� ����.
                    /// �÷��̾��� Client ID�� string�ε� NPC�� UID�� int��;;
                    DtoNPC? npc = NPCManager.GetNPCData(npcID);
                    if (npc == null)
                    {
                        continue;
                    }
                    NPCManager.Interaction(playerNetID, npc.Value.npcUID);
                }
            }
            /// �̰� ����Ʈ ��ƼƼ �ʱ�ȭ ������� ���ٵ� TransformPacket SenderSystem���� �ؾ��ұ�?
            boundA.contactsEntity = new List<Entity>();
        });
    }
}