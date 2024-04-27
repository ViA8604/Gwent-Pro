using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace GwentPro
{
    public class CemeteryB : MonoBehaviour
    {
        public GameObject Board;
        GameObject End;
        EndTurnButton button;
        bool RoundEnd;
        // Start is called before the first frame update
        void Start()
        {
            Board = GameObject.Find("Board");
            End = GameObject.Find("EndTurnButton");
            button = End.GetComponent<EndTurnButton>();
        }

        // Update is called once per frame
        void Update()
        {
            RoundEnd = button.RoundEnd;
            if(RoundEnd)
            {
            for (int i = 0; i < Board.transform.childCount; i++)
            {
                GameObject panel = Board.transform.GetChild(i).gameObject;
                if(panel.tag == gameObject.tag)
                {
                     for (int j = 0; j < panel.transform.childCount; j++)
                     { 
                        GameObject card = panel.transform.GetChild(j).gameObject;
                        card.transform.SetParent(gameObject.transform, false);
                     }
                }
            }
            }
            if(gameObject.transform.childCount != 0)
            {
                HorizontalLayoutGroup horizontal = gameObject.GetComponent<HorizontalLayoutGroup>();
                for(int i = 0; i < gameObject.transform.childCount; i++)
                {
                    GameObject card = gameObject.transform.GetChild(i).gameObject;
                    CardClass cardClass = card.GetComponent<CardClass>();
                    if(cardClass.crdtype == CardClass.cardtype.Special)
                    {
                        
                        horizontal.childControlHeight = enabled;
                        horizontal.childScaleWidth = enabled;
                    }
                    else
                    {
                        horizontal.childControlHeight = !enabled;
                        horizontal.childScaleWidth = !enabled;
                    }
                }
            }
        }
    }
}
