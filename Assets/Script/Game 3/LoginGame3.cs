using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginGame3 : MonoBehaviour
{
    public GameObject game3;
    public ObjectPlayG3 ObjectPlayG3;
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
        game3.SetActive(true);
        Debug.Log("line28 logingame2");
        yield return new WaitForSeconds(2.0f);
        ObjectPlayG3.MAtikanSemuaKamera();
        Debug.Log("line31 logingame2");
        yield return new WaitForSeconds(1.0f);
        ForceObjectSwitcher.SwitchObjectPositions();
        yield return new WaitForSeconds(5.0f);
        Debug.Log("line34 logingame2");
        ObjectPlayG3.PlayGame2();
        yield return new WaitForSeconds(2.0f);
        Debug.Log("line37 logingame2");
        ObjectPlayG3.CekposisiKamera();
        Debug.Log("line40 logingame2");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
