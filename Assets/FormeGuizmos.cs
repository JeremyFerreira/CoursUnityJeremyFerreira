using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class FormeGuizmos : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] float radius;
    [Range(3, 15)]
    [SerializeField] int detail;
    [Range(2, 15)]
    [SerializeField] int density;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    const float TAU = 6.28318530718f;
    private void OnDrawGizmos()
    {
        DrawCircle(radius, Quaternion.identity, transform.position, detail);
        DrawStar(radius, Quaternion.identity, transform.position, detail, density);
    }
    public void DrawCircle(float radius, Quaternion rot, Vector3 pos, int detail)
    {
        Vector3[] points3D = new Vector3[detail];
        for (int i = 0; i < detail; i++)
        {
            float t = i / (float)detail;
            float angleRadian = t * TAU;
            Vector2 point2D = new Vector2(Mathf.Cos(angleRadian), Mathf.Sin(angleRadian)) * radius;
            points3D[i] = pos + rot * point2D;
        }
        for (int i = 0; i < points3D.Length; i++)
        {
            Gizmos.DrawLine(points3D[i], points3D[(i + 1) % points3D.Length]);
        }

    }
    public void DrawStar(float radius, Quaternion rot, Vector3 pos, int detail, int density)
    {
        Vector3[] points3D = new Vector3[detail];
        for (int i = 0; i < detail; i++)
        {
            float t = i / (float)detail;
            float angleRadian = t * TAU;
            Vector2 point2D = new Vector2(Mathf.Cos(angleRadian), Mathf.Sin(angleRadian)) * radius;
            points3D[i] = pos + rot * point2D;
        }
        for (int i = 0; i < points3D.Length; i++)
        {
            Gizmos.DrawLine(points3D[i], points3D[(i + density) % points3D.Length]);
        }
    }
}
