using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshBaker : MonoBehaviour {

    private SkinnedMeshRenderer skinnedMeshRenderer;
    private MeshCollider meshCollider;
    public float tUpdateMesh;
    private float _ctUpdateMesh;
    private bool updateMesh;

    void Awake(){
        skinnedMeshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
        meshCollider = gameObject.GetComponent<MeshCollider>();
        meshCollider.enabled = false;
        _ctUpdateMesh = tUpdateMesh;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckBaked();
        
	}

    void CheckBaked(){
        if (!updateMesh) return;
        _ctUpdateMesh -= Time.deltaTime;
        if(_ctUpdateMesh <= 0){
            BakeMesh();
            _ctUpdateMesh = tUpdateMesh;
        }
    }

    void BakeMesh(){
        Mesh bakedMesh = new Mesh();
        skinnedMeshRenderer.BakeMesh(bakedMesh);
        meshCollider.sharedMesh = bakedMesh;
        meshCollider.transform.localScale = transform.localScale;
    }

    public void EnableMesh(){
        updateMesh = true;
        meshCollider.enabled = true;
    }

    public void DisableMesh(){
        updateMesh = false;
        meshCollider.enabled = false;
    }
}
