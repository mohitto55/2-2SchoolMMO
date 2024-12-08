using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;

public static class PacketHandlerPoolManager
{
    static bool m_isInit;

    static Dictionary<EHandleType, List<PacketHandler?>> m_packetHandlerPool =
        new Dictionary<EHandleType, List<PacketHandler?>>();

    public static void Init()
    {


        // 어셈블리안의 핸들러 타입을 전부 수색
        var assembly = Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes().Where(_ => _.IsSubclassOf(typeof(PacketHandler)));


        // 풀링시킬 객체생성 
        // - 초기화 과정 또는 풀이 부족할때만 Activate.CreateInstance 를 호출하기 떄문에
        // - 풀만 잘 조절한다면 괜찮다고 생각함
        foreach (var type in types)
        {
            if (type.IsGenericType) continue;

            string packetTypeStr = type.Name.Substring(0, type.Name.Length - 7);

            EHandleType packetType = Enum.Parse<EHandleType>(packetTypeStr);

            m_packetHandlerPool.Add(
                packetType,
                new List<PacketHandler?>());


            m_packetHandlerPool[packetType].Add((PacketHandler?)Activator.CreateInstance(type, null, EHandleType.Default));

            //Console.WriteLine(packetType);
        }


        m_isInit = true;
    }

    public static PacketHandler GetPacketHandler(EHandleType packetType)
    {
        if (!m_isInit)
            throw new Exception("[Warning] PacketHandlerPoolManager is not initilize (Use Init method)");

        if (!m_packetHandlerPool.ContainsKey(packetType))
        {
            throw new Exception("[Warning] Didn't find packetType correspond handler");
        }
        var handle = m_packetHandlerPool[packetType].Find(_ => _.usable);

        // 없다면 리플렉션을 활용해서 타입을 찾고 추가 후 반환 (성능이 느림)
        if (handle == null)
        {
            handle = (PacketHandler?)Activator.CreateInstance(
                m_packetHandlerPool[packetType][0].GetType(),
                null, packetType);

            m_packetHandlerPool[packetType].Add(handle);
        }
        return handle;
    }

}
