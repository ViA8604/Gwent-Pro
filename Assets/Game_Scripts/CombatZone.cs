using System.Collections;
using System.Collections.Generic;
using GwentPro;
using Unity.VisualScripting;
using UnityEngine;

public class CombatZone : MonoBehaviour
{
    public CardClass.combatype cmbtype;
    public Transform Panel;
    public bool CrTurn;
    EndTurnButton button;

    // Start is called before the first frame update
    void Start()
    {
        GameObject end = GameObject.Find("EndTurnButton");
        button = end.GetComponent<EndTurnButton>();
    }

    // Update is called once per frame
    void Update()
    {
        CrTurn = button.CrTurn;
        if(cmbtype == CardClass.combatype.Special)
        {
            if(CrTurn)
            {
                gameObject.tag = "Suns";
            }
            else
            {
                gameObject.tag = "Crows";
            }
        }
    }

 
}
