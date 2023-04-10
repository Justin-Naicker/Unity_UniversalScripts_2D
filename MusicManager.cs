using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public GameObject musicObject;
    public static MusicManager music;
    public AudioClip backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        PlayMusic("music");
    }

    public void PlayMusic(string nameMusic)
    {
        switch (nameMusic)
        {
            case "music":
                MusicObject(backgroundMusic);
                break;
            default:
                break;
        }
    }

    public void MusicObject(AudioClip clip)
    {
        GameObject newObject = Instantiate(musicObject, transform);
        newObject.GetComponent<AudioSource>().clip = clip;
        newObject.GetComponent<AudioSource>().loop = true;
        newObject.GetComponent<AudioSource>().Play();
    }
}
