using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuStart : MonoBehaviour
{
    Text text;

    [SerializeField]
    private Color hoverEnterColor;

    [SerializeField]
    private Color hoverExitColor;

    private Color currentColor;

    public FMOD.Studio.EventInstance BGM;

    private void Start()
    {
        text = transform.GetComponent<Text>();
        currentColor = hoverExitColor;

        BGM = FMODUnity.RuntimeManager.CreateInstance("event:/Menu");
        BGM.start();
    }

    public void OnHoverEnter()
    {
        currentColor = hoverEnterColor;
    }

    public void OnHoverExit()
    {
        currentColor = hoverExitColor;
    }

    public void onClickStart()
    {
        BGM.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    private void Update()
    {
        text.color = Color.Lerp(text.color, currentColor, 0.1f);
    }
}
