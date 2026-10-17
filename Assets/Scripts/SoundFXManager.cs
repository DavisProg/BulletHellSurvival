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
public void playSoundEffect(AudioClip[] audioClip, Transform spawnTransform, float volume)
{
    AudioClip soundEffect = audioClip[Random.Range(0, audioClip.Length)];
    AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
    audioSource.clip = soundEffect;
    audioSource.volume = volume;
    audioSource.Play();
    float clipLength = audioSource.clip.length;
    Destroy(audioSource.gameObject, clipLength);
}

}
