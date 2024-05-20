using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using GwentPro;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class SideEfCard : MonoBehaviour
{
    public string cardname;
    public string decktoaffect;
    GameObject Board;
    int pointstoadd = 0;
    CardClass card;
    public bool pointAdded = false;
    public GameObject weatherpanel;

    // Start is called before the first frame update
    void Start()
    {
        weatherpanel = GameObject.Find("WeatherZone");
        card = gameObject.GetComponent<CardClass>();
        cardname = card.cardname;
        pointAdded = false;
        pointstoadd = card.cardpoint;
        Board = GameObject.Find("Board");
        if (gameObject.transform.tag == "Suns")
        {
            decktoaffect = "Crows";
        }
        else
        {
            decktoaffect = "Suns";
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.scene.name == "GameTable")
        {
            if (gameObject.transform.parent != null && gameObject.transform.parent.name.Contains("IncreaseZone") || gameObject.transform.parent.name.Contains("WeatherZone"))
            {
                if (!pointAdded)
                {
                    switch (cardname)
                    {
                        case "Frente Frío":
                        case "Arbol Sagrado":
                        case "Fiesta Reloca":
                        case "Café de estudio":
                        case "Estatua Bendita":
                            for (int i = 0; i < Board.transform.childCount; i++)
                            {
                                GameObject panel = Board.transform.GetChild(i).gameObject;
                                if (panel.tag == gameObject.tag)
                                {
                                    for (int j = 0; j < panel.transform.childCount; j++)
                                    {
                                        GameObject card = panel.transform.GetChild(j).gameObject;
                                        CardClass cardClass = card.GetComponent<CardClass>();
                                        if (cardClass.cmbtype == CardClass.combatype.Melee && cardClass.crdtype != CardClass.cardtype.Gold )
                                        {
                                            cardClass.cardpoint += pointstoadd;
                                            pointAdded = true;
                                        }
                                    }

                                }
                                
                            }
                            break;
                        case "Ciclón":
                            for (int i = 0; i < Board.transform.childCount; i++)
                            {
                                GameObject panel = Board.transform.GetChild(i).gameObject;
                                if (panel.tag == decktoaffect)
                                {
                                    for (int j = 0; j < panel.transform.childCount; j++)
                                    {
                                        GameObject card = panel.transform.GetChild(j).gameObject;
                                        CardClass cardClass = card.GetComponent<CardClass>();
                                        if (cardClass.cmbtype == CardClass.combatype.Melee && cardClass.crdtype != CardClass.cardtype.Gold)
                                            {
                                            cardClass.cardpoint -= pointstoadd;
                                        pointAdded = true;
                                            }
                                    }
                                }
                            }
                            break;
                        case "Polvo del Sahara":
                            for (int i = 0; i < Board.transform.childCount; i++)
                            {
                                GameObject panel = Board.transform.GetChild(i).gameObject;
                                if (panel.tag == decktoaffect)
                                {
                                    for (int j = 0; j < panel.transform.childCount; j++)
                                    {
                                            GameObject card = panel.transform.GetChild(j).gameObject;
                                            CardClass cardClass = card.GetComponent<CardClass>();
                                            if (cardClass.cmbtype == CardClass.combatype.Range && cardClass.crdtype != CardClass.cardtype.Gold)
                                            {
                                                cardClass.cardpoint -= pointstoadd;

                                            }
                                        }
                                    }
                                }
                            
                            break;
                        case "Tormenta":
                            for (int i = 0; i < Board.transform.childCount; i++)
                            {
                                GameObject panel = Board.transform.GetChild(i).gameObject;
                                if (panel.tag == decktoaffect)
                                {
                                    for (int j = 0; j < panel.transform.childCount; j++)
                                    {
                                        GameObject card = panel.transform.GetChild(j).gameObject;
                                        CardClass cardClass = card.GetComponent<CardClass>();
                                        if (cardClass.cmbtype == CardClass.combatype.Siege && cardClass.crdtype != CardClass.cardtype.Gold)
                                        {
                                            cardClass.cardpoint -= pointstoadd;
                                            pointAdded = true;
                                        }
                                    }
                                }
                            }
                            break;

                        case "Cielos Despejados":

                            for (int j = 0; j < weatherpanel.transform.childCount; j++)
                            {
                                GameObject card = weatherpanel.transform.GetChild(j).gameObject;
                                if (card.tag == "Crows")
                                {
                                    GameObject Grave = GameObject.Find("CemeteryC");
                                    card.transform.SetParent(Grave.transform, false);
                                }
                                else
                                {
                                    GameObject Grave = GameObject.Find("CemeteryS");
                                    card.transform.SetParent(Grave.transform, false);
                                }
                            }

                            break;
                    }
                }
                    pointAdded = false;

            }

        }

    }
}