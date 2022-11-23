using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldToLocal : MonoBehaviour
{
    [SerializeField] Transform pointInGlobal;
    [SerializeField] Transform NewOrigin;
    [SerializeField] Transform pointInLocal;
    [SerializeField] Vector2 worldTolocalPoint;
    Matrix4x4 mat1;
    Matrix4x4 mat2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        Vector3 rightLocal = NewOrigin.transform.TransformPoint(Vector3.right);
        Vector3 upLocal = NewOrigin.transform.TransformPoint(Vector3.up);
       
        Vector3 newPos = rightLocal*pointInGlobal.position.x+ upLocal*pointInGlobal.position.y;

        pointInLocal.position = Vector3.Dot(rightLocal.normalized, pointInGlobal.position.normalized) * rightLocal + Vector3.Dot(upLocal.normalized, pointInGlobal.position.normalized) * upLocal;


        Gizmos.DrawLine(NewOrigin.position, new Vector3(rightLocal.x, rightLocal.y, 0));
        Gizmos.DrawLine(NewOrigin.position, new Vector3(upLocal.x, upLocal.y, 0));
        Gizmos.DrawLine(Vector3.zero, Vector3.right);
        Gizmos.DrawLine(Vector3.zero, Vector3.up);
        Gizmos.DrawLine(NewOrigin.position, pointInLocal.position);
        Gizmos.DrawLine(Vector3.zero, pointInGlobal.position);



    }
}
