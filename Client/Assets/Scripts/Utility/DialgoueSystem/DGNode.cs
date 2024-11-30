using UnityEditor;
using UnityEngine;

public class DGNode : ScriptableObject
{
    public Dialogue m_dialog;

    public enum State
    {
        Ready,
        Play,
        End,
        Complete,
        Finish,
    }

    public int m_index;
    [HideInInspector]
    public string m_description;
    [HideInInspector]
    public string m_guid;
    [HideInInspector]
    public State m_state = State.Ready;
    [HideInInspector]
    public Vector2 m_position;
    [HideInInspector]
    public DGNode m_connectedNode;

    [TextArea]
    public string m_message;

    public float m_fontSize = 10f;
    public float m_floatingSpeed = 0.1f;

    public int callStack = 0;
    public State Execute()
    {
        if (m_state == State.Complete)
        {
            Debug.Log("Complete");
            if(m_connectedNode == null)
            {
                return State.Complete;
            }
            if(m_connectedNode.Execute() == State.Complete)
            {
                m_state = State.Ready;
                return State.Complete;
            }
        }

        if(m_state == State.Ready)
        {
            Debug.Log("Play");
            if (m_dialog.m_dialogueTarget.Count > m_index)
            {
                m_dialog.m_dialogBox.Setting(m_message,
                    m_dialog.m_dialogueTarget[m_index].position,
                    m_floatingSpeed,
                    m_fontSize);
            }
            else
            {
                Debug.LogWarning("Didn't find dialog target transform");
            }
            m_dialog.m_dialogBox.PlayDialog();
            m_state = State.Play;

            return State.Play;
        }
        else if(m_dialog.m_dialogBox.m_state == UIDialogBox.State.Play &&
            m_state == State.Play)
        {
            Debug.Log("Skip");
            m_dialog.m_dialogBox.SkipDialog();
            m_state = State.End;

            return State.Play;
        }
        else if(m_dialog.m_dialogBox.m_state == UIDialogBox.State.End || 
            m_state == State.End)
        {
            Debug.Log("End");
            m_state = State.Complete;

            if (m_connectedNode == null)
            {
                return State.Complete;
            }

            //m_dialog.m_dialogBox.m_state = UIDialogBox.State.Play;
            m_connectedNode.Execute();
        }

        return State.Play;        
    }


}
