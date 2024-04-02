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
        public cardtype crdtype;
        public bool selected;
        public combatype cmbtype;

        private Material material;
        private Vector3 screenPoint;
        private Vector3 offset;
        public bool isdragging;

        public GameObject ActualZone;


        void Start()
        {
            ActualZone = null;
            material = GetComponent<Renderer>().material;
        }

        void Update()
        {
            if (selected)
            {
                Color bluecool = new Color(28f / 255f, 42f / 255f, 71f / 255f);

                material.SetColor("_BorderColor", bluecool);
                screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                // Calculamos la distancia entre el punto del ratón en la pantalla y la posición del objeto
                offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

            }
            else
            {
                material.SetColor("_BorderColor", Color.blue);
            }
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
            if (gameObject.scene.name == "RedrawScene") return;
            else
            {
                isdragging = !isdragging;
                // Mientras arrastramos el ratón, calculamos la nueva posición del objeto
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

                // Convertimos la posición del ratón en la pantalla a la posición en el mundo
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

                // Actualizamos la posición del objeto
                transform.position = curPosition;
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            // Verifica si el objeto con el que estás colisionando tiene un HorizontalLayoutGroup
            HorizontalLayoutGroup layoutGroup = collision.gameObject.GetComponent<HorizontalLayoutGroup>();
            if (layoutGroup != null)
            {
                // Si el objeto tiene un HorizontalLayoutGroup, lo guarda y lo organiza dentro del HorizontalLayoutGroup
                ActualZone= collision.gameObject;
                transform.SetParent(ActualZone.transform);
            }
        }
    }
}
