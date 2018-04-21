using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnHoverText : MonoBehaviour
{
    Text text;
    [SerializeField] Text dont;
    public bool canClose = false;
    int suicidecount = 0;

    private void Awake()
    {
        text = transform.GetComponent<Text>();
    }

    public void OnClick()
    {
        suicidecount++;
        text.color = new Color(1, 1f- (float)suicidecount / 5, 1f- (float)suicidecount / 5);

        switch(suicidecount)
        {
            case 1:
                dont.text = "Please Don't";
                dont.color = Color.white;
                break;
            case 2:
                dont.text = "You Can Make It";
                dont.color = Color.white;
                break;
            case 3:
                dont.text = "You're Not Alone";
                dont.color = Color.white;
                break;
            case 4:
                dont.text = "Still Not Too Late";
                dont.color = Color.white;
                break;
        }

        if (suicidecount > 5)
        {
            Application.Quit();
            canClose = true;
        }
    }

    private void OnApplicationQuit()
    {
        if (!canClose)
            Application.CancelQuit();
    }


}

