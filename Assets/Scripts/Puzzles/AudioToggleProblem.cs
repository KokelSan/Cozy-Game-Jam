using System.Collections.Generic;
using UnityEngine;

public class AudioToggleProblem : Problem
{
    public AudioClip CorrectClip;
    private List<AudioClip> m_PlayedClips = new List<AudioClip>();

    public void ToggleClips(List<AudioToggleProblemElement> list)
    {
        Debug.Log("toggleclips");
        foreach (AudioToggleProblemElement item in list)
        {
            if (item.Toggle())
            {
                AudioClip clip = item.AudioSource.clip;
                if (!m_PlayedClips.Contains(clip))
                {
                    m_PlayedClips.Add(clip);
                }
            }
            else
            {
                AudioClip clip = item.AudioSource.clip;
                if (m_PlayedClips.Contains(clip))
                {
                    m_PlayedClips.Remove(clip);
                }
            }
        }

        if (m_PlayedClips.Count == 1 && m_PlayedClips[0] == CorrectClip)
        {
            SetSolveStatus(true);
        }
    }
}