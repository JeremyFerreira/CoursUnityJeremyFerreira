using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class DrawCircleGizmos : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] float radius;
    [Range(0, 100)]
    [SerializeField] float radius2;
    [Range(3,100)]
    [SerializeField] float radius3;
    [Range(3, 100)]
    [SerializeField] int detail;
    [SerializeField] Transform point;
    [SerializeField] Transform point2;
    [SerializeField] Transform point3;
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
        //CircleCollisionPoint();
        CircleCollisionCircle();
    }

    public void CircleCollisionPoint()
    {
        DrawCircle(radius, Quaternion.identity, transform.position,detail);
        
        Handles.DrawWireDisc(transform.position, transform.forward, radius);
        
    }
    public void CircleCollisionCircle()
    {
        if ((point3.position - point2.position).sqrMagnitude > (radius2 + radius3) * (radius2 + radius3))
        {
            Handles.color = Color.red;
        }
        else
        {
            Handles.color = Color.blue;
        }
        Handles.DrawWireDisc(point3.position, transform.forward, radius3);
        Handles.DrawWireDisc(point2.position, transform.forward, radius2);
    }

    public const float TAU = 6.28318530718f;
    public static Vector2 GetUnitVectorByAngle(float angRad)
    {
        return new Vector2(Mathf.Cos(angRad), Mathf.Sin(angRad));
    }

    public void DrawCircle(float radius, Quaternion rot, Vector3 pos, int detail)
    {
        Vector3[] points3D = new Vector3[detail];
        for (int i = 0; i < detail; i++)
        {
            float t = i / (float)detail;
            float angRad = t * TAU;
            Vector2 point2D = GetUnitVectorByAngle(angRad) * radius;
            points3D[i] = pos + rot * point2D;
        }

        if ((transform.position - point.position).sqrMagnitude > radius * radius)
        {
            Gizmos.color = Color.white;
            
        }
        else
        {
            Gizmos.color = Color.green;
        }
        for (int i = 0; i < points3D.Length; i++)
        {
            Gizmos.DrawLine(points3D[i], points3D[(i + 1) % points3D.Length]);
        }

    }
}
