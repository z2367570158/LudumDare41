using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeoutText : MonoBehaviour {
    Text t;

    private void Start()
    {
        t = GetComponent<Text>();
    }

    void Update () {
        t.color = Color.Lerp(t.color, Color.clear, 0.03f);
	}
}
