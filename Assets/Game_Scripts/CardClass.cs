using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GwentPro
{
    public class CardClass : MonoBehaviour
    {
        public string cardname;
        public int cardpoint;
        public cardtype crdtype;
        public bool selected;
        public combatype cmbtype;

        private Material material;
        private Vector3 screenPoint;
        private Vector3 offset;
        public bool isdragging;

        public string ActualScene;

        public GameObject ActualZone;
        public bool CrTurn;
        public bool playable;
        public bool alreadyplayed;
        Player yourP;
        EndTurnButton end;

        public Sprite def;


        void Start()
        {
            ActualZone = null;
            material = GetComponent<Renderer>().material;
            ActualScene = gameObject.scene.name;
            if(ActualScene == "GameTable")
            {
            GameObject EndB = GameObject.Find("EndTurnButton");
            end = EndB.GetComponent<EndTurnButton>();
            def = end.def;
            string tagtofind;
            if(gameObject.tag == "Crows")
            {
                tagtofind = "Deck1";
            }
            else
            {
                tagtofind = "Deck2";
            }
            GameObject CardDeck = GameObject.FindGameObjectWithTag(tagtofind);
            yourP = CardDeck.GetComponent<Player>();
            }
            //ARREGLAR ESTO
            if (cmbtype == combatype.Special)
            {
                if (ActualScene == "RedrawSuns")
                {
                    gameObject.tag = "Suns";
                }
                else gameObject.tag = "Crows";
            }
        }

        void Update()
        {
            if(ActualScene == "GameTable")
            {
            CrTurn = end.CrTurn;
            playable = Playable(CrTurn , gameObject.tag);

            alreadyplayed = yourP.alreadyplayed;

            }
            Camera camera = Camera.main;
            if (camera == null)
            {
                Debug.LogError("No main camera found.");
                return;
            }
            //Asignando el color del borde a cada carta
            if (gameObject.tag == "Crows")
            {
                if (selected)
                {
                    Color bluecool = new Color(28f / 255f, 42f / 255f, 71f / 255f);
                    material.SetColor("_BorderColor", bluecool);
                    screenPoint = camera.WorldToScreenPoint(gameObject.transform.position);
                    offset = gameObject.transform.position - camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
                }
                else
                {
                    Color bluetone = new Color(12f / 255f, 35f / 255f, 179f / 255f);
                    material.SetColor("_BorderColor", bluetone);
                }
            }
            else
            {
                if (selected)
                {
                    material.SetColor("_BorderColor", Color.black);
                    screenPoint = camera.WorldToScreenPoint(gameObject.transform.position);
                    offset = gameObject.transform.position - camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
                }
                else
                {
                    Color greencool = new Color(5f / 255f, 153f / 255f, 54f / 255f);
                    material.SetColor("_BorderColor", greencool);
                }
            }
            
        }

        void OnMouseEnter()
        {
            if(CrTurn)
            {
                GameObject.Find("Zoom").GetComponent<Image>().sprite = def;
            }
            GameObject.Find("Zoom").GetComponent<Image>().sprite=gameObject.GetComponent<Image>().sprite;
            
        }


        [System.Serializable]
        public enum combatype
        {
            Melee,
            Range,
            Siege,
            Special,
            None
        }

        [System.Serializable]
        public enum cardtype
        {
            Leader,
            Gold,
            Silver,
            Special
        }

        void OnMouseDown()
        {
            selected = !selected;
        }
        void OnMouseDrag()
        {
            if (ActualScene == "RedrawScene" || ActualScene == "RedrawSuns" || !selected || !playable || alreadyplayed)
            {
                isdragging = false;
                return;
            }
            else
            {
                GameObject Camera = GameObject.Find("EndTurnButton");
                EndTurnButton SelectC = Camera.GetComponent<EndTurnButton>();
                Camera activeCamera;
                isdragging = true;
                // Al arrastrar el mouse, se calcula la nueva posición del objeto
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

                // Aquí usamos la cámara activa
                    activeCamera = SelectC.MainCamera1;/* inserta aquí tu código para obtener la cámara activa */;

                // Convertimos la posición del ratón en la pantalla a la posición en el mundo
                Vector3 curPosition = activeCamera.ScreenToWorldPoint(curScreenPoint) + offset;

                // Actualizamos la posición del objeto
                transform.position = curPosition;
            }
        }

        void OnMouseUp()
        {
            if (isdragging)
            {
                isdragging = false;
                selected = false;
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            // Verifica si el objeto con el que estás colisionando tiene un HorizontalLayoutGroup
            HorizontalLayoutGroup layoutGroup = collision.gameObject.GetComponent<HorizontalLayoutGroup>();
            if (layoutGroup != null && layoutGroup.tag == gameObject.tag)
            {
                // Si el objeto tiene un HorizontalLayoutGroup, lo guarda y lo organiza dentro del HorizontalLayoutGroup
                ActualZone = collision.gameObject;

            }
        }
        void OnCollisionStay2D(Collision2D collision)
        {
            HorizontalLayoutGroup layoutGroup = collision.gameObject.GetComponent<HorizontalLayoutGroup>();
            if (layoutGroup != null && layoutGroup.tag == gameObject.tag)
            {
                // Si el objeto tiene un HorizontalLayoutGroup, lo guarda y lo organiza dentro del HorizontalLayoutGroup
                ActualZone = collision.gameObject;

            }
        }
        void OnCollisionExit2D(Collision2D collision)
        {
            ActualZone = null;
        }

        static bool Playable (bool CrTurn, string cardtag)
        {
            if (CrTurn && cardtag == "Crows")
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }
    }

}
