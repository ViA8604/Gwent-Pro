using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GwentPro;
using JetBrains.Annotations;

namespace GwentPro
{
    public class Player : MonoBehaviour
    {
        public bool alreadyplayed;
        public List<GameObject> mazo1;
        public int[] indexcard;
        public List<GameObject> yourhand1;
        public Transform HandPanel;
        private Vector3 originalPosition; // Para guardar la posición original

        public Camera MainCamera1;
        bool RoundEnd;
        EndTurnButton buttom;
        // Start is called before the first frame update
        void Start()
        {
            GameObject endbutton = GameObject.Find("EndTurnButton");
            buttom = endbutton.GetComponent<EndTurnButton>();
            if (gameObject.tag == "Deck2")
            {
                GameObject sdeck = GameObject.FindGameObjectWithTag("Suns");
                ChooseButton choosesun = sdeck.GetComponent<ChooseButton>();
                mazo1 = choosesun.mazo11;
                indexcard = choosesun.indexcard;
                for (int i = 0; i < indexcard.Length; i++)
                {
                    GameObject card = Instantiate(mazo1[indexcard[i]], new Vector3(0, 0, 0), Quaternion.identity);
                    card.transform.SetParent(HandPanel, false);
                    yourhand1.Insert(i, card);
                }
            }
            else
            {
                GameObject cdeck = GameObject.FindGameObjectWithTag("Crows");
                ChooseButton choosecrow = cdeck.GetComponent<ChooseButton>();
                mazo1 = choosecrow.mazo11;
                indexcard = choosecrow.indexcard;
                for (int i = 0; i < indexcard.Length; i++)
                {
                    GameObject card = Instantiate(mazo1[indexcard[i]], new Vector3(0, 0, 0), Quaternion.identity);
                    card.transform.SetParent(HandPanel, false);
                    yourhand1.Insert(i, card);

                }
            }
        }

        /*indexcard = new int[10];
          for (int i = 0; i < indexcard.Length; i++)
          {
              indexcard[i] = i;
              GameObject card = Instantiate(mazo1[indexcard[i]], new Vector3(0, 0, 0), Quaternion.identity);
              card.transform.SetParent(HandPanel, false);
              yourhand1.Insert(i, card);

          }
          */




        // Update is called once per frame
        void Update()
        {
            RoundEnd = buttom.RoundEnd;
            if (RoundEnd)
            {
                int adding = 15;
                if (gameObject.tag == "Deck2")
                {
                    GameObject DeckC = GameObject.Find("CrowDeck");
                    Player playerC = DeckC.GetComponent<Player>();
                    for (int i = adding; i < adding + 2; i++)
                    {
                        Instantiate(mazo1[i], new Vector3(0, 0, 0), Quaternion.identity);
                        yourhand1.Add(mazo1[i]);
                    }
                }
                else
                    {
                        GameObject DeckS = GameObject.Find("SunDeck");
                        Player playerS = DeckS.GetComponent<Player>();
                        for (int i = adding; i < adding + 2; i++)
                    {
                        Instantiate(mazo1[i], new Vector3(0, 0, 0), Quaternion.identity);
                        yourhand1.Add(mazo1[i]);
                    }
                    }

                    
                }
                
                for (int i = 0; i < yourhand1.Count; i++)
                {
                    CardClass card = yourhand1[i].GetComponent<CardClass>(); // Consigues el componente
                    CardClass.combatype fightype = card.cmbtype;
                    GameObject actualzone = card.ActualZone;  // Guardas la zona actual por la que va el objeto
                    if (card.isdragging)
                    {
                        continue;
                    }
                    else
                    {
                        if (actualzone != null)
                        {
                            CombatZone panel = actualzone.GetComponent<CombatZone>();   // Consigues el panel de por donde va el objeto
                            if (fightype == panel.cmbtype && card.tag == panel.tag)
                            {
                                yourhand1[i].transform.SetParent(actualzone.transform, false);
                                alreadyplayed = true;
                            }
                            else
                            {
                                if (card.transform.parent.tag.Contains("Deck"))
                                {
                                    card.transform.SetParent(HandPanel, false);
                                }
                            }
                        }
                        else
                        {
                          
                        }
                    }
                }
            }


            public static bool PeekCardDragging(GameObject cardtocheck)
            {
                CardClass cardcomp = cardtocheck.GetComponent<CardClass>(); //OHE
                if (cardcomp.isdragging == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }