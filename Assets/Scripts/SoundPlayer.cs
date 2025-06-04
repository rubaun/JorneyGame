using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySoundBackground(AudioClip clip)
    {
        if (clip != null && player != null)
        {
            player.clip = clip;
            player.Play();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null && player != null)
        {
            player.PlayOneShot(clip);
        }
    }
}
