using UnityEngine;

[CreateAssetMenu(fileName = "SFXConfig", menuName = "SFX Config")]
public class SFXConfigSO : ScriptableObject
{
    public AudioClip[] shootingClips;
    public AudioClip[] hitClips;
    public AudioClip[] crushClips;
}
