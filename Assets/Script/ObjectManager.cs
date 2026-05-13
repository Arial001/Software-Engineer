using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public List<GameObject> objects;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Gobject1OFF()
    {
        objects[0].gameObject.SetActive(false);
    }
    public void Gobject2OFF()
    {
        objects[1].gameObject.SetActive(false);
    }
    public void Gobject3OFF()
    {
        objects[2].gameObject.SetActive(false);
      
    }
    public void Gobject4OFF()
    {
        objects[3].gameObject.SetActive(false);

    }
    public void Gobject1ON()
    {
        objects[0].gameObject.SetActive(true);
    }
    public void Gobject2ON()
    {
        objects[1].gameObject.SetActive(true);
    }
    public void Gobject3ON()
    {
        objects[2].gameObject.SetActive(true);
    }
    public void Gobject4ON()
    {
        objects[3].gameObject.SetActive(true);
    }
}
