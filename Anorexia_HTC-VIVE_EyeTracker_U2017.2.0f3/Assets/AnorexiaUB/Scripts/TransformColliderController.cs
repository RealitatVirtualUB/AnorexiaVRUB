using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformColliderController : MonoBehaviour {

    public List<CapsuleCollider> baseCollider;
    //public List<CapsuleCollider> maxSizeCollider;
    public List<ColliderData> colliderData;

    // Use this for initialization
    void Awake () {
       //if(baseCollider.Count == maxSizeCollider.Count)
       // {
       //     for(int i= 0; i < baseCollider.Count; i++)
       //     {
       //         ColliderData newData;
       //         newData.baseCenter = baseCollider[i].center;
       //         newData.maxCenter = maxSizeCollider[i].center;
       //         newData.baseRadius = baseCollider[i].radius;
       //         newData.maxRadius = maxSizeCollider[i].radius;
       //         newData.baseHeight = baseCollider[i].height;
       //         newData.maxHeight = maxSizeCollider[i].height;
       //         colliderData.Add(newData);
       //     }
       // }
       // else
       // {
       //     Debug.Log("ERROR: program need all colliders setted");
       // }
       ////clean reference Colliders
       // foreach (CapsuleCollider collider in maxSizeCollider) Destroy(collider);
       // maxSizeCollider.Clear();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetColliderSize(float interpolation){
        for(int i=0;i < colliderData.Count ;i++)
        {
            Vector3 tempCenter = Vector3.zero;
            tempCenter.x = Mathf.Lerp(colliderData[i].baseCenter.x, colliderData[i].maxCenter.x, interpolation);
            tempCenter.y = Mathf.Lerp(colliderData[i].baseCenter.y, colliderData[i].maxCenter.y, interpolation);
            tempCenter.z = Mathf.Lerp(colliderData[i].baseCenter.z, colliderData[i].maxCenter.z, interpolation);
            baseCollider[i].center = tempCenter;
            baseCollider[i].radius = Mathf.Lerp(colliderData[i].baseRadius, colliderData[i].maxRadius, interpolation);
            baseCollider[i].height = Mathf.Lerp(colliderData[i].baseHeight, colliderData[i].maxHeight, interpolation);
        }
    }

    [System.Serializable]
    public struct ColliderData
    {
        public Vector3 baseCenter;
        public Vector3 maxCenter;
        public float baseRadius;
        public float maxRadius;
        public float baseHeight;
        public float maxHeight; 
    }

}
