[System.Serializable]
public abstract class MapSerilizer : ISerializer<string>
{
    public abstract void Serialize(string mapName);
}