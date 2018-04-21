using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeData : MonoBehaviour {

    public float timer = 0f;
    public int age = 15;
    PlayerControl pc;

	void Start () {
        pc = GetComponent<PlayerControl>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(((int)timer + 15)>age)
        {
            changeAge();
        }
	}

    void changeAge()
    {
        age++;
        if(age<35)
        {
            pc.speed++;
        }
        else
        {
            if(pc.speed>1)
            {
                pc.speed--;
            }
        }
    }
}
