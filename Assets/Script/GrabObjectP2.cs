using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class GrabObjectP2 : MonoBehaviour
{
    [Header("Referensi Game1Manager")]
    public Game1Manager Game1Manager;

    [Header("Selection Manager")]
    public SelectionManager selectionManager;

    [Header("Referensi ParentObjectNameReader")]
    public ParentObjectNameReader ParentObjectNameReader;

    [Header("Target Parent")]
    public Transform targetParent;

    [Header("Gamepad Settings")]
    [SerializeField] private int joystickIndex = 0;
    private Gamepad joystick;

    [Header("Throw Settings")]
    public float throwForce = 10f;

    [Header("Raycast Visualizer")]
    public RaycastVisualizer raycastVisualizer;

    private GameObject lastSelectedObject;

    [Header("Target forbiddend Grab")]
    public GameObject forbiddend1;
    [Header("Script References BackgroundMusic")]
    public BackgroundMusic BackgroundMusic;
    private void Start()
    {
        if (Gamepad.all.Count > joystickIndex)
        {
            joystick = Gamepad.all[joystickIndex];
        }
    }

    private void Update()
    {
        if (joystick == null) return;

        Debug.DrawRay(raycastVisualizer.transform.position, raycastVisualizer.transform.forward * 10f, Color.red);

        if (selectionManager == null || targetParent == null || raycastVisualizer == null)
        {
            Debug.LogWarning("SelectionManager, TargetParent, atau RaycastVisualizer belum diatur!");
            return;
        }

        var buttonR1 = joystick["rightShoulder"] as ButtonControl;    // R1
        var buttonSquare = joystick["buttonWest"] as ButtonControl;    // Kotak
        var buttonTriangle = joystick["buttonNorth"] as ButtonControl;  // Segitiga

        if (selectionManager.interaction_Info_UI.activeSelf)
        {
            if (buttonR1 != null && buttonR1.wasPressedThisFrame)
            {
                Debug.Log("R1 ditekan");
                GameObject objectInFront = GetObjectInFront();
                if (objectInFront != null)
                {
                    InteractableObject interactable = objectInFront.GetComponent<InteractableObject>();
                    if (interactable != null && selectionManager.IsInteractable(interactable))
                    {
                        if (objectInFront != forbiddend1)
                        {
                            MoveToTargetParent(objectInFront);
                            lastSelectedObject = objectInFront;
                            Debug.Log($"objetinfront = {objectInFront}");
                            //Debug.Log($"forbiddend = {forbiddend1}");
                            BackgroundMusic.PlayMusic();
                        }
                    }
                    else
                    {
                        Debug.Log("Objek bukan InteractableObject yang valid dalam daftar SelectionManager");
                    }
                }
                else
                {
                    Debug.LogWarning("Tidak ada objek di depan kamera.");
                }
            }
        }

        if (buttonTriangle != null && buttonTriangle.wasPressedThisFrame)
        {
            if (lastSelectedObject != null)
            {
                GameObject newParent = GetObjectInFront();
                if (newParent != null)
                {
                    InteractableObject interactable = newParent.GetComponent<InteractableObject>();
                    if (interactable != null && selectionManager.IsInteractable(interactable))
                    {
                        Game1Manager.CheckALL();
                        ReparentObject(lastSelectedObject, newParent);
                        ParentObjectNameReader.ProcessChildren();
                        BackgroundMusic.PlayMusic();
                    }
                    else
                    {
                        Debug.Log("Objek bukan InteractableObject yang valid dalam daftar SelectionManager");
                    } 
                }
                else
                {
                    Debug.LogWarning("Tidak ada objek di depan untuk menjadi parent baru.");
                }
            }
        }

        if (buttonSquare != null && buttonSquare.wasPressedThisFrame)
        {
            if (lastSelectedObject != null)
            {
                
                ThrowObject(lastSelectedObject);
                lastSelectedObject = null;
            }
            else
            {
                ThrowAllObjectsInParent();
            }
        }
    }

    // Sisanya tetap sama
    private GameObject GetObjectInFront()
    {
        // Gunakan posisi dan arah dari RaycastVisualizer
        Ray ray = new Ray(raycastVisualizer.transform.position, raycastVisualizer.transform.forward);
        RaycastHit hit;

        // Periksa apakah raycast mengenai sesuatu dengan LayerMask dari RaycastVisualizer
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, raycastVisualizer.raycastLayerMask))
        {
            // Pastikan hit.transform tidak null
            if (hit.transform != null)
            {
                Debug.Log($"Objek di depan: {hit.transform.name}");
                return hit.transform.gameObject; // Kembalikan game object yang valid
            }
        }

        // Tidak ada objek yang valid
        return null;
    }

    private void MoveToTargetParent(GameObject selectedObject)
    {
        // Pindahkan posisi objek ke posisi targetParent
        selectedObject.transform.position = targetParent.position;

        // Ubah parent objek menjadi targetParent
        selectedObject.transform.SetParent(targetParent);

        Debug.Log($"{selectedObject.name} telah dipindahkan ke {targetParent.name}");
    }

    private void ReparentObject(GameObject selectedObject, GameObject newParent)
    {
        // Ubah parent objek menjadi newParent
        selectedObject.transform.SetParent(newParent.transform);

        // Sesuaikan posisi anak agar mengikuti posisi parent baru
        selectedObject.transform.localPosition = Vector3.zero;

        Debug.Log($"{selectedObject.name} telah dipindahkan ke parent baru: {newParent.name}");
    }

    private void ThrowObject(GameObject selectedObject)
    {
        // Pastikan objek memiliki Rigidbody untuk menerima gaya dorong
        Rigidbody rb = selectedObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = selectedObject.AddComponent<Rigidbody>(); // Tambahkan Rigidbody jika belum ada
        }

        // Lepaskan dari parent terlebih dahulu agar tidak terikat pada hierarki targetParent
        selectedObject.transform.SetParent(null);

        // Gunakan arah ray dari RaycastVisualizer untuk menentukan arah lemparan
        Vector3 throwDirection = raycastVisualizer.transform.forward;

        rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);

        Debug.Log($"{selectedObject.name} telah dilempar dengan kecepatan {throwForce} ke arah {throwDirection}");
    }

    private void ThrowAllObjectsInParent()
    {
        // Loop melalui semua anak dari targetParent
        for (int i = 0; i < targetParent.childCount; i++)
        {
            Transform child = targetParent.GetChild(i);

            // Pastikan objek memiliki Rigidbody untuk menerima gaya dorong
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = child.gameObject.AddComponent<Rigidbody>(); // Tambahkan Rigidbody jika belum ada
            }

            // Gunakan arah ray dari RaycastVisualizer untuk menentukan arah lemparan
            Vector3 throwDirection = raycastVisualizer.transform.forward;

            rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);

            // Lepaskan dari parent terlebih dahulu agar tidak terikat pada hierarki targetParent
            child.SetParent(null);

            Debug.Log($"{child.name} telah dilempar dengan kecepatan {throwForce} ke arah {throwDirection}");
        }
    }
}
