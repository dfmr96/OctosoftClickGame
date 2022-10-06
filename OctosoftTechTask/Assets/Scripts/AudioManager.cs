using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager sharedInstance;
    [Header("Audios Sources")]
    public AudioSource btnSound, objectDestroyed, objectDissapear;
    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
