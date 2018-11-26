using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using System.Linq;

public class VRTrackSetter : MonoBehaviour {

    public SteamVR_ControllerManager controllerManager;
    private VRIK vrik;
    public List<GameObject> orderedTracks;
    public float overlapRadius;
    public List<BodyTrackSetter> bodyParts;
    public List<BodyPartId> bodyDummies;
    /*
    public BodyTrackSetter head;
    public BodyTrackSetter leftArm;
    public BodyTrackSetter rightArm;
    public BodyTrackSetter hip;
    public BodyTrackSetter leftFoot;
    public BodyTrackSetter rightFoot;
    */
    public Color missingTrackerColor;
    public Color correctTrackerColor;
    public Color conflictTrackerColor;
    

    void Awake(){
        //controllerManager = GetComponent<SteamVR_ControllerManager>();
        vrik = GetComponent<VRIK>();
    }

    

    void Start () {
        
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetTracksAndBody()
    {
        return;
        ///
        //
        //Right Arm - Left Arm - Therapist Arm
        //Hip
        //Right Leg - Left Leg
        /// 

        //GameObject[] tracks = controllerManager.objects;
        /*for (int i = 0; i < controllerManager.objects.Length-1; i++){
            GameObject highest = controllerManager.objects[i];
            for (int j = i+1; j < controllerManager.objects.Length; j++)
            {
                if(controllerManager.objects[i].transform.position.y > controllerManager.objects[j].transform.position.y){
                    highest = controllerManager.objects[i];
                }
            }
            orderedTracks.Add(highest);
        }*/
        //ArrayList.Sort((emp1, emp2) => emp1.FirstName.CompareTo(emp2.FirstName))
        GameObject[] objs = controllerManager.objects;
        var obs = objs.OrderBy(o => o.transform.position.y).ToList<GameObject>();
        for (int i = 0; i < obs.Count; i++)
        {
            Debug.Log(obs[i].name);
        }
        /*
        if (orderedTracks[(int)BODYPARTS.RIGHT_ARM].transform.position.x < orderedTracks[(int)BODYPARTS.LEFT_ARM].transform.position.x){
            GameObject aux = orderedTracks[(int)BODYPARTS.RIGHT_ARM];
            orderedTracks[(int)BODYPARTS.RIGHT_ARM] = orderedTracks[(int)BODYPARTS.LEFT_ARM];
            orderedTracks[(int)BODYPARTS.LEFT_ARM] = aux;

        }
        if (orderedTracks[(int)BODYPARTS.RIGHT_LEG].transform.position.x < orderedTracks[(int)BODYPARTS.LEFT_LEG].transform.position.x)
        {
            GameObject aux = orderedTracks[(int)BODYPARTS.RIGHT_LEG];
            orderedTracks[(int)BODYPARTS.RIGHT_LEG] = orderedTracks[(int)BODYPARTS.LEFT_LEG];
            orderedTracks[(int)BODYPARTS.LEFT_LEG] = aux;

        }*/
        //List<GameObject>
        //vrik.solver.leftArm.target
    }

    public void SetTracksOnAvatar(){
        /*vrik.solver.spine.headTarget = head.choosenTracker.transform;
        vrik.solver.rightArm.target = rightArm.choosenTracker.transform;
        vrik.solver.leftArm.target = leftArm.choosenTracker.transform;
        vrik.solver.spine.pelvisTarget = hip.choosenTracker.transform;
        vrik.solver.rightLeg.target = rightFoot.choosenTracker.transform;
        vrik.solver.leftLeg.target = leftFoot.choosenTracker.transform;*/
        int bodyPartsReadyCount = 0;
        for (int i = 0; i < bodyParts.Count; i++){
            if (bodyParts[i].trackReady) bodyPartsReadyCount++;
        }
        if(bodyPartsReadyCount == bodyParts.Count){
            //poner dummies correctos
            /*for (int i = 0; i < bodyParts.Count; i++){
                //for (int j = 0; j < bodyDummies.Count; j++){
                if (bodyParts[i].choosenDummie.bodyPart != bodyParts[i].bodyPart)
                {
                    //Transform Parent = bodyParts[i].choosenDummie.transform.parent;
                    //bodyParts[i].choosenDummie.transform.parent = null;
                    //bodyParts[i].choosenDummie.transform.parent = null;
                    for (int j = 0; j < bodyDummies.Count; j++)
                    {
                        if(bodyParts[i].bodyPart == bodyDummies[j].bodyPart)
                        {
                            Vector3 localPos = bodyDummies[j].transform.localPosition;
                            bodyDummies[j].transform.parent = bodyParts[i].choosenDummie.transform.parent;
                            bodyDummies[j].transform.localPosition = localPos;
                            bodyParts[i].choosenDummie = bodyDummies[j];


                        }
                    }
                }

                //}
            
            }*/



            vrik.solver.spine.headTarget = bodyParts[(int)BODYPARTS.HEAD].choosenDummie.transform;
            //bodyDummies[(int)BODYPARTS.HEAD].bodyPart = BODYPARTS.HEAD;
            vrik.solver.leftArm.target = bodyParts[(int)BODYPARTS.LEFT_ARM].choosenDummie.transform;
           // bodyDummies[(int)BODYPARTS.LEFT_ARM].bodyPart = BODYPARTS.LEFT_ARM;
            vrik.solver.rightArm.target = bodyParts[(int)BODYPARTS.RIGHT_ARM].choosenDummie.transform;
           // bodyDummies[(int)BODYPARTS.RIGHT_ARM].bodyPart = BODYPARTS.RIGHT_ARM;
            vrik.solver.spine.pelvisTarget = bodyParts[(int)BODYPARTS.HIP].choosenDummie.transform;
            //bodyDummies[(int)BODYPARTS.HIP].bodyPart = BODYPARTS.HIP;
            vrik.solver.leftLeg.target = bodyParts[(int)BODYPARTS.LEFT_LEG].choosenDummie.transform;
            //bodyDummies[(int)BODYPARTS.LEFT_LEG].bodyPart = BODYPARTS.LEFT_LEG;
            vrik.solver.rightLeg.target = bodyParts[(int)BODYPARTS.RIGHT_LEG].choosenDummie.transform;
            //bodyDummies[(int)BODYPARTS.RIGHT_LEG].bodyPart = BODYPARTS.RIGHT_LEG;
            Debug.Log("TRACKERS PAIRED");


            vrik.enabled = true;
            DestroyTrackSetters();
        }
        else
        {
            Debug.Log("RETRY PAIR THE TRACKERS");
        }
  }

    public void DestroyTrackSetters(){
        for (int i = 0; i < bodyParts.Count; i++){
            Destroy(bodyParts[i].GetComponent<BodyTrackSetter>());
        }
        bodyParts = null;
    }

    void OnDrawGizmos() { 
    /*  Gizmos.color = missingTrackerColor;
      Gizmos.DrawSphere(head.position, overlapRadius);
      Gizmos.DrawSphere(leftArm.position, overlapRadius);
      Gizmos.DrawSphere(rightArm.position, overlapRadius);
      Gizmos.DrawSphere(hip.position, overlapRadius);
      Gizmos.DrawSphere(leftFoot.position, overlapRadius);
      Gizmos.DrawSphere(rightFoot.position, overlapRadius);*/
        }

}
public enum BODYPARTS
{
    HEAD,
    LEFT_ARM,
    RIGHT_ARM,
    //AUX_ARM,
    HIP,
    LEFT_LEG,
    RIGHT_LEG
}
