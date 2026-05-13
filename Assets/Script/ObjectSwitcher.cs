using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectList;
    private int currentIndex = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SwitchObjectPosition();
        }
    }

    public void SwitchObjectPosition()
    {
        if (objectList.Count < 2) return;

        // Simpan posisi objek saat ini
        Vector3 currentPosition = objectList[currentIndex].transform.position;

        // Hitung indeks berikutnya
        int nextIndex = (currentIndex + 1) % objectList.Count;

        // Tukar posisi objek saat ini dengan objek berikutnya
        objectList[currentIndex].transform.position = objectList[nextIndex].transform.position;
        objectList[nextIndex].transform.position = currentPosition;

        // Perbarui currentIndex
        currentIndex = nextIndex;
    }
}