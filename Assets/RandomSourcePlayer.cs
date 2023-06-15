using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Explication ChatGPT: Ce script joue un son � des intervalles al�atoires en utilisant l'AudioSource attach� � l'objet. Il calcule un d�lai al�atoire entre MinSoundDelay et MinSoundDelay + RandomDelay, puis joue le son et calcule le d�lai pour le prochain son. Ce processus se r�p�te � chaque intervalle d�fini.
public class RandomSourcePlayer : MonoBehaviour
{
    public float MinSoundDelay = 1;
    public float RandomDelay = 5;
    private float NextSoundDelay = 0;
    private AudioSource aSource;

    private void computeNextSoundDelay()
    {
        NextSoundDelay = MinSoundDelay + Random.Range(0, RandomDelay);
    }

    void Start()
    {
        aSource = GetComponent<AudioSource>();
        computeNextSoundDelay();
    }

    void Update()
    {
        NextSoundDelay -= Time.deltaTime;
        if (NextSoundDelay <= 0)
        {
            aSource.Play();
            computeNextSoundDelay();
        }
    }
}