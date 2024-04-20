using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        public Sprite def;
        GameObject Deck1;
        GameObject Deck2;
        Player P1;
        Player P2;
        public int counter;
        public TextMeshProUGUI RoundEd;
        TextMeshProUGUI roundEdInstance;

        Vector3 newPositionI;
        Quaternion newRotationI;
        GameObject ObjectP;
        Vector3 ZoomPositionI;
        BoardPoints Crow;
        BoardPoints Sun;
        bool gotawinner;

        public TMPro.TextMeshProUGUI Winround;
        void Start()
        {
            newPositionI = MainCamera1.transform.position;
            newRotationI = MainCamera1.transform.rotation;
            rectTransform = End.GetComponent<RectTransform>();
            Deck1 = GameObject.Find("SunDeck");
            Deck2 = GameObject.Find("CrowDeck");
            P1 = Deck1.GetComponent<Player>();
            P2 = Deck2.GetComponent<Player>();
            ObjectP = GameObject.Find("ZoomPos");
            GameObject zoomI = GameObject.Find("Zoom");
            ZoomPositionI = zoomI.transform.position;
            GameObject CObj = GameObject.Find("CPoint");
            GameObject SObj = GameObject.Find("SPoint");
            Crow = CObj.GetComponent<BoardPoints>();
            Sun = SObj.GetComponent<BoardPoints>();
        }

        public void Control()
        {
            int roundc = 0;
            if (!P1.alreadyplayed && !P2.alreadyplayed)
            {
                counter++;
            }
            if (counter == 2)
            {
                RoundEnd = true;
                GameObject canvas = GameObject.Find("Canvas");
                roundEdInstance = Instantiate(RoundEd, canvas.transform, false);
                roundc ++;

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
                GameObject zoom = GameObject.Find("Zoom");
                zoom.transform.position = ObjectP.transform.position;
                zoom.transform.localScale = new Vector3(0.4f, 0.6f, 0.2f);
                zoom.transform.rotation = Quaternion.Euler(0, 0, 180);

                // GameObject PointC = GameObject.Find("PointTextC");
                //PointC.transform.localPosition = new Vector3(-30.8f, 4.5f , -1.8f);
                // PointC.transform.rotation = Quaternion.Euler(0,0,180);

                if (RoundEnd)
                {
                    //RoundEd.transform.rotation = Quaternion.Euler(0, 0, 0);
                    if (Crow.sum > Sun.sum)
                    {
                        Winround.text = "Cuervos ganan";
                    }
                    else
                    {
                        Winround.text = "Cuervos pierden";
                    }
                    Winround.transform.localScale = new Vector3(-13.2f, -4.7f, -1.8f);
                }
                CrTurn = true;
            }
            else
            {
                Debug.Log("Entraste al else");
                MainCamera1.transform.position = newPositionI;
                MainCamera1.transform.rotation = newRotationI;
                rectTransform.rotation = Quaternion.Euler(0, 0, 0);
                GameObject zoom = GameObject.Find("Zoom");
                zoom.transform.position = ZoomPositionI;
                zoom.transform.rotation = Quaternion.Euler(0, 0, 0);
                /*GameObject PointC = GameObject.Find("PointTextC");
                PointC.transform.localPosition = new Vector3(18.4f, -10.6f , -1.8f);
                PointC.transform.rotation = Quaternion.Euler(0,0,0);
                */
                if (RoundEnd)
                {
                    if (RoundEnd)
                    {
                       // RoundEd.transform.rotation = Quaternion.Euler(180, 180, 0);
                        if (Crow.sum > Sun.sum)
                        {
                            Winround.text = "Cuervos ganan";
                        }
                        else
                        {
                            Winround.text = "Cuervos pierden";
                        }
                        Winround.transform.localScale = new Vector3(42.6f, -122f, -0.7f);

                    }
                }
                CrTurn = false;
            }
        }

    }
}
