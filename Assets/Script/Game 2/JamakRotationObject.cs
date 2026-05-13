using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JamakRotationObject : MonoBehaviour
{
    // Mengubah ObjectTerputar menjadi List agar bisa mengontrol banyak objek
    [Header("Daftar Objek yang Akan Diputar")]
    public List<GameObject> ObjectsToManipulate = new List<GameObject>(); // List yang akan diisi di Inspector

    // --- Mengeluarkan variabel target dan durasi ke dalam kelas baru ---
    [System.Serializable] // Agar kelas ini bisa terlihat dan diedit di Inspector
    public class TransformSetting
    {
        [Header("Target Transform")]
        [Tooltip("Posisi Y target untuk objek.")]
        public float targetPositionY;
        [Tooltip("Rotasi X target untuk objek.")]
        public float targetRotationX;

        [Header("Pengaturan Kehalusan Gerakan")]
        [Tooltip("Durasi pergerakan posisi (dalam detik).")]
        public float moveDuration = 1.0f;
        [Tooltip("Durasi rotasi (dalam detik).")]
        public float rotateDuration = 1.0f;
    }

    [Header("Set Target 1")]
    public TransformSetting settings1; // Objek yang berisi setting untuk triggerKey (M)
    [Header("Set Target 2")]
    public TransformSetting settings2; // Objek yang berisi setting untuk triggerKey2 (N)
    [Header("Set Target 3")] // Header untuk setingan target ketiga
    public TransformSetting settings3; // Objek yang berisi setting untuk triggerKey3 (O)


    [Header("Kontrol Input")]
    [SerializeField] private KeyCode triggerKey = KeyCode.M; // Tombol untuk memicu perubahan pertama
    [SerializeField] private KeyCode triggerKey2 = KeyCode.N; // Tombol untuk memicu perubahan kedua
    [SerializeField] private KeyCode triggerKey3 = KeyCode.O; // Tombol untuk memicu perubahan ketiga


    // Dictionary untuk menyimpan referensi Coroutine per GameObject
    // Ini penting agar setiap objek bisa memiliki coroutine-nya sendiri
    private Dictionary<GameObject, Coroutine> movementCoroutines = new Dictionary<GameObject, Coroutine>();
    private Dictionary<GameObject, Coroutine> rotationCoroutines = new Dictionary<GameObject, Coroutine>();


    void Start()
    {
        // Pastikan list ObjectsToManipulate tidak kosong
        if (ObjectsToManipulate.Count == 0)
        {
            Debug.LogWarning("JamakRotationObject: List 'ObjectsToManipulate' kosong! Seret GameObject ke slot di Inspector.", this);
            enabled = false; // Nonaktifkan script jika tidak ada objek untuk diubah
            return;
        }

        // Opsional: Validasi awal untuk setiap objek di list
        foreach (GameObject obj in ObjectsToManipulate)
        {
            if (obj == null)
            {
                Debug.LogWarning("JamakRotationObject: Ada slot kosong di list 'ObjectsToManipulate'!", this);
            }
        }
    }

    void Update()
    {
        // Deteksi penekanan tombol 'M'
        //if (Input.GetKeyDown(triggerKey))
        //{
        //    // Panggil fungsi Apply untuk seting 1
        //    ApplyTransformsSmoothlyToAllObjects(settings1);
        //}

        //// Deteksi penekanan tombol 'N'
        //if (Input.GetKeyDown(triggerKey2))
        //{
        //    // Panggil fungsi Apply untuk seting 2
        //    ApplyTransformsSmoothlyToAllObjects(settings2);
        //}

        //// --- BARU: Deteksi penekanan tombol 'O' ---
        //if (Input.GetKeyDown(triggerKey3))
        //{
        //    // Panggil fungsi Apply untuk seting 3
        //    ApplyTransformsSmoothlyToAllObjects(settings3);
        //}
    }

    public void PutarKedepan()
    {
        ApplyTransformsSmoothlyToAllObjects(settings1);
    }
    public void PutarKeatas()
    {
        ApplyTransformsSmoothlyToAllObjects(settings2);
    }
    public void PutarKebawah()
    {
        ApplyTransformsSmoothlyToAllObjects(settings3);
    }
    public void PutarResetKebawah()
    {
        ResetToOriginal();
    }

    // Fungsi utama untuk menerapkan transformasi ke semua objek dalam list
    private void ApplyTransformsSmoothlyToAllObjects(TransformSetting currentSettings)
    {
        if (ObjectsToManipulate.Count == 0) return; // Keluar jika tidak ada objek

        foreach (GameObject obj in ObjectsToManipulate)
        {
            if (obj == null) continue; // Lewati jika ada slot kosong di list

            // Hentikan coroutine objek ini jika sedang berjalan
            if (movementCoroutines.ContainsKey(obj) && movementCoroutines[obj] != null)
            {
                StopCoroutine(movementCoroutines[obj]);
            }
            if (rotationCoroutines.ContainsKey(obj) && rotationCoroutines[obj] != null)
            {
                StopCoroutine(rotationCoroutines[obj]);
            }

            // Mulai coroutine baru untuk objek ini dan simpan referensinya
            movementCoroutines[obj] = StartCoroutine(MoveYSmoothly(obj, obj.transform.position.y, currentSettings.targetPositionY, currentSettings.moveDuration));
            rotationCoroutines[obj] = StartCoroutine(RotateXSmoothly(obj, obj.transform.localEulerAngles.x, currentSettings.targetRotationX, currentSettings.rotateDuration));
        }
    }

    public void ResetToOriginal()
    {
        foreach (GameObject obj in ObjectsToManipulate)
        {
            if (obj == null) continue;
            // Ambil script RotationObject dari GameObject
            RotationObject rotScript = obj.GetComponent<RotationObject>();
            if (rotScript != null)
            {
                rotScript.ToggleRotation2();
            }
            else
            {
                Debug.LogWarning($"Objek '{obj.name}' tidak punya komponen RotationObject!", obj);
            }
        }
    }

    // Coroutine untuk menggerakkan posisi Y secara halus
    IEnumerator MoveYSmoothly(GameObject targetObject, float startY, float endY, float duration)
    {
        if (targetObject == null) yield break; // Keluar jika objek sudah null

        float timeElapsed = 0f;
        Vector3 startPosition = targetObject.transform.position;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            t = t * t * (3f - 2f * t); // Smoothstep interpolation (opsional)

            Vector3 newPosition = startPosition;
            newPosition.y = Mathf.Lerp(startY, endY, t);

            targetObject.transform.position = newPosition;

            timeElapsed += Time.deltaTime;
            yield return null;
            if (targetObject == null) yield break; // Cek lagi jika objek dihancurkan saat yield
        }

        // Pastikan objek berada tepat di posisi akhir
        if (targetObject != null)
        {
            Vector3 finalPosition = startPosition;
            finalPosition.y = endY;
            targetObject.transform.position = finalPosition;
            Debug.Log($"Posisi Y '{targetObject.name}' diubah secara halus menjadi: {endY}", targetObject);
        }
    }

    // Coroutine untuk merotasi X secara halus
    IEnumerator RotateXSmoothly(GameObject targetObject, float startRotationX, float endRotationX, float duration)
    {
        if (targetObject == null) yield break; // Keluar jika objek sudah null

        float timeElapsed = 0f;
        Vector3 startEuler = targetObject.transform.localEulerAngles;

        endRotationX = NormalizeAngle(endRotationX);
        startRotationX = NormalizeAngle(startRotationX);

        Quaternion startQuaternion = Quaternion.Euler(startRotationX, startEuler.y, startEuler.z);
        Quaternion endQuaternion = Quaternion.Euler(endRotationX, startEuler.y, startEuler.z);

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            t = t * t * (3f - 2f * t); // Smoothstep interpolation

            targetObject.transform.localRotation = Quaternion.Slerp(startQuaternion, endQuaternion, t);

            timeElapsed += Time.deltaTime;
            yield return null;
            if (targetObject == null) yield break; // Cek lagi jika objek dihancurkan saat yield
        }

        // Pastikan objek berada tepat di rotasi akhir
        if (targetObject != null)
        {
            targetObject.transform.localRotation = endQuaternion;
            Debug.Log($"Rotasi X '{targetObject.name}' diubah secara halus menjadi: {endRotationX}", targetObject);
        }
    }

    // Fungsi bantu untuk menormalisasi sudut ke rentang 0-360
    private float NormalizeAngle(float angle)
    {
        angle = angle % 360;
        if (angle < 0)
        {
            angle += 360;
        }
        return angle;
    }
}