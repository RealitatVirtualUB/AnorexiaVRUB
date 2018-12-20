using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformColliderController : MonoBehaviour {

    public List<CapsuleCollider> baseCollider;
    //deprecated
    public List<ColliderData> colliderData;

    public List<Mesh> thinColliders;
    public List<Mesh> midColliders;
    public List<Mesh> fatColliders;
    public List<MeshCollider> currentColliders;

    List<Vector3> midVertices = new List<Vector3>();
    List<Vector3> fatVertices = new List<Vector3>();

    private float currentInterpolationValue;
    // Use this for initialization
    void Awake () {

        for (int i = 0; i < midColliders.Count; i++)
        {
            List<Vector3> newMidVerticesData = new List<Vector3>();
            List<Vector3> newFatVerticesData = new List<Vector3>();
            //load vertices of the mid model
            midColliders[i].GetVertices(newMidVerticesData);
            //load the vertices of the fat model 
            fatColliders[i].GetVertices(newFatVerticesData);

            for (int j= 0;j < newMidVerticesData.Count ;j++)
            {
                //Debug.Log("index of the loop " + i + " vertex number " + j);
                midVertices.Add(newMidVerticesData[j]);
                fatVertices.Add(newFatVerticesData[j]);
            }
        }
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

    public void SaveInterpolationValue(float interpolation)
    {
        currentInterpolationValue = interpolation;
        SetMeshColliderSize(0);
    }

    public void SetMeshColliderSize(float interpolation)
    {
        List<Vector3> newVertices = new List<Vector3>();

        int baseIndexRow = 0;
        //this returns a mesh
        for (int i= 0;i < midColliders.Count ;i++)
        {
            Mesh newMesh = currentColliders[i].sharedMesh;
            //create the new list of vertices that you need to alocate
            //load the vertices of the mid model
            //for every vertex, interpolate between the to extrems with interpolation input
            for (int j=0;j < newMesh.vertices.Length; j++)
            {
                //Debug.Log("id part: " + i  + " vertex number of the part: " + j + " index into de array: " + (baseIndexRow + j) + " size of vertex mid data: " + midVertices.Count + " size of vertex fat data: " + fatVertices.Count);
                Vector3 vertex = Vector3.Lerp(midVertices[baseIndexRow + j], fatVertices[baseIndexRow + j], currentInterpolationValue);
                newVertices.Add(vertex);
            }
            baseIndexRow += newMesh.vertices.Length;
            //set the new vertices to the shared mesh
            newMesh.SetVertices(newVertices);
            currentColliders[i].sharedMesh= null;
            currentColliders[i].sharedMesh = newMesh;
            newVertices.Clear();
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
