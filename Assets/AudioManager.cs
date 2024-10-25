using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();
    public AudioSource audioSource;
    private int currentClipIndex = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Kiểm tra xem có âm thanh nào được đặt trong danh sách không
        if (audioClips.Count > 0)
        {
            // Phát âm thanh đầu tiên trong danh sách
            PlayAudioClip();
        }
    }

    private void Update()
    {
        if (audioSource.time == audioSource.clip.length)
        {
            if (currentClipIndex >= audioClips.Count)
            {
                currentClipIndex = -1;
            }
            currentClipIndex++;
            PlayAudioClip();
        }
    }

    // Phát âm thanh tiếp theo trong danh sách
    void PlayAudioClip()
    {
        if (audioClips.Count > 0)
        {
            audioSource.clip = audioClips[currentClipIndex];
            audioSource.Play();
            //currentClipIndex = (currentClipIndex + 1) % audioClips.Count; // Lặp lại danh sách
        }
    }
}
