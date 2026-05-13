using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedObjectSwitcher : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToMove;
    [SerializeField] private List<GameObject> targetPositions;
    private List<Vector3> originalPositions = new List<Vector3>();
    private bool isInOriginalPosition = true;

    private void Start()
    {
        // Menyimpan posisi awal semua objek yang akan dipindahkan
        foreach (GameObject obj in objectsToMove)
        {
            originalPositions.Add(obj.transform.position);
        }
    }



    public void SwitchObjectPositions()
    {
        if (objectsToMove.Count != targetPositions.Count)
        {
            Debug.LogError("Jumlah objek dan posisi target tidak sama!");
            return;
        }

        for (int i = 0; i < objectsToMove.Count; i++)
        {
            if (isInOriginalPosition)
            {
                // Pindahkan ke posisi target
                objectsToMove[i].transform.position = targetPositions[i].transform.position;
            }
            else
            {
                // Kembalikan ke posisi awal
                objectsToMove[i].transform.position = originalPositions[i];
            }
        }

        // Toggle status posisi
        isInOriginalPosition = !isInOriginalPosition;
    }
}