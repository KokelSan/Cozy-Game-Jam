using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    // Audio players components.
    [Min(1)] [Tooltip("Generally 8 on mobile and 32 on PC")] [SerializeField]
    public int m_MaxAudioTrack = 8;

    private List<AudioSource> m_EffectsSource = new List<AudioSource>();
    private AudioSource m_AmbienceSource;
    private AudioSource m_MusicSource;
    private int m_CurrentTrackIndex = 0;

    [SerializeField] private AudioMixerGroup m_SFXMixer;
    [SerializeField] private AudioMixerGroup m_AmbienceMixer;
    [SerializeField] private AudioMixerGroup m_MusicMixer;

    // Singleton instance.
    public static SoundManager Instance = null;

    // Initialize the singleton instance.
    private void Awake()
    {
        // If there is not already an instance of SoundManager, set it to this.
        if (Instance == null)
        {
            Instance = this;
            InitBank();
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    private void InitBank()
    {
        m_MusicSource = gameObject.AddComponent<AudioSource>();
        m_MusicSource.outputAudioMixerGroup = m_MusicMixer;
        m_AmbienceSource = gameObject.AddComponent<AudioSource>();
        m_AmbienceSource.outputAudioMixerGroup = m_AmbienceMixer;
        
        for (int i = 0; i < m_MaxAudioTrack; i++)
        {
            m_EffectsSource.Add(gameObject.AddComponent<AudioSource>());
            m_EffectsSource[i].outputAudioMixerGroup = m_SFXMixer;
        }
    }

    public void PlayLoop(AudioClip clip)
    {
        for (int i = 0; i < m_EffectsSource.Count; i++)
        {
            if (!m_EffectsSource[i].isPlaying)
            {
                m_EffectsSource[i].clip = clip;
                m_EffectsSource[i].Play();
                m_EffectsSource[i].loop = true;
                m_CurrentTrackIndex = (m_CurrentTrackIndex + 1) % m_EffectsSource.Count;
                return;
            }

            m_CurrentTrackIndex = (m_CurrentTrackIndex + 1) % m_EffectsSource.Count;
        }

        Debug.LogWarning("All track used. Add more track to SoundManager");
    }

    // Play a single clip through the sound effects source.
    public void Play(AudioClip clip)
    {
        for (int i = 0; i < m_EffectsSource.Count; i++)
        {
            if (!m_EffectsSource[i].isPlaying)
            {
                m_EffectsSource[i].clip = clip;
                m_EffectsSource[i].Play();
                m_CurrentTrackIndex = (m_CurrentTrackIndex + 1) % m_EffectsSource.Count;
                return;
            }

            m_CurrentTrackIndex = (m_CurrentTrackIndex + 1) % m_EffectsSource.Count;
        }

        Debug.LogWarning("All track used. Add more track to SoundManager");
    }

    // Play a single clip through the music source.
    public void PlayMusic(AudioClip clip)
    {
        m_MusicSource.clip = clip;
        m_MusicSource.Play();
    }

    public void PlayAmbience(AudioClip clip)
    {
        m_AmbienceSource.clip = clip;
        m_AmbienceSource.Play();
    }

    // Play a random clip from an array, and randomize the pitch slightly.
    public void RandomSoundEffect(AudioClip[] clips, float lowPitchRange = .95f, float highPitchRange = 1.05f)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        for (int i = 0; i < m_EffectsSource.Count; i++)
        {
            if (!m_EffectsSource[i].isPlaying)
            {
                m_EffectsSource[i].pitch = randomPitch;
                m_EffectsSource[i].clip = clips[randomIndex];
                m_EffectsSource[i].Play();
            }

            m_CurrentTrackIndex = (m_CurrentTrackIndex + 1) % m_EffectsSource.Count;
        }

        Debug.LogWarning("All track used. Add more track to SoundManager");
    }
}