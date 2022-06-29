using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    static public WeatherManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }

    }

    private AudioManager theAudio;
    public ParticleSystem rain;
    public string rain_Sound;

    // Start is called before the first frame update
    void Start()
    {
        theAudio = GetComponent<AudioManager>();
        rain.Play();
    }

    public void Rain()
    {
        rain.Play();
        theAudio.Play(rain_Sound);

    }
    public void RainStop()
    {
        rain.Stop();
        theAudio.Stop(rain_Sound);
    }
    public void RainDrop()
    {
        rain.Emit(10);
    }
}
