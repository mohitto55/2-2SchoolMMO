using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class ForceDialoguePlayMarker : Marker, INotification
{
    public PropertyName id { get; }

}