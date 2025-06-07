using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);

        //Find other AudioSources in the scene and destroy them
        GameObject[] audioSources = GameObject.FindGameObjectsWithTag("Audio");
        foreach (GameObject audioSource in audioSources)
        {
            if (audioSource == this.gameObject && audioSources.Length > 1)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void PlaySoundBackground(AudioClip clip)
    {
        if (clip != null && player != null && player.isPlaying && player.clip.name != clip.name)
        {
            player.clip = clip;
            player.Play();
        }
        else if(clip != null && player != null && !player.isPlaying)
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
