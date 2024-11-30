using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DialogueMarker : Marker, INotification
{
    public PropertyName id { get; }

}