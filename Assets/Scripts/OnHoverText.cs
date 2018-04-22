using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnHoverText : MonoBehaviour
{
    Text text;
    [SerializeField] Text dont;
    int suicidecount = 0;

    private void Awake()
    {
        text = transform.GetComponent<Text>();
    }

    public void OnClick()
    {
        suicidecount++;
        text.color = new Color(1, 1f- (float)suicidecount / 3, 1f- (float)suicidecount / 3);

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<LifeData>().stop)
            suicidecount = 4;

        switch(suicidecount)
        {
            case 1:
                dont.text = "Are you sure?";
                dont.color = Color.red;
                break;
            case 2:
                dont.text = "Think again, seriously.";
                dont.color = Color.red;
                break;
            case 3:
                dont.text = "Wish you having a long journey.";
                dont.color = Color.red;
                break;
        }

        if (suicidecount > 3)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().BGM.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }

    private void OnApplicationQuit()
    {
            Application.CancelQuit();
    }


}

