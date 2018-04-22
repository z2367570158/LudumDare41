using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuQuit : MonoBehaviour
{
    Text text;

    [SerializeField]
    private Color hoverEnterColor;

    [SerializeField]
    private Color hoverExitColor;

    private Color currentColor;


    private void Start()
    {
        text = transform.GetComponent<Text>();
        currentColor = hoverExitColor;
    }

    public void OnHoverEnter()
    {
        currentColor = hoverEnterColor;
    }

    public void OnHoverExit()
    {
        currentColor = hoverExitColor;
    }

    public void onClickQuit()
    {
        Application.Quit();
    }
    private void Update()
    {
        text.color = Color.Lerp(text.color, currentColor, 0.1f);
    }
}
