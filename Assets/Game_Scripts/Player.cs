using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GwentPro;
using JetBrains.Annotations;
using UnityEngine.UI;

namespace GwentPro
{
    public class Player : MonoBehaviour
    {
        public bool alreadyplayed;
        public List<GameObject> mazo1;
        public int[] indexcard;
        public GameObject CrLeader;
        public GameObject SuLeader;
        CardClass crowl;
        CardClass sunls;
        public List<GameObject> yourhand1;
        public Transform HandPanel;
        private Vector3 originalPosition; // Para guardar la posición original

        public Camera MainCamera1;
        bool RoundEnd;
        EndTurnButton buttom;
        // Start is called before the first frame update
        void Start()
        {
            if (gameObject.tag == "Deck1")
            {
                CrLeader = Instantiate(CrLeader, new Vector3(952.6099f, 534.9073f, 0), Quaternion.identity);
                crowl = CrLeader.GetComponent<CardClass>();
                SuLeader = Instantiate(SuLeader, new Vector3(952.53f, 540.28f, 0), Quaternion.identity);
                SuLeader.transform.rotation = Quaternion.Euler(180, 180, 0);
                sunls = SuLeader.GetComponent<CardClass>();

            }
            
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
        
           /* indexcard = new int[10];
            for (int i = 0; i < indexcard.Length; i++)
            {
                indexcard[i] = i;
                GameObject card = Instantiate(mazo1[indexcard[i]], new Vector3(0, 0, 0), Quaternion.identity);
                card.transform.SetParent(HandPanel, false);
                yourhand1.Insert(i, card);

            }
            */
        }




        // Update is called once per frame
        void Update()
        {

            /*
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
            */
            for (int i = 0; i < yourhand1.Count; i++)
            {
                CardClass card = yourhand1[i].GetComponent<CardClass>();
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
                            card.ActualZone = actualzone;
                            alreadyplayed = true;
                        }
                        else
                        {
                            if (card.startposition != card.draggedposition)
                            {
                                HorizontalLayoutGroup layoutGroup = HandPanel.GetComponent<HorizontalLayoutGroup>();
                                layoutGroup.enabled = false;

                                layoutGroup.enabled = true;
                                card.draggedposition = card.startposition;
                                Vector3 pos = card.transform.position;
                                pos.z = 0;
                                card.transform.position = pos;

                            }
                        }
                    }
                    else
                    {
                        if (card.startposition != card.draggedposition)
                        {
                            HorizontalLayoutGroup layoutGroup = HandPanel.GetComponent<HorizontalLayoutGroup>();
                            layoutGroup.enabled = false;

                            layoutGroup.enabled = true;
                            card.draggedposition = card.startposition;
                            Vector3 pos = card.transform.position;
                            pos.z = 0;
                            card.transform.position = pos;

                        }
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