using UnityEngine;
using System.Collections.Generic;

[ExecuteAlways]
public class RaycastVisualizerForObject : MonoBehaviour
{
    [Header("Raycast Settings")]
    [SerializeField] private Color rayColor = Color.red;
    [SerializeField] private float rayLength = 10f;
    [SerializeField] private float rayThickness = 0.05f;
    public LayerMask raycastLayerMask;

    [Header("Ray Directions")]
    [SerializeField, Tooltip("Arah-arah raycast yang diinginkan (dalam koordinat lokal)")]
    private Vector3[] rayDirections = new Vector3[]
    {
        Vector3.forward,
        Vector3.back,
        Vector3.left,
        Vector3.right,
        Vector3.up,
        Vector3.down
    };

    private RaycastHit _firstHit;
    private bool _hasHit = false;
    public RaycastHit CurrentHit => _firstHit;
    public bool HasHit => _hasHit;

    [Header("Real-Time Info")]
    [SerializeField, Tooltip("Panjang aktual setiap raycast (real-time)")]
    private float[] realRayLengths;
    private RaycastHit[] allHits;

    private void Start()
    {
        InitializeArrays();
    }

    private void OnValidate()
    {
        InitializeArrays();
    }

    private void InitializeArrays()
    {
        if (rayDirections != null)
        {
            if (realRayLengths == null || realRayLengths.Length != rayDirections.Length)
            {
                realRayLengths = new float[rayDirections.Length];
            }
            if (allHits == null || allHits.Length != rayDirections.Length)
            {
                allHits = new RaycastHit[rayDirections.Length];
            }
        }
    }

    private void Update()
    {
        _hasHit = false;

        if (rayDirections == null || rayDirections.Length == 0)
        {
            return;
        }

        if (allHits == null || allHits.Length != rayDirections.Length)
        {
            InitializeArrays();
        }

        for (int i = 0; i < rayDirections.Length; i++)
        {
            Ray ray = new Ray(transform.position, transform.TransformDirection(rayDirections[i]));
            RaycastHit hit;

            bool raycastSucceeded = Physics.Raycast(ray, out hit, rayLength, raycastLayerMask);

            if (raycastSucceeded)
            {
                realRayLengths[i] = hit.distance;
                allHits[i] = hit;

                if (!_hasHit)
                {
                    _firstHit = hit;
                    _hasHit = true;
                }
            }
            else
            {
                realRayLengths[i] = rayLength;
                allHits[i] = new RaycastHit();
            }
        }
    }

    private void OnDrawGizmos()
    {
        // --- Perbaikan di sini: periksa ukuran array sebelum menggambar ---
        if (rayDirections == null || realRayLengths == null || rayDirections.Length != realRayLengths.Length)
        {
            return;
        }

        Gizmos.color = rayColor;

        for (int i = 0; i < rayDirections.Length; i++)
        {
            Vector3 startPoint = transform.position;
            Vector3 endPoint = startPoint + transform.TransformDirection(rayDirections[i]) * realRayLengths[i];

            Gizmos.DrawMesh(CreateRayMesh(startPoint, endPoint, rayThickness));
        }
    }

    private Mesh CreateRayMesh(Vector3 start, Vector3 end, float thickness)
    {
        Mesh mesh = new Mesh();
        Vector3 direction = (end - start).normalized;
        Vector3 cross;
        if (Mathf.Abs(Vector3.Dot(direction, Vector3.up)) > 0.99f)
        {
            cross = Vector3.Cross(direction, Vector3.forward).normalized * thickness;
        }
        else
        {
            cross = Vector3.Cross(direction, Vector3.up).normalized * thickness;
        }

        Vector3[] vertices = new Vector3[4]
        {
            start - cross,
            start + cross,
            end - cross,
            end + cross
        };
        int[] triangles = new int[]
        {
            0, 2, 1,
            1, 2, 3
        };
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        return mesh;
    }
}