using System.Collections.Generic;
using UnityEngine;

public class DialoguePrinterHelper : MonoBehaviour
{
    public UIDialogBox m_dialogBox;

    public Transform m_offset;
    public UIDialogBox m_dialogBoxPrefabs;

    [TextArea]
    public List<string> message = new List<string>();
    public int idx = 0;

    public int endDelay = 1;

    public void SetEndDelay(int delay)
    {
        endDelay = delay;
    }
    public void SayDust()
    {
        //DustController.Instance.Say(message[idx]);
        idx++;
    }
    public void Play()
    {
        m_dialogBox = Instantiate(m_dialogBoxPrefabs);
        m_dialogBox.Setting(message[idx],
        transform.position + Vector3.up,
        0.1f,
        10);
        m_dialogBox.PlayDialog();

        m_dialogBox.transform.position = m_offset.position + Vector3.up;

        m_dialogBox.endCallback = () =>
        {
            idx++;
            Destroy(m_dialogBox.gameObject, endDelay);
        };
    }
}