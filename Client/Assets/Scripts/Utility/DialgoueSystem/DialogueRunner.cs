using System.Collections.Generic;
using UnityEngine;

public class DialogueRunner : MonoBehaviour
{
    DialogueReact reactor;

    [SerializeField]
    Dialogue dialog;


    [SerializeField]
    List<Transform> offset;
    private void Awake()
    {
        reactor ??= GetComponent<DialogueReact>();
        dialog.m_dialogueTarget.Clear();
        dialog.m_isPlay = false;
        dialog.m_dialogueTarget.AddRange(offset);
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.W))
        //{
        //    Execute();
        //}
    }
    public void Play()
    {
        if (!dialog.PlayDialog())
        {
            Debug.Log("END");
            reactor?.Play();
        }
    }
    [ContextMenu("Execute")]
    public void Execute()
    {
        if (dialog.m_isPlay)
        {
            if (!dialog.PlayDialog())
            {
                Debug.Log("END");
                reactor?.Play();
            }
        }
    }

}