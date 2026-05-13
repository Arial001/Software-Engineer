using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObject : MonoBehaviour
{
    public GameObject ObjectTerputar; // GameObject yang ingin diubah
    public List <RotationObject> Rotations = new List <RotationObject>();

    [Header("Target Transform")]
    [SerializeField] private float targetPositionY = 1.2f;
    [SerializeField] private float targetRotationX = 90.0f;

    [Header("Target Transform2")]
    [SerializeField] private float targetPositionY2 = 0.1f;
    [SerializeField] private float targetRotationX2 = 180.0f;

    [Header("Kontrol Input")]
    [SerializeField] private KeyCode triggerKey = KeyCode.M; // Tombol untuk memicu perubahan pertama
    [Header("Kontrol Input2")]
    [SerializeField] private KeyCode triggerKey2 = KeyCode.O; // Tombol untuk memicu perubahan kedua

    [Header("Pengaturan Kehalusan Gerakan")]
    [SerializeField] private float moveDuration = 1.0f; // Durasi pergerakan posisi (dalam detik)
    [SerializeField] private float rotateDuration = 1.0f; // Durasi rotasi (dalam detik)
    private bool isFirstTargetActive = false;
    private Coroutine currentMovementCoroutine; // Untuk menghentikan coroutine sebelumnya jika dipicu lagi
    private Coroutine currentRotationCoroutine; // Untuk menghentikan coroutine sebelumnya jika dipicu lagi

    void Start()
    {
        // Pastikan ObjectTerputar tidak null saat game dimulai
        if (ObjectTerputar == null)
        {
            Debug.LogError("RotationObject: 'ObjectTerputar' belum di-assign di Inspector! Mohon seret GameObject yang ingin diubah.", this);
            enabled = false; // Menonaktifkan script ini agar tidak menyebabkan error
            return;
        }
    }

    void Update()
    {
        // Pastikan ObjectTerputar sudah di-assign
        if (ObjectTerputar == null)
        {
            return;
        }

        // Deteksi penekanan tombol 'M'
        if (Input.GetKeyDown(triggerKey))
        {
            ApplyTargetTransformSmoothly();
        }

        // --- Tambahan untuk tombol 'N' ---
        // Deteksi penekanan tombol 'N'
        if (Input.GetKeyDown(triggerKey2))
        {
            ApplyTargetTransformSmoothly2(); // Panggil fungsi baru untuk target kedua
        }
    }

    // Fungsi untuk memulai animasi transformasi secara halus ke Target Transform (Tombol M)
    public void ApplyTargetTransformSmoothly()
    {
        // Hentikan coroutine sebelumnya jika sedang berjalan untuk menghindari konflik
        if (currentMovementCoroutine != null)
        {
            StopCoroutine(currentMovementCoroutine);
        }
        if (currentRotationCoroutine != null)
        {
            StopCoroutine(currentRotationCoroutine);
        }

        // Mulai coroutine baru untuk pergerakan posisi dan rotasi
        currentMovementCoroutine = StartCoroutine(MoveYSmoothly(ObjectTerputar.transform.position.y, targetPositionY, moveDuration));
        currentRotationCoroutine = StartCoroutine(RotateXSmoothly(ObjectTerputar.transform.localEulerAngles.x, targetRotationX, rotateDuration));
        isFirstTargetActive = true;
    }

    // --- Fungsi baru untuk Target Transform2 (Tombol N) ---
    public void ApplyTargetTransformSmoothly2()
    {
        // Hentikan coroutine sebelumnya jika sedang berjalan untuk menghindari konflik
        if (currentMovementCoroutine != null)
        {
            StopCoroutine(currentMovementCoroutine);
        }
        if (currentRotationCoroutine != null)
        {
            StopCoroutine(currentRotationCoroutine);
        }

        // Mulai coroutine baru untuk pergerakan posisi dan rotasi ke target kedua
        currentMovementCoroutine = StartCoroutine(MoveYSmoothly(ObjectTerputar.transform.position.y, targetPositionY2, moveDuration));
        currentRotationCoroutine = StartCoroutine(RotateXSmoothly(ObjectTerputar.transform.localEulerAngles.x, targetRotationX2, rotateDuration));
        isFirstTargetActive = false;
    }
    public void ToggleRotation2()
    {
        // Hentikan coroutine sebelumnya jika sedang berjalan
        if (currentMovementCoroutine != null)
        {
            StopCoroutine(currentMovementCoroutine);
        }
        if (currentRotationCoroutine != null)
        {
            StopCoroutine(currentRotationCoroutine);
        }

        if (isFirstTargetActive)
        {
            // Pindah ke Target Transform 2
            currentMovementCoroutine = StartCoroutine(MoveYSmoothly(ObjectTerputar.transform.position.y, targetPositionY2, moveDuration));
            currentRotationCoroutine = StartCoroutine(RotateXSmoothly(ObjectTerputar.transform.localEulerAngles.x, targetRotationX2, rotateDuration));
        }

        isFirstTargetActive = !isFirstTargetActive;
    }

    // Coroutine untuk menggerakkan posisi Y secara halus
    IEnumerator MoveYSmoothly(float startY, float endY, float duration)
    {
        float timeElapsed = 0f;
        Vector3 startPosition = ObjectTerputar.transform.position; // Ambil posisi awal penuh

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            t = t * t * (3f - 2f * t); // Smoothstep interpolation (opsional, untuk akselerasi/deselerasi)

            Vector3 newPosition = startPosition; // Pertahankan X dan Z
            newPosition.y = Mathf.Lerp(startY, endY, t); // Interpolasi Y

            ObjectTerputar.transform.position = newPosition; // Terapkan posisi baru

            timeElapsed += Time.deltaTime;
            yield return null; // Tunggu hingga frame berikutnya
        }

        // Pastikan objek berada tepat di posisi akhir
        Vector3 finalPosition = startPosition;
        finalPosition.y = endY;
        ObjectTerputar.transform.position = finalPosition;

        Debug.Log($"Posisi Y '{ObjectTerputar.name}' diubah secara halus menjadi: {endY}", this);
    }

    // Coroutine untuk merotasi X secara halus
    IEnumerator RotateXSmoothly(float startRotationX, float endRotationX, float duration)
    {
        float timeElapsed = 0f;
        Vector3 startEuler = ObjectTerputar.transform.localEulerAngles; // Ambil rotasi Euler awal penuh
        // Pastikan rotasi akhir dikonversi ke rentang 0-360 untuk interpolasi yang benar
        endRotationX = NormalizeAngle(endRotationX);
        startRotationX = NormalizeAngle(startRotationX);

        // Perhitungan untuk Slerp yang tepat, menangani rotasi melalui 360 derajat
        Quaternion startQuaternion = Quaternion.Euler(startRotationX, startEuler.y, startEuler.z);
        Quaternion endQuaternion = Quaternion.Euler(endRotationX, startEuler.y, startEuler.z);


        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            t = t * t * (3f - 2f * t); // Smoothstep interpolation

            // Slerp untuk rotasi yang lebih alami (mengikuti busur terpendek)
            ObjectTerputar.transform.localRotation = Quaternion.Slerp(startQuaternion, endQuaternion, t);

            timeElapsed += Time.deltaTime;
            yield return null; // Tunggu hingga frame berikutnya
        }

        // Pastikan objek berada tepat di rotasi akhir
        ObjectTerputar.transform.localRotation = endQuaternion;

        Debug.Log($"Rotasi X '{ObjectTerputar.name}' diubah secara halus menjadi: {endRotationX}", this);
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
