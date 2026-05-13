using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcherStatis : MonoBehaviour
{
    [SerializeField] private Camera mainCamera; // Kamera yang akan dipindahkan
    [SerializeField] private List<Transform> cameraPositions; // Daftar posisi kamera
    private int currentPositionIndex = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SwitchToNextPosition();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SwitchToPreviousPosition();
        }
    }

    private void SwitchToNextPosition()
    {
        currentPositionIndex = (currentPositionIndex + 1) % cameraPositions.Count;
        MoveCamera();
    }

    private void SwitchToPreviousPosition()
    {
        currentPositionIndex = (currentPositionIndex - 1 + cameraPositions.Count) % cameraPositions.Count;
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (cameraPositions.Count > 0)
        {
            Transform targetTransform = cameraPositions[currentPositionIndex];
            mainCamera.transform.position = targetTransform.position;
            mainCamera.transform.rotation = targetTransform.rotation;
        }
    }

    public void mainCameraOFF()
    {
        mainCamera.gameObject.SetActive(false);
    }
    public void mainCameraON()
    {
        mainCamera.gameObject.SetActive(true);
    }
}