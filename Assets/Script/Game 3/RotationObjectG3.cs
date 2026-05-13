using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObjectG3 : MonoBehaviour
{
    public GameObject ObjectTerputar;
    public List<RotationObject> Rotations = new List<RotationObject>();

    [Header("Target Transform 1")]
    [SerializeField] private float targetPositionY = 1.2f;
    [SerializeField] private float targetRotationX = 90.0f;

    [Header("Target Transform 2")]
    [SerializeField] private float targetPositionY2 = 0.1f;
    [SerializeField] private float targetRotationX2 = 180.0f;

    [Header("Pengaturan Kehalusan Gerakan")]
    [SerializeField] private float moveDuration = 1.0f;
    [SerializeField] private float rotateDuration = 1.0f;

    // Variabel status baru untuk melacak target mana yang sedang aktif
    public bool isFirstTargetActive = false;

    private Coroutine currentMovementCoroutine;
    private Coroutine currentRotationCoroutine;

    private void Awake()
    {
        if (ObjectTerputar == null)
        {
            ObjectTerputar = this.gameObject;
        }
    }

    void Start()
    {
        if (ObjectTerputar == null)
        {
            Debug.LogError("RotationObject: 'ObjectTerputar' belum di-assign di Inspector!", this);
            enabled = false;
        }
    }

    // Metode publik baru yang berfungsi sebagai toggle
    public void ToggleRotation()
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
        else
        {
            // Kembali ke Target Transform 1
            currentMovementCoroutine = StartCoroutine(MoveYSmoothly(ObjectTerputar.transform.position.y, targetPositionY, moveDuration));
            currentRotationCoroutine = StartCoroutine(RotateXSmoothly(ObjectTerputar.transform.localEulerAngles.x, targetRotationX, rotateDuration));
        }

        isFirstTargetActive = !isFirstTargetActive;
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

    IEnumerator MoveYSmoothly(float startY, float endY, float duration)
    {
        float timeElapsed = 0f;
        Vector3 startPosition = ObjectTerputar.transform.position;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            t = t * t * (3f - 2f * t);

            Vector3 newPosition = startPosition;
            newPosition.y = Mathf.Lerp(startY, endY, t);

            ObjectTerputar.transform.position = newPosition;

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        Vector3 finalPosition = startPosition;
        finalPosition.y = endY;
        ObjectTerputar.transform.position = finalPosition;
    }

    IEnumerator RotateXSmoothly(float startRotationX, float endRotationX, float duration)
    {
        float timeElapsed = 0f;
        Vector3 startEuler = ObjectTerputar.transform.localEulerAngles;
        endRotationX = NormalizeAngle(endRotationX);
        startRotationX = NormalizeAngle(startRotationX);

        Quaternion startQuaternion = Quaternion.Euler(startRotationX, startEuler.y, startEuler.z);
        Quaternion endQuaternion = Quaternion.Euler(endRotationX, startEuler.y, startEuler.z);

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            t = t * t * (3f - 2f * t);

            ObjectTerputar.transform.localRotation = Quaternion.Slerp(startQuaternion, endQuaternion, t);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        ObjectTerputar.transform.localRotation = endQuaternion;
    }

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