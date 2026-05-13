using UnityEngine;

using UnityEngine.UI;



[ExecuteAlways]

public class RaycastVisualizer : MonoBehaviour

{

    [Header("Raycast Settings")]

    [SerializeField] private Color rayColor = Color.red;

    [SerializeField] private float rayLength = 10f;

    [SerializeField] private float rayThickness = 0.05f;

    public LayerMask raycastLayerMask;



    [Header("Real-Time Info")]

    [SerializeField, Tooltip("Panjang aktual raycast (real-time)")]

    private float realRayLength = 0f;



    // Variabel untuk menyimpan data raycast

    private Ray currentRay;

    private RaycastHit currentHit;

    private bool hasHit;



    // Properties untuk mengakses data raycast

    public Ray CurrentRay => currentRay;

    public RaycastHit CurrentHit => currentHit;

    public bool HasHit => hasHit;

    public float RealRayLength => realRayLength;



    private void Start()

    {

        Debug.Log("RaycastVisualizer aktif");

    }



    private void Update()

    {

        // Membuat ray dari posisi objek ke arah depan

        currentRay = new Ray(transform.position, transform.forward);



        // Cek apakah raycast mengenai sesuatu pada layer yang sesuai

        hasHit = Physics.Raycast(currentRay, out currentHit, rayLength, raycastLayerMask);



        if (hasHit)

        {

            realRayLength = currentHit.distance;

        }

        else

        {

            realRayLength = rayLength;

        }

    }



    private void OnDrawGizmos()

    {

        // Tentukan posisi awal dan akhir raycast

        Vector3 startPoint = transform.position;

        Vector3 endPoint = transform.position + transform.forward * realRayLength;



        // Simulasikan garis dengan "tabung" untuk ketebalan

        Gizmos.color = rayColor;

        Gizmos.DrawMesh(CreateRayMesh(startPoint, endPoint, rayThickness));

    }



    private Mesh CreateRayMesh(Vector3 start, Vector3 end, float thickness)

    {

        Mesh mesh = new Mesh();



        // Tentukan titik-titik tabung

        Vector3 direction = (end - start).normalized;

        Vector3 cross = Vector3.Cross(direction, Vector3.up).normalized * thickness;



        Vector3[] vertices = new Vector3[4]

        {

      start - cross, // Kiri bawah

            start + cross, // Kanan bawah

            end - cross,   // Kiri atas

            end + cross    // Kanan atas

            };



        // Tentukan urutan segitiga untuk mesh

        int[] triangles = new int[]

    {

      0, 2, 1, // Segitiga pertama

            1, 2, 3  // Segitiga kedua

        };



        // Atur mesh

        mesh.vertices = vertices;

        mesh.triangles = triangles;

        mesh.RecalculateNormals();



        return mesh;

    }



    // Method untuk mengubah warna garis

    public void SetRayColor(Color newColor)

    {

        rayColor = newColor;

    }



    // Method untuk mengubah panjang maksimal raycast

    public void SetRayLength(float newLength)

    {

        rayLength = Mathf.Max(0, newLength);

    }



    // Method untuk mengubah ketebalan raycast

    public void SetRayThickness(float newThickness)

    {

        rayThickness = Mathf.Max(0.01f, newThickness);

    }



    // Method untuk mengatur LayerMask

    public void SetLayerMask(LayerMask newLayerMask)

    {

        raycastLayerMask = newLayerMask;

    }



    // Method untuk mendapatkan informasi hit point

    public Vector3 GetHitPoint()

    {

        return hasHit ? currentHit.point : transform.position + transform.forward * rayLength;

    }



    // Method untuk mendapatkan normal hit point

    public Vector3 GetHitNormal()

    {

        return hasHit ? currentHit.normal : Vector3.zero;

    }



    // Method untuk mendapatkan collider yang terkena

    public Collider GetHitCollider()

    {

        return hasHit ? currentHit.collider : null;

    }

}