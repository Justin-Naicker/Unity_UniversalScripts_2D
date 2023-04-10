using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Variables
    public AudioClip jumpSfx, landingSfx, pickupSfx, useItemSfx, openMenuSfx, hurtSfx;
    public GameObject soundObject;
    public static SoundManager instance;
    #endregion

    #region UnityEvents
    void Awake()
    {
        instance = this;
    }

    private void Update()
    {

    }
    #endregion

    #region Custom Events
    public void PlaySFX(string nameSfx)
    {
        switch (nameSfx)
        {
            case "landing":
                SoundObject(landingSfx);
                break;
            case "pickup":
                SoundObject(pickupSfx);
                break;
            case "useItem":
                SoundObject(useItemSfx);
                break;
            case "jump":
                SoundObject(jumpSfx);
                break;
            case "openMenu":
                SoundObject(openMenuSfx);
                break;
            case "hurt":
                SoundObject(hurtSfx);
                break;
            default:
                break;
        }
    }

    public void SoundObject(AudioClip clip)
    {
        GameObject newObject = Instantiate(soundObject, transform);
        newObject.GetComponent<AudioSource>().clip = clip;
        newObject.GetComponent<AudioSource>().Play();
    }
}
#endregion
