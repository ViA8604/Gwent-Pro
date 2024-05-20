using System.Collections;
using System.Collections.Generic;
using GwentPro;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        int roundc;
        public GameObject selectedcard = null;
        CardClass lurecard;

        // Start is called before the first frame update
        void Start()
        {
            if (gameObject.tag == "Deck1")
            {
                CrLeader = Instantiate(
                    CrLeader,
                    new Vector3(952.6099f, 534.9073f, 0),
                    Quaternion.identity
                );
                crowl = CrLeader.GetComponent<CardClass>();
                SuLeader = Instantiate(
                    SuLeader,
                    new Vector3(952.53f, 540.28f, 0),
                    Quaternion.identity
                );
                SuLeader.transform.rotation = Quaternion.Euler(180, 180, 0);
                sunls = SuLeader.GetComponent<CardClass>();
            }

            GameObject endbutton = GameObject.Find("EndTurnButton");
            buttom = endbutton.GetComponent<EndTurnButton>();
        /*
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
           */
           indexcard = new int[10];
            for (int i = 0; i < indexcard.Length; i++)
            {
                indexcard[i] = i;
                GameObject card = Instantiate(
                    mazo1[indexcard[i]],
                    new Vector3(0, 0, 0),
                    Quaternion.identity
                );
                card.transform.SetParent(HandPanel, false);
                yourhand1.Insert(i, card);
            }
        
        }

        // Update is called once per frame
        void Update()
        {
            roundc = buttom.roundc;
            RoundEnd = buttom.RoundEnd;
            if (RoundEnd)
            {
                StartCoroutine(NextRound());
                buttom.waitRedr++;
                if (buttom.waitRedr == 2)
                {
                    buttom.RoundEnd = false;
                    buttom.waitRedr = 0;
                }
            }
            for (int i = 0; i < yourhand1.Count; i++)
            {
                CardClass card = yourhand1[i].GetComponent<CardClass>();
                CardClass.combatype fightype = card.cmbtype;
                GameObject actualzone = card.ActualZone; // Guardas la zona actual por la que va el objeto
                if (card.isdragging)
                {
                    continue;
                }
                else
                {
                    if (card.cmbtype == CardClass.combatype.Lure && card.selected)
                    {
                        lurecard = card;
                            if (selectedcard != null)
                            {
                                string parent = selectedcard.transform.parent.name;
                                if (
                                    parent != HandPanel.name
                                    && selectedcard.transform.parent.gameObject != null
                                )
                                {
                                    selectedcard
                                        .transform.parent.gameObject.GetComponent<CombatZone>()
                                        .changedgravity = false;
                                }
                                card.transform.SetParent(selectedcard.transform.parent, false);
                                selectedcard.transform.SetParent(HandPanel.transform, false);
                                selectedcard.GetComponent<CardClass>().nodrag = false;
                                selectedcard = null;
                                alreadyplayed = true;
                                Destroy(lurecard.LureinfoInstance);
                                lurecard = null;


                            }
                    }
                    
                    
                    if (actualzone != null)
                    {
                        CombatZone panel = actualzone.GetComponent<CombatZone>(); // Consigues el panel de por donde va el objeto
                        if(panel!= null)
                        {
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
                                HorizontalLayoutGroup layoutGroup =
                                    HandPanel.GetComponent<HorizontalLayoutGroup>();
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
                    else
                    {
                        if (card.startposition != card.draggedposition)
                        {
                            HorizontalLayoutGroup layoutGroup =
                                HandPanel.GetComponent<HorizontalLayoutGroup>();
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

        IEnumerator NextRound()
        {
            // Espera hasta el siguiente clic del mouse
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            TextMeshProUGUI endsign = GameObject
                .Find("round text(Clone)")
                .GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI winsign = GameObject
                .Find("win round(Clone)")
                .GetComponent<TextMeshProUGUI>();
            Destroy(endsign.gameObject);
            Destroy(winsign.gameObject);

            foreach (GameObject obj in yourhand1)
            {
                Destroy(obj);
            }
            yourhand1.Clear();

            int deckpos = 14;

            if (roundc == 2)
            {
                deckpos = 17;
            }
            int cardstoadd = 2;
            if (gameObject.tag == "Deck2")
            {
                cardstoadd += 1;
            }
            for (int i = deckpos; i < deckpos + cardstoadd; i++)
            {
                GameObject card = Instantiate(mazo1[i], new Vector3(0, 0, 0), Quaternion.identity);
                card.transform.SetParent(HandPanel, false);
                yourhand1.Add(card);
            }
        }
    }
}
