/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class IG2EnemyController : AgentController
{
    public Transform projectile;
    private float PcentLife = 100;
    private FSMachine<IG2SBase, IG2StateInfo> FSM = new FSMachine<IG2SBase, IG2StateInfo>();
    IG2StateInfo FSMInfos = new IG2StateInfo();

    void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        GetComponent<AISenseSight>().AddSenseHandler(new AISense<SightStimulus>.SenseEventHandler(HandleSight));
        GetComponent<AISenseSight>().AddObjectToTrack(player);
        GetComponent<AISenseHearing>().AddSenseHandler(new AISense<HearingStimulus>.SenseEventHandler(HandleHearing));
        GetComponent<AISenseHearing>().AddObjectToTrack(player);
    }

    void HandleSight(SightStimulus sti, AISense<SightStimulus>.Status evt)
    {
        FSMInfos.CanSeeTarget = evt == AISense<SightStimulus>.Status.Enter || evt == AISense<SightStimulus>.Status.Stay;

        if (evt == AISense<SightStimulus>.Status.Enter)
            Debug.Log("Objet " + evt + " vue en " + sti.position);

        if (evt != AISense<SightStimulus>.Status.Leave)
        {
            FSMInfos.LastPlayerPosition = sti.position;
            FSMInfos.LastPlayerVelocity = sti.velocity;
            FSMInfos.LastPlayerPerceivedTime = Time.time;
        }

    }

    void HandleHearing(HearingStimulus sti, AISense<HearingStimulus>.Status evt)
    {
        FSMInfos.CanHearTarget = evt == AISense<HearingStimulus>.Status.Enter || evt == AISense<HearingStimulus>.Status.Stay;

        if (evt == AISense<HearingStimulus>.Status.Enter)
            Debug.Log("Objet " + evt + " ou�e en " + sti.position);

        if (evt != AISense<HearingStimulus>.Status.Leave)
        {
            FSMInfos.LastPlayerPosition = sti.position;
            FSMInfos.LastPlayerPerceivedTime = Time.time;
        }
    }

    void Update()
    {
        FSM.ShowDebug = this.ShowDebug;
        FSMInfos.Controller = this;
        FSMInfos.PcentLife = PcentLife;
        FSM.Update(FSMInfos);
    }

    //Jet tr�s approximatif ne mache que si meme hauteur (bonus par rapport � cette s�ance)
    // voir https://fr.wikipedia.org/wiki/Port%C3%A9e_(balistique)
    public void getParamsShoot(Vector3 targetPosition, Vector3 shootPosition, out Vector3 velocityProjectile)
    {
        Vector3 dirTarget = targetPosition - shootPosition;
        float distTarget = dirTarget.magnitude;
        dirTarget = dirTarget / distTarget; //Pour �viter un sqrt de plus

        //Tir � 45 deg
        float speedProj = Mathf.Sqrt((distTarget - 1) * Physics.gravity.magnitude);
        Vector3 dirShoot = Quaternion.AngleAxis(-45, Vector3.Cross(Vector3.up, dirTarget)) * dirTarget;

        velocityProjectile = dirShoot * speedProj;
    }

    public void Attack(Vector3 targetPosition, Vector3 targetVelocity)
    {
        //Regarde l'ennemi mais reste droit
        Vector3 lookPos = targetPosition;
        lookPos.y = transform.position.y;
        transform.LookAt(targetPosition);

        //Cr�ation du projectile
        Transform proj = GameObject.Instantiate<Transform>(projectile);
        proj.position = transform.position + Vector3.up + transform.forward; //on le lance de devant nous

        //Si on tire sans mouvement de la cible, quels sont les param�tres
        Vector3 velocityProjectile;
        getParamsShoot(targetPosition, proj.position, out velocityProjectile);

        //Si mouvement de la cible on en tient compte
        if (targetVelocity.sqrMagnitude > 0.1f)
        {
            //Duree du trajet
            float distance = (targetPosition - proj.position).magnitude;
            float dureeTrajet = distance / new Vector3(velocityProjectile.x, 0, velocityProjectile.z).magnitude;

            //Decalage estim� du perso
            Vector3 positionApresMouvement = targetPosition + targetVelocity * dureeTrajet;

            getParamsShoot(positionApresMouvement, proj.position, out velocityProjectile);
        }


        proj.GetComponent<Rigidbody>().velocity = velocityProjectile * 1.1f;
        proj.GetComponent<Rigidbody>().angularVelocity = Random.onUnitSphere * 3.0f; //On fait tourner le projectile en l'air, pour le fun
    }
}
*/