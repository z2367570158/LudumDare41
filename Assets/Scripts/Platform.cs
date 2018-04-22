using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Platform : MonoBehaviour {

    public bool disableFirst;
    public bool addText;
    public string text;

    public float speedChange = 0f;
    public float jumpSpeedChange = 0f;
    public float gravityChange = 0f;

    public List<GameObject> triggerSet;
    public List<GameObject> disableSet;

    public bool triggered;
    private Text dont;

    

    private void OnTriggerEnter(Collider other)
    {
        if(!triggered)
        {
            triggered = true;

            PlayerControl pc = other.gameObject.GetComponent<PlayerControl>();
            pc.speed += speedChange;
            if(pc.speed>15)
            {
                pc.speed = 15;
            }else if(pc.speed<1)
            {
                pc.speed = 1;
            }
            pc.jumpSpeed += jumpSpeedChange;
            pc.gravityFactor += gravityChange;

            //string show = "";
            //if(speedChange!=0)
            //{
            //    show = "Speed change " + speedChange;
            //}
            //if(jumpSpeedChange!=0)
            //{
            //    show = show+"\nJump speed change " + jumpSpeedChange;
            //}

            //dont = GameObject.FindGameObjectWithTag("DontUI").GetComponent<Text>();
            //dont.text = show;
            //dont.color = Color.white;

            if(addText)
            {
                LifeData ld = other.gameObject.GetComponent<LifeData>();
                ld.addExperience(ld.age, text);

                string s = "Age " + ld.age + "  " + text;
                Debug.Log(s);
            }


            foreach (GameObject g in triggerSet)
            {
                g.SetActive(true);
            }


            foreach (GameObject g in disableSet)
            {
                g.SetActive(false);
            }

            GetComponent<Renderer>().material.color = Color.green;
        }

    }

}
