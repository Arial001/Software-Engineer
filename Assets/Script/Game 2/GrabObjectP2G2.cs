using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class GrabObjectP2G2 : MonoBehaviour
{
    [Header("Selection Manager")]
    public SelectionManager selectionManager; // Referensi ke script SelectionManager

    [Header("Target Parent")]
    public Transform targetParent; // GameObject yang akan menjadi parent baru untuk kartu yang diambil

    [Header("Gamepad Settings")]
    [SerializeField] private int joystickIndex = 0;
    private Gamepad joystick;
    [SerializeField] private int player = 0;
    private int Hasilplayer1;
    private int Hasilplayer2;
    private int Hasilplayer3;
    private int Hasilplayer4;

    [Header("Throw Settings")]
    public float throwForce = 10f; // Kecepatan lemparan

    [Header("Raycast Visualizer")]
    public RaycastVisualizer raycastVisualizer; // Referensi ke script RaycastVisualizer untuk arah lemparan

    private GameObject lastSelectedObject; // Objek terakhir yang dipilih berdasarkan nama di hierarki

    [Header("Target forbiddend Grab")]
    public List<GameObject> forbiddendGrabs; // Ganti nama agar lebih deskriptif
    [Header("Target forbiddend Reparent")]
    public List<GameObject> forbiddendReparents; // Ganti nama agar lebih deskriptif

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

        // Pindahkan objek berdasarkan kondisi UI dari SelectionManager
        if (selectionManager.interaction_Info_UI.activeSelf) // UI aktif
        {
            if (buttonR1 != null && buttonR1.wasPressedThisFrame)
            {
                Debug.Log("R1 ditekan");
                GameObject objectInFront = GetObjectInFront();
                if (!forbiddendGrabs.Any(obj => obj.name == objectInFront.name))
                {
                    // Hanya pindahkan jika objek yang valid ditemukan
                    if (objectInFront != null)
                    {
                        InteractableObject interactable = objectInFront.GetComponent<InteractableObject>();
                        if (interactable != null && selectionManager.IsInteractable(interactable))
                        {
                                MoveToTargetParent(objectInFront);
                                lastSelectedObject = objectInFront;
                                Debug.Log($"objetinfront = {objectInFront}");
                                BackgroundMusic.PlayMusic();
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
                else
                {
                    Debug.LogWarning("Tidak ada objek di depan untuk menjadi parent baru.");
                }

            }
        }
        
        // Reparent objek ke parent lain
        if (buttonTriangle != null && buttonTriangle.wasPressedThisFrame)
        {
            // Temukan objek (kartu di lantai) yang ada di depan kamera
            GameObject newParent = GetObjectInFront();
            if (!forbiddendReparents.Any(obj => obj.name == newParent.name))
            {
                if (newParent != null)
                {
                    // Ambil referensi skrip ParentObjectNameReaderG2 dari kartu yang disorot
                    ParentObjectNameReaderG2 readerScript = newParent.GetComponent<ParentObjectNameReaderG2>();

                    if (readerScript != null)
                    {
                        // Periksa apakah ada objek yang sedang dipegang (lastSelectedObject)
                        if (lastSelectedObject != null)
                        {
                            // Skenario 1: Ada kartu di tangan
                            // Pindahkan kartu dari tangan menjadi "anak" dari kartu di lantai
                            ReparentObject(lastSelectedObject, newParent);

                            // Panggil ProcessChildren() pada skrip yang benar
                            readerScript.verifplayer = player;
                            readerScript.ProcessChildren();

                            // Hancurkan kartu yang sudah dibandingkan dari tangan
                            Destroy(lastSelectedObject);
                            lastSelectedObject = null;
                            Hasilplayer1 = readerScript.jawabanbenarP1 + Hasilplayer1;
                            Debug.Log($"hasilplayer1: 'hasilplayer1' ({Hasilplayer1}).", this);
                            Hasilplayer2 = readerScript.jawabanbenarP2 + Hasilplayer2;
                            Debug.Log($"hasilplayer2: 'hasilplayer2' ({Hasilplayer2}).", this);
                            Hasilplayer3 = readerScript.jawabanbenarP3 + Hasilplayer3;
                            Debug.Log($"hasilplayer3: 'hasilplayer3' ({Hasilplayer3}).", this);
                            Hasilplayer4 = readerScript.jawabanbenarP4 + Hasilplayer4;
                            Debug.Log($"hasilplayer4: 'hasilplayer4' ({Hasilplayer4}).", this);
                        }
                        else
                        {
                            // Skenario 2: TIDAK ada kartu di tangan
                            // Tetap panggil ProcessChildren() pada kartu di lantai
                            readerScript.ProcessChildren();
                            Debug.Log("Melakukan proses child pada kartu di lantai tanpa objek di tangan.");
                        }

                        BackgroundMusic.PlayMusic();
                    }
                    else
                    {
                        Debug.LogWarning("Objek yang disorot tidak memiliki skrip ParentObjectNameReaderG2.");
                    }
                }
                else
                {
                    Debug.LogWarning("Tidak ada objek di depan untuk menjadi parent baru.");
                }
            }
            else
            {
                Debug.LogWarning("Tidak ada objek di depan untuk menjadi parent baru.");
            }

        }

        // Lempar objek individu atau semua anak di parent jika tombol lempar ditekan
        if (buttonSquare != null && buttonSquare.wasPressedThisFrame)
        {
            if (lastSelectedObject != null)
            {
                ThrowObject(lastSelectedObject);
                lastSelectedObject = null; // Reset setelah dilempar
            }
            else
            {
                ThrowAllObjectsInParent();
            }
        }
    }
    public int HasilP1G2()
    {
        return Hasilplayer1;
    }
    public int HasilP2G2()
    {
        return Hasilplayer2;
    }
    public int HasilP3G2()
    {
        return Hasilplayer3;
    }
    public int HasilP4G2()
    {
        return Hasilplayer4;
    }

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
