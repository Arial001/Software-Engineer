using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultGame2 : MonoBehaviour
{
    [Header("Referensi Scripts GrabObjectP2G2")]
    public List<GrabObjectP2G2> GrabObjectP2G2 = new List<GrabObjectP2G2>();
    public GrabObjectG2 GrabObjectG2;
    public int Player1;
    public int Player2;
    public int Player3;
    public int Player4;
    


    // Start is called before the first frame update
    void Start()
    {
        

    }
    public void HitungNilai()
    {

        Player1 = GrabObjectG2.HasilP1G2();
        Player2 = GrabObjectP2G2[0].HasilP2G2();
        Player3 = GrabObjectP2G2[1].HasilP3G2();
        Player4 = GrabObjectP2G2[2].HasilP4G2();


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
