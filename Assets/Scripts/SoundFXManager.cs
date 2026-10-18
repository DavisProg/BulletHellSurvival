using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
[SerializeField] AudioSource soundFXObject;
public static SoundFXManager instance;

void Awake()
{
        if (!instance)
        {
            instance = this;
        }
}
public void playSoundEffect(AudioClip[] audioClip, Transform spawnTransform, float volume, float pitch = 1f)
{
    AudioClip soundEffect = audioClip[Random.Range(0, audioClip.Length)];
    AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
    audioSource.clip = soundEffect;
    audioSource.volume = volume;
    audioSource.pitch = pitch;
    audioSource.Play();
    float clipLength = audioSource.clip.length;
    Destroy(audioSource.gameObject, clipLength);
}

}
