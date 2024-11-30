using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DialogueReact : MonoBehaviour, INotificationReceiver
{
    PlayableDirector m_director;
    DialogueRunner m_runner;
    private void Awake()
    {
        m_director = GetComponent<PlayableDirector>();
        m_runner = GetComponent<DialogueRunner>();
    }
    public void OnNotify(Playable origin, INotification notification, object context)
    {

        if (!(notification is DialogueMarker)) return;

        m_director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        m_runner.Play();


        //if (!(notification is ForceDialoguePlayMarker)) return;
        //m_runner.Play();

    }
    [ContextMenu("Play")]
    public void Play()
    {
        m_director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }
}