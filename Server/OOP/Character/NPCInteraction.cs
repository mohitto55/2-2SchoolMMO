[AttributeUsage(AttributeTargets.Class)]
public class NPCInteractionTypeAttribure : Attribute
{
    public EInteractionType type;
    public NPCInteractionTypeAttribure(EInteractionType type)
    {
        this.type = type;
    }
}

public abstract class NPCInteraction
{
    public abstract void Interaction(string playerID, string npcUID);
}