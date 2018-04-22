using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeData : MonoBehaviour {

    public float timer = 0f;
    public int age = 15;
    Transform c;
    public List<string> life;

    PlayerControl pc;
    Text ageText1;
    Text ageText2;
    public Text result;
    public GameObject scrollView;
    Image speedImage;
    public bool stop = false;
    public Transform target;

    Vector3 startPosition = Vector3.zero;
    float startTime = 0f;
    Quaternion startrotation = Quaternion.identity;
    public PlatformManager pm;

    void Start () {
        pc = GetComponent<PlayerControl>();
        c = GameObject.FindGameObjectWithTag("MainCamera").transform;
        GameObject[] temp = GameObject.FindGameObjectsWithTag("AgeText");
        ageText1 = temp[0].GetComponent<Text>();
        ageText2 = temp[1].GetComponent<Text>();
        speedImage = GameObject.FindGameObjectWithTag("SpeedImage").GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        speedImage.fillAmount = pc.speed / pc.maxSpeed;

        if(age<35)
        {
            if (((int)timer + 70) >= age * 5)
            {
                changeAge();
            }
        }else if(age <60)
        {
            if (((int)timer - 65) >= age)
            {
                changeAge();
            }
        }else
        {
            if(!stop)
            {
                stop = true;
                pc.movetype = PlayerControl.MoveType.Stop;
                pm.ActivateAll();

                string show = "";
                foreach (string s in life)
                {
                    show = show + s + "\n";
                }
                result.text = show;
                scrollView.SetActive(true);
            }
            if (startTime!=0f)
            {
                float speed = (Time.time - startTime)/2f;
                c.position = Vector3.Lerp(startPosition, target.position, speed);
                c.rotation = Quaternion.Lerp(startrotation, target.rotation, speed);
                
            }else
            {
                startTime = Time.time;
                startPosition = c.position;
                startrotation = c.rotation;
            }


        }

	}

    void changeAge()
    {
        age++;
        ageText1.text = "Age:" + age;
        ageText2.text = "Age:" + age;

        if (age<35)
        {
            
            pc.speed += 0.1f;
            if(pc.speed>15)
            {
                pc.speed = 15;
            }
        }
        else
        {
            if(pc.speed>1)
            {
                pc.speed -= 0.7f;
            }
        }
    }

    public void addExperience(int age, string thing)
    {
        string s = "Age " + age + "  " + thing;
        life.Add(s);
    }

    public void printLife()
    {
        foreach(string s in life)
        {
            Debug.Log(s);
        }
    }
}
