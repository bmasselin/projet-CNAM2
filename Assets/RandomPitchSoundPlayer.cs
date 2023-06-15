using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Explications ChatGPT : Ce script est utilis� pour contr�ler la lecture de sons associ�s � un objet avec une variation de hauteur (pitch). Le script utilise un d�lai al�atoire entre les sons, d�fini par les variables MinSoundDelay et RandomDelay. Lorsque le d�lai s'�coule, un son est jou� � l'aide de l'AudioSource attach� � l'objet. De plus, le script modifie �galement le pitch du son en utilisant la variable MaxDeltaPitch, ce qui donne une variation al�atoire de hauteur pour chaque son jou�. Apr�s avoir jou� un son, le script calcule le prochain d�lai al�atoire et continue le processus.
public class RandomPitchSoundPlayer : MonoBehaviour
{
    public float MinSoundDelay = 1;
    public float RandomDelay = 5;
    private float NextSoundDelay = 0;

    public float MaxDeltaPitch = 0.1f;

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
            //Variation du pitch entre +MaxDeltaPitch et -MaxDeltaPitch
            aSource.pitch = 1 + Random.Range(0, MaxDeltaPitch) * 2 - MaxDeltaPitch;
            computeNextSoundDelay();
        }
    }
}