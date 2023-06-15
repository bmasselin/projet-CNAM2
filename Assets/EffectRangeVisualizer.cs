using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//Explications ChatGPT: Ce script permet de visualiser une zone d'effet et d'ajuster les param�tres audio dans un mixer en fonction de la distance des objets suivis par rapport au centre de la zone. Lorsque le GameObject auquel le script est attach� est s�lectionn� dans l'�diteur, il affiche une sph�re filaire de couleur bleue autour de lui. Cette sph�re repr�sente une zone centr�e sur le GameObject qui permet � l'utilisateur de visualiser la port�e ou l'�tendue d'un effet ou d'une influence li�e � cet objet dans l'�diteur.
//

public class EffectRangeVisualizer : MonoBehaviour
{
    public Vector3 centreZone = Vector3.zero;
    public float radiusStartEffect = 10;

    public AudioMixer mixer;

    [System.Serializable]
    public class TrackedObject
    {
        public Transform transform;
        public string MixerParameter = "Undefined Tracked Param";
        public float MinVal = 0;
        public float MaxVal = 0;
    }

    public TrackedObject[] trackedObjects;

    public void Update()
    {
        //On v�rifie tous les objets track�s
        foreach (TrackedObject t in trackedObjects)
        {
            //Calcul de la distance au centre de la zone (ne pas oublier de transformer le vecteur centre du rep�re local au rep�re global)
            float distToT = Vector3.Distance(transform.TransformPoint(centreZone), t.transform.position);
            //La valeur de l'effet : Min + (Max-Min) * distanceNormalis�e
            float effectVal = t.MinVal + (t.MaxVal - t.MinVal) * (1.0f - (distToT / radiusStartEffect));
            //On ajoute le param�tre au mixer
            mixer.SetFloat(t.MixerParameter, effectVal);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.TransformPoint(centreZone), radiusStartEffect);
    }
}