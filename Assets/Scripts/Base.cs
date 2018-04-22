using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {

    public string text;

    private void OnTriggerEnter(Collider other)
    {
            PlayerControl pc = other.gameObject.GetComponent<PlayerControl>();
            LifeData ld = other.gameObject.GetComponent<LifeData>();
            ld.addExperience(ld.age, text);

            string s = "Age " + ld.age + "  " + text;
            Debug.Log(s);

    }

}
