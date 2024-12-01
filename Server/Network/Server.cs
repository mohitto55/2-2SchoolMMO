using System.Net.Sockets;
using System.Net;

public class IOCPServer
{
    public static Queue<PacketHandler> m_packetHandlerQueue = new Queue<PacketHandler>();

    // id : Client
    static Dictionary<string, Client> m_clients = new Dictionary<string, Client>();

    TcpListener m_tcpListener;

    int m_port;

    public IOCPServer(int port)
    {
        m_port = port;
        m_tcpListener = new TcpListener(IPAddress.Any, port);

    }
    public void Init()
    {
        try
        {
            m_tcpListener.Start();
            //Console.WriteLine($"Running server on {IPAddress.Any}:{m_port}");
            m_tcpListener.BeginAcceptTcpClient(ServerConnectCallBack, null);
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
        }
    }
    private void ServerConnectCallBack(IAsyncResult result)
    {
        var tcpClient = m_tcpListener.EndAcceptTcpClient(result);

        m_tcpListener.BeginAcceptTcpClient(ServerConnectCallBack, null);

        // 임시 id를 반환

        int i = 0;
        while (true)
        {
            i++;
            string tempID = $"Temp:{i}";
            if (m_clients.ContainsKey(tempID)) continue;


            var client = new Client(tempID, tcpClient);
            Console.WriteLine($"Connect client on {tcpClient.Client.LocalEndPoint}");
            m_clients.Add(tempID, client);

            return;
        }
    }

    public bool Run()
    {
        while (m_packetHandlerQueue.Count > 0)
        {
            var handler = m_packetHandlerQueue.Dequeue();
            Task task = new Task(handler.OnProcess);
            task.Start();
        }
        return true;
    }
    public void Shutdown()
    {

    }
    public static Client GetClient(string id)
    {
        return m_clients[id];
    }
    public static List<string> GetConnectClientNames()
    {
        var clientNames = new List<string>();
        foreach (var item in m_clients)
        {
            clientNames.Add(item.Key);
        }
        return clientNames;
    }
    public static void RegisterClient(Client client)
    {
        m_clients.Add(client.m_id, client);
    }
    public static void RemoveClient(string id)
    {
        m_clients.Remove(id);
    }
    public static void SendClient(string id, PacketHandler handler)
    {
        m_clients[id].SendPacket(handler);
    }
    public static void SendAllClient(PacketHandler handler)
    {
        foreach (var item in m_clients)
        {
            item.Value.SendPacket(handler);
        }
    }
}