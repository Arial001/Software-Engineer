using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioClip backgroundMusic; // Musik latar
    public float volume = 1.0f; // Kekencangan suara (0.0 - 1.0)
    public float startTime = 0f; // Waktu mulai pemutaran dalam detik
    public float duration = 10f; // Durasi pemutaran dalam detik
    public float playbackSpeed = 10f; // Kecepatan pemutaran (misalnya, 10x)

    private AudioSource audioSource;

    void Start()
    {
        
    }

    private IEnumerator LoopMusic()
    {
        while (true)
        {
            yield return new WaitForSeconds(duration / playbackSpeed); // Tunggu sesuai durasi dengan kecepatan
            audioSource.time = startTime; // Kembali ke waktu mulai
            audioSource.Play(); // Memulai kembali pemutaran
        }
    }

    // Fungsi untuk menghentikan musik
    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            Debug.Log("Musik dihentikan.");
        }
    }

    // Fungsi untuk memutar musik dari awal
    public void PlayMusic()
    {
        // Mendapatkan komponen AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.volume = volume;
        audioSource.pitch = playbackSpeed; // Mengatur pitch untuk percepatan

        // Mengatur waktu mulai pemutaran
        audioSource.time = startTime;

        // Memulai pemutaran musik
        audioSource.Play();

        // Memanggil coroutine untuk mengatur looping
        //StartCoroutine(LoopMusic());
        Debug.Log("Musik diputar.");
    }
}
