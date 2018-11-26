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
    public List<GameObject> bodyTransforms;
    public List<GameObject> htcSensors;

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

        vrik.enabled = true;
        //int bodyPartsReadyCount = 0;
        //for (int i = 0; i < bodyParts.Count; i++){
        //    if (bodyParts[i].trackReady) bodyPartsReadyCount++;
        //}

        //if(bodyPartsReadyCount == bodyParts.Count){
        //    //poner dummies correctos
        //    if(htcSensors[(int)BODYPARTS.HEAD] != null) vrik.solver.spine.headTarget = bodyTransforms[(int)BODYPARTS.HEAD];
        //    //bodyDummies[(int)BODYPARTS.HEAD].bodyPart = BODYPARTS.HEAD;
        //    if (htcSensors[(int)BODYPARTS.LEFT_ARM] != null) vrik.solver.leftArm.target = bodyTransforms[(int)BODYPARTS.LEFT_ARM];
        //    // bodyDummies[(int)BODYPARTS.LEFT_ARM].bodyPart = BODYPARTS.LEFT_ARM;
        //    if (htcSensors[(int)BODYPARTS.RIGHT_ARM] != null) vrik.solver.rightArm.target = bodyTransforms[(int)BODYPARTS.RIGHT_ARM];
        //    // bodyDummies[(int)BODYPARTS.RIGHT_ARM].bodyPart = BODYPARTS.RIGHT_ARM;
        //    if (htcSensors[(int)BODYPARTS.HIP] != null) vrik.solver.spine.pelvisTarget = bodyTransforms[(int)BODYPARTS.HIP];
        //    //bodyDummies[(int)BODYPARTS.HIP].bodyPart = BODYPARTS.HIP;
        //    if (htcSensors[(int)BODYPARTS.LEFT_LEG] != null) vrik.solver.leftLeg.target = bodyTransforms[(int)BODYPARTS.LEFT_LEG];
        //    //bodyDummies[(int)BODYPARTS.LEFT_LEG].bodyPart = BODYPARTS.LEFT_LEG;
        //    if (htcSensors[(int)BODYPARTS.RIGHT_LEG] != null) vrik.solver.rightLeg.target = bodyTransforms[(int)BODYPARTS.RIGHT_LEG];
        //    //bodyDummies[(int)BODYPARTS.RIGHT_LEG].bodyPart = BODYPARTS.RIGHT_LEG;

        //    Debug.Log("TRACKERS PAIRED " + bodyPartsReadyCount);


        //    vrik.enabled = true;
        //    DestroyTrackSetters();
        //}
        //else
        //{
        //    Debug.Log("RETRY PAIR THE TRACKERS");
        //}
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
