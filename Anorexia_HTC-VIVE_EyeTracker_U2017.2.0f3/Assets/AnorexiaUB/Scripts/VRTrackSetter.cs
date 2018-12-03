using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using System.Linq;

public class VRTrackSetter : MonoBehaviour {

    public SteamVR_ControllerManager controllerManager;
    private VRIK vrik;
    public float overlapRadius;

    public List<GameObject> bodyTransforms;
    public List<GameObject> htcSensors;

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
        if (vrik == null)
        {
            Debug.Log("reference is not seted");
            vrik = GetComponent<VRIK>();
        }
        if (htcSensors[(int)BODYPARTS.HEAD].activeInHierarchy) vrik.solver.spine.headTarget = bodyTransforms[(int)BODYPARTS.HEAD].transform;
        //bodyDummies[(int)BODYPARTS.HEAD].bodyPart = BODYPARTS.HEAD;
        if (htcSensors[(int)BODYPARTS.LEFT_ARM].activeInHierarchy) vrik.solver.leftArm.target = bodyTransforms[(int)BODYPARTS.LEFT_ARM].transform;
        // bodyDummies[(int)BODYPARTS.LEFT_ARM].bodyPart = BODYPARTS.LEFT_ARM;
        if (htcSensors[(int)BODYPARTS.RIGHT_ARM].activeInHierarchy) vrik.solver.rightArm.target = bodyTransforms[(int)BODYPARTS.RIGHT_ARM].transform;
        // bodyDummies[(int)BODYPARTS.RIGHT_ARM].bodyPart = BODYPARTS.RIGHT_ARM;
        if (htcSensors[(int)BODYPARTS.HIP].activeInHierarchy) vrik.solver.spine.pelvisTarget = bodyTransforms[(int)BODYPARTS.HIP].transform;
        //bodyDummies[(int)BODYPARTS.HIP].bodyPart = BODYPARTS.HIP;
        if (htcSensors[(int)BODYPARTS.LEFT_LEG].activeInHierarchy) vrik.solver.leftLeg.target = bodyTransforms[(int)BODYPARTS.LEFT_LEG].transform;
        //bodyDummies[(int)BODYPARTS.LEFT_LEG].bodyPart = BODYPARTS.LEFT_LEG;
        if (htcSensors[(int)BODYPARTS.RIGHT_LEG].activeInHierarchy) vrik.solver.rightLeg.target = bodyTransforms[(int)BODYPARTS.RIGHT_LEG].transform;

        if(bodyTransforms[(int)BODYPARTS.RIGHT_LEG].transform.position.x < bodyTransforms[(int)BODYPARTS.LEFT_LEG].transform.position.x && 
            htcSensors[(int)BODYPARTS.LEFT_LEG].activeInHierarchy &&
            htcSensors[(int)BODYPARTS.RIGHT_LEG].activeInHierarchy)
        {
            SwipeTargetControllers(BODYPARTS.RIGHT_LEG, BODYPARTS.LEFT_LEG);
        }

        vrik.enabled = true;

    }

    void SwipeTargetControllers(BODYPARTS firstIdPart, BODYPARTS secondIdPart)
    {
        Debug.Log("hi, we are your legs. Now we still swiped! please change our transform locations");
        Vector3 rightPos = bodyTransforms[(int)firstIdPart].transform.position;
        Vector3 leftPos = bodyTransforms[(int)secondIdPart].transform.position;
        Quaternion rightRotation = bodyTransforms[(int)firstIdPart].transform.rotation;
        Quaternion leftRotation = bodyTransforms[(int)secondIdPart].transform.rotation;

        //bodyTransforms[(int)firstIdPart].transform.rotation = Quaternion.identity;
        //bodyTransforms[(int)secondIdPart].transform.rotation = Quaternion.identity;
        bodyTransforms[(int)firstIdPart].transform.rotation = leftRotation;
        bodyTransforms[(int)secondIdPart].transform.rotation = rightRotation;

        bodyTransforms[(int)firstIdPart].transform.SetParent(htcSensors[(int)secondIdPart].transform);
        bodyTransforms[(int)secondIdPart].transform.SetParent(htcSensors[(int)firstIdPart].transform);

        bodyTransforms[(int)firstIdPart].transform.position = leftPos;
        bodyTransforms[(int)secondIdPart].transform.position = rightPos;

        


    }
    //public void DestroyTrackSetters(){
    //    for (int i = 0; i < bodyParts.Count; i++){
    //        Destroy(bodyParts[i].GetComponent<BodyTrackSetter>());
    //    }
    //    bodyParts = null;
    //}

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
