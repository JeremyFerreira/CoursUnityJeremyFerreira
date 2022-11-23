using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI.Table;

public class IAVision : MonoBehaviour
{
    [Range(0, 180)]
    [SerializeField] float Angle;
    [SerializeField] float Radius;
    [SerializeField] Transform[] point;
    private Camera camera;
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
        camera = GetComponentInChildren<Camera>();
        Gizmos.color = Color.red;
        float ang = 0;
        int pointPos = 0;
        for (int i = 0; i < point.Length; i++)
        {
            float currentAng = CalculateAngle(point[i].position, transform.position, transform.right) * 2;
            
            if ( currentAng > ang)
            {
                ang = currentAng;
                pointPos = i;
            }
        }
        
        Gizmos.color = Color.blue;
        DrawFOV(Radius, ang, transform.position);

        for (int i = 0; i < point.Length; i++)
        {

            if (pointPos == i)
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.white;
            }
            Gizmos.DrawLine(transform.position, point[i].position);
        }

        camera.fieldOfView = ang;

    }

    const float tau = 6.28318530718f;
    const float radToDeg = 57.2957795131f;


    void DrawFOV(float radius, float angle, Vector3 pos)
    {
        float angleRadian = Mathf.Deg2Rad * angle/2;
        Vector2 point1 = transform.position + new Vector3(Mathf.Cos(angleRadian), Mathf.Sin(angleRadian),0) * radius;
        float angleRadian2 = Mathf.Deg2Rad * (- angle / 2);
        Vector2 point2 = transform.position + new Vector3(Mathf.Cos(angleRadian2), Mathf.Sin(angleRadian2),0) * radius;

        Gizmos.DrawLine(pos, point1);
        Gizmos.DrawLine(pos, point2);
    }
    float CalculateAngle(Vector3 point, Vector3 origin, Vector3 direction)
    {
        Vector3 OP = (point - origin).normalized;
        Vector3 OA = direction;
        float angle = Mathf.Acos(Vector3.Dot(OP,OA))*radToDeg;
        return angle;
    }
}
