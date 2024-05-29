using UnityEngine;

public class SoundManager : MonoBehaviour
{


    public static SoundManager instance { get; private set; }
    private AudioSource soundsource;
    private AudioSource musicSource;

    private void Awake()
    {
        instance = this;
        soundsource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();
      
        // If there is no instance of SoundManager, set this as the instance and don't destroy it on load
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // If there is already an instance of SoundManager, destroy this one
        else if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        ChangeMusicVolume(0);
        ChangeSoundVolume(0);
    }

    public void PlaySound(AudioClip _sound)
    {
        soundsource.PlayOneShot(_sound);
    }

    public void ChangeSoundVolume(float _change)
    {
        ChnageSourceVolume(1, "soundVolume", _change, soundsource);
        
    }
    public void ChangeMusicVolume(float _change)
    {
        ChnageSourceVolume(.4f, "musicVolume", _change, musicSource);
    }

    private void ChnageSourceVolume(float baseVolume, string volumeName, float _change,AudioSource source)
    {
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1); // Get the current volume from the player prefs (default is 1 if it doesn't exist
        currentVolume += _change;

        if (currentVolume < 0)
        {
            currentVolume = 1;
        }
        else if (currentVolume > 1)
        {
            currentVolume = 0;
        }
        float finalVolume = baseVolume * currentVolume;
        source.volume = finalVolume;
        PlayerPrefs.SetFloat(volumeName, currentVolume); // Save the new volume to the player prefs
    }


}
