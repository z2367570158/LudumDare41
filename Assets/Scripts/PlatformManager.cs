using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour {

    GameObject[] platforms;
	void Start () {

        platforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach (GameObject g in platforms)
        {
            if(g.GetComponent<Platform>().disableFirst)
            {
                g.SetActive(false);
            }
        }
	}
    public void ActivateAll()
    {
        foreach (GameObject g in platforms)
        {
                g.SetActive(true);
        }

    }

	
}
