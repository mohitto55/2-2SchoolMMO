using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Server.Debug;
using System.Reflection;
using System.Runtime.InteropServices;
using Utility.Data;


namespace Server.MySQL
{
    public static class NPCManager
    {
        private static Dictionary<string, DtoNPC> npcTable = new Dictionary<string, DtoNPC>();
        private static Dictionary<EInteractionType, NPCInteraction> interactionTable = new Dictionary<EInteractionType, NPCInteraction>();
        public static async Task InitAsync(string url, string gid)
        {
            NPCInteractionInit();

            GoogleSheetLoader loader = new GoogleSheetLoader();
            loader.Initialize();
            loader.AddGuid<DtoNPC>(gid);
            IAsyncEnumerator<DtoNPC> data = loader.Load<DtoNPC>(url);

            // 데이터 확인
            // 비동기 + yield return으로 데이터를 가져오면 순차적으로 순회한다.
            while (await data.MoveNextAsync())
            {
                var current = data.Current;
                if (current.npcUID != null)
                {
                    if (!npcTable.ContainsKey(current.npcUID))
                    {
                        npcTable.Add(current.npcUID, current);
                    }
                }
            }
        }

        private static void NPCInteractionInit()
        {
            NPCInteraction?[] npcInteractions = Assembly.GetEntryAssembly().GetTypes()
                .Where(t => t.IsSubclassOf(typeof(NPCInteraction)) && !t.IsAbstract)
                .Select(t => Activator.CreateInstance(t) as NPCInteraction).ToArray();
            foreach (var npcInteraction in npcInteractions)
            {
                NPCInteractionTypeAttribure? interactionTypeAttribure = npcInteraction.GetType().GetCustomAttribute<NPCInteractionTypeAttribure>();
                if (interactionTypeAttribure == null)
                    continue;

                EInteractionType interactionType = interactionTypeAttribure.type;
                if (!interactionTable.ContainsKey(interactionType))
                {
                    ServerDebug.Log(LogType.Warning, interactionType.ToString() + "타입 NPCInteraction 생성. Class : " + npcInteraction.GetType().ToString());
                    interactionTable.Add(interactionType, npcInteraction);
                }
                else
                {
                    ServerDebug.Log(LogType.Warning, interactionType.ToString() + "타입의 NPCInteraction은 이미 존재합니다. Class : " + npcInteraction.GetType().ToString());
                }
            }
        }

        public static DtoNPC? GetNPCData(string npcUID)
        {
            if (npcTable.ContainsKey(npcUID))
                return npcTable[npcUID];
            ServerDebug.Log(LogType.Warning, npcUID + " NPC 데이터는 없습니다.");
            return null;
        }

        public static void Interaction(string playerNetID, string npcUID)
        {
            DtoNPC? npc = GetNPCData(npcUID);
            if (npc == null)
            {
                return;
            }
            NPCInteraction interaction;
            if (!interactionTable.TryGetValue(npc.Value.interactionType, out interaction))
            {
                ServerDebug.Log(LogType.Warning, npc.Value.interactionType.ToString() + "타입의 NPC Interaction은 없습니다.");
                return;
            }
            interaction.Interaction(playerNetID, npcUID);
        }
    }
}
