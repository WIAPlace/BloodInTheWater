using UnityEngine;

[CreateAssetMenu]
public class DialogueAsset : ScriptableObject
{
    public string[] speaker;
    [TextArea]
    public string[] dialogue;
    public AudioClip[] audioclip;
}
