using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public List<Transform> m_dialogueTarget = new List<Transform>();

    public List<DGNode> m_nodes = new List<DGNode>();

    public UIDialogBox m_dialogBox;

    public UIDialogBox m_dialogBoxPrefabs;

    public bool m_isPlay = false;
    public bool m_isAllPlay = false;
    void Init()
    {
        for (int i = 0; i < m_nodes.Count; i++)
        {
            m_nodes[i].m_state = DGNode.State.Ready;
        }
        
    }
     public bool PlayDialog()
     {
        if(!m_isPlay)
        {
            m_isPlay = true;
            m_isAllPlay = true;
            Init();
            m_dialogBox = Instantiate(m_dialogBoxPrefabs);
        }
        var result = m_nodes[0].Execute();
        if (result == DGNode.State.Complete)
        {
            m_isPlay = false;
            DestroyImmediate(m_dialogBox.gameObject);
            return false;

        }


        

        return true;
    }
#if UNITY_EDITOR

    public DGNode CreateNode(System.Type type)
    {
        DGNode node = ScriptableObject.CreateInstance(type) as DGNode;

        node.name = type.Name;
        node.m_dialog = this;
        node.m_guid = GUID.Generate().ToString();

        m_nodes.Add(node);

        AssetDatabase.AddObjectToAsset(node, this);
        AssetDatabase.SaveAssets();

        return node;
    }
    public void DeleteNode(DGNode node)
    {
        m_nodes.Remove(node);

        AssetDatabase.RemoveObjectFromAsset(node);
        AssetDatabase.SaveAssets();
    }
    public void AddChild(DGNode parent, DGNode child)
    {

        parent.m_connectedNode = child;

    }
    public void RemoveChild(DGNode parent, DGNode child)
    {
        parent.m_connectedNode = null;
    }
    public List<DGNode> GetChildren(DGNode parent)
    {
        var children = new List<DGNode>();

        if(parent.m_connectedNode != null)
            children.Add(parent.m_connectedNode);

        return children;
    }
#endif
}