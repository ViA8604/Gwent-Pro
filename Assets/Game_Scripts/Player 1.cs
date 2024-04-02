using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GwentPro;
using JetBrains.Annotations;
public class Player1 : MonoBehaviour
{
    public List<GameObject> mazo1;
    public int[] indexcard;
    public List<GameObject> yourhand1;
    public Transform HandPanel;
    // Start is called before the first frame update
    void Start()
    {
        /*   ChooseButton deckInstance = GameObject.FindObjectOfType<ChooseButton>();

           mazo1 = deckInstance.mazo11;
           indexcard = deckInstance.indexcard;
           for (int i = 0; i < indexcard.Length; i++)
           {
              Instantiate(mazo1[indexcard[i]], HandPanel);
           }
       */
        // /*    
        indexcard = new int[10];
        for (int i = 0; i < indexcard.Length; i++)
        {
            indexcard[i] = i;
            GameObject card = Instantiate(mazo1[indexcard[i]], new Vector3(0, 0, 0), Quaternion.identity);
            card.transform.SetParent(HandPanel, false);
            yourhand1.Insert(i, card);

        }
        //    */
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < yourhand1.Count; i++)
        {
            CardClass combat = yourhand1[i].GetComponent<CardClass>();
            CardClass.combatype actual = combat.cmbtype;
            GameObject actualzone = combat.ActualZone;
            if(actualzone != null)
            {
            CombatZone panel = actualzone.GetComponent<CombatZone>();
            if (PeekCardDragging(yourhand1[i]) && (int)actual == (int)panel.cmbtype)
            {
                transform.SetParent(actualzone.transform, false);
            }
            }
        }
    }

    public static bool PeekCardDragging(GameObject cardtocheck)
    {
        Debug.Log("Entra a comprobar si hay carta con selected");
        CardClass cardcomp = cardtocheck.GetComponent<CardClass>(); //OHE
        if (cardcomp.isdragging == true)
        {
            Debug.Log("arrastrando en true");
            return true;
        }
        else
        {
            Debug.Log("arrastrando en false");
            return false;
        }
    }

}
