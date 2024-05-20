using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentPro;
using UnityEngine.UI;
public class EndGameC : MonoBehaviour
{
    int crowstreak;
    int sunstreak;
    public string wingame;
    EndTurnButton button;
    public GameObject crimage;
    public GameObject suimage;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.scene.name == "GameTable")
        {
            GameObject buttom = GameObject.Find("EndTurnButton");
            button = buttom.GetComponent<EndTurnButton>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.scene.name == "GameTable")
        {
            crowstreak = button.crowstreak;
            sunstreak = button.sunstreak;

            if (crowstreak >= sunstreak)
            {
                wingame = "Crows";
            }
            else
            {
                wingame = "Suns";
            }
        }
        if (gameObject.scene.name == "EndGameScene")
        {
            if (wingame == "Crows")
            {
                GameObject.Find("WinImage").GetComponent<SpriteRenderer>().sprite = crimage.GetComponent<SpriteRenderer>().sprite;

            }
            else
            {
                GameObject.Find("WinImage").GetComponent<SpriteRenderer>().sprite = suimage.GetComponent<SpriteRenderer>().sprite;

            }
        }
    }
}
