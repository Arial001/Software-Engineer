using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginGame2 : MonoBehaviour
{
    public GameObject game2;
    public ObjectPlayG2 ObjectPlayG2;
    public ForceObjectSwitcher ForceObjectSwitcher;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoginKeGame2()
    {



        Debug.Log("line21 logingame2");
        StartCoroutine(Reload());

    }
    private IEnumerator Reload()
    {
        game2.SetActive(true);
        Debug.Log("line28 logingame2");
        yield return new WaitForSeconds(1.0f);
        ObjectPlayG2.MAtikanSemuaKamera();
        Debug.Log("line31 logingame2");
        yield return new WaitForSeconds(1.0f);
        ForceObjectSwitcher.SwitchObjectPositions();
        Debug.Log("line34 logingame2");
        yield return new WaitForSeconds(1.0f);
        ObjectPlayG2.PlayGame2();
        Debug.Log("line37 logingame2");
        yield return new WaitForSeconds(1.0f);
        ObjectPlayG2.CekposisiKamera();
        Debug.Log("line40 logingame2");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
