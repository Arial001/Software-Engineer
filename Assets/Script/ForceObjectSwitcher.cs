using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceObjectSwitcher : MonoBehaviour
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
        StartCoroutine(SwitchRoutine());
    }
    private IEnumerator SwitchRoutine() 
    {
        for (int i = 0; i < objectsToMove.Count; i++)
        {
            // Matikan komponen KarakterKompleks dan KarakterKompleksP2 jika ada
            var karakterKompleks = objectsToMove[i].GetComponent<karakterkompleks>();
            if (karakterKompleks != null)
            {
                if (karakterKompleks.enabled == true)
                {
                    karakterKompleks.enabled = false;
                    Debug.Log("semua player  karakterKompleks.enabled = false;");
                }
                //karakterKompleks.enabled = false;
            }

            var karakterKompleksP2 = objectsToMove[i].GetComponent<KarakterKompleksP2>();
            if (karakterKompleksP2 != null)
            {
                if (karakterKompleksP2.enabled == true)
                {
                    karakterKompleksP2.enabled = false;
                    Debug.Log("semua player karakterKompleksP2.enabled = false;");
                }
                //karakterKompleksP2.enabled = false;
            }
        }
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < objectsToMove.Count; i++)
        {
            if (isInOriginalPosition)
            {
                // Pindahkan ke posisi target
                objectsToMove[i].transform.position = targetPositions[i].transform.position;
                Debug.Log("semua player berpindah dari A ke B");
            }
            else
            {
                // Kembalikan ke posisi awal
                objectsToMove[i].transform.position = originalPositions[i];
                Debug.Log("semua player berpindah dari B ke A");
            }
        }
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < objectsToMove.Count; i++) 
        {
            var karakterKompleks = objectsToMove[i].GetComponent<karakterkompleks>();
            var karakterKompleksP2 = objectsToMove[i].GetComponent<KarakterKompleksP2>();
            // Hidupkan kembali komponen setelah pemindahan
            if (karakterKompleks != null)
            {
                karakterKompleks.enabled = true;
            }

            if (karakterKompleksP2 != null)
            {
                karakterKompleksP2.enabled = true;
            }
        }
        yield return new WaitForSeconds(1.5f);

        // Toggle status posisi
        isInOriginalPosition = !isInOriginalPosition;
    }
}
