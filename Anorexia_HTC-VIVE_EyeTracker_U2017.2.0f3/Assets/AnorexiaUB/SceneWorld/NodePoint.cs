using UnityEngine;

public class NodePoint : MonoBehaviour {
    public Nodes nodes;
    public NodePoint next;

    void OnDrawGizmos()
    {
        if (this.next)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(this.transform.position, this.next.transform.position);
        }
    }

    void Update()
    {
        if (this.next) {
            Vector3 direction = this.next.transform.position - this.transform.position;
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position, direction.normalized, out hit, direction.magnitude))
            {
                MimicPlane mimicPlane = hit.collider.GetComponent<MimicPlane>();
                if (mimicPlane)
                {
                    this.nodes.mimicController.model.position = hit.point;
                }
            }
        }
    }
}
