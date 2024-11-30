using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogBox : MonoBehaviour
{
    public enum State
    {
        Play,
        End,
    }

    private Transform m_target;
    [SerializeField]
    TMP_Text m_textMesh;
    [SerializeField]
    TMPTextPrinter m_printer;

    [SerializeField]
    [TextArea]
    string m_message;

    [SerializeField]
    VerticalLayoutGroup group;

    // 글자 등장 속도

    [SerializeField]
    float m_floatingSpeed;

    public State m_state;

    Coroutine m_dialogRoutine;

    public void Awake()
    {
        group = GetComponent<VerticalLayoutGroup>();
        transform.SetParent(GameObject.FindGameObjectWithTag("DialogueUI").transform);
    }

   
    public void Show()
    {

    }
    public void Hide()
    {

    }
    public void Setting(string msg, Vector3 pos, float speed, float fontSize)
    {
        m_printer.InitText(msg);
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
        transform.position = pos;
        transform.localScale = Vector3.one;
    }
    [ContextMenu("Play")]
    public void PlayDialog()
    {

        m_state = State.Play;
        m_dialogRoutine = StartCoroutine(PlayDialogRoutine());
        
    }
    public Action endCallback;
    IEnumerator PlayDialogRoutine()
    {
        yield return m_printer.PrintDialogue();
        Debug.Log("end dialog");
        m_state = State.End;
        endCallback?.Invoke();
    }
    [ContextMenu("Skip")]
    public void SkipDialog()
    {
        m_state = State.End;
        m_printer.SkipDialogue();
    }

}