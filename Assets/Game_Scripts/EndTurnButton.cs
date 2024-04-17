using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GwentPro
{
    public class EndTurnButton : MonoBehaviour
    {
        public bool RoundEnd;
        public bool CrTurn;
        public Button End;
        public Camera MainCamera1;
        public Camera MainCamera2;
        public GameObject ButtonContainer;
        private RectTransform rectTransform;
        GameObject Deck1;
        GameObject Deck2;
        Player P1;
        Player P2;
        public int counter;

        Vector3 newPositionI;
        Quaternion newRotationI;

        void Start()
        {
            newPositionI = MainCamera1.transform.position;
            newRotationI = MainCamera1.transform.rotation;
            rectTransform = End.GetComponent<RectTransform>();
            Deck1 = GameObject.Find("SunDeck");
            Deck2 = GameObject.Find("CrowDeck");
            P1 = Deck1.GetComponent<Player>();
            P2 = Deck2.GetComponent<Player>();
        }

        public void Control()
        {
            if(!P1.alreadyplayed && !P2.alreadyplayed)
            {
                counter++;
            }
            if(counter == 2)
            {
               RoundEnd = true;
            }
            
            P1.alreadyplayed = false;
            P2.alreadyplayed = false;
            if (!CrTurn)
            {
                Vector3 newPosition = MainCamera2.transform.position;
                Quaternion newRotation = MainCamera2.transform.rotation;

                MainCamera1.transform.position = newPosition;

                // Cambia la rotación de la cámara a la nueva rotación
                MainCamera1.transform.rotation = newRotation;
                rectTransform.rotation = Quaternion.Euler(0, 0, 180);

                CrTurn = true;
            }
            else
            {
                Debug.Log("Entraste al else");
                MainCamera1.transform.position = newPositionI;
                MainCamera1.transform.rotation = newRotationI;
                rectTransform.rotation = Quaternion.Euler(0, 0, 0);
                CrTurn = false;
            }
        }

    }
}
