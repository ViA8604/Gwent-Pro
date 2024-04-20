using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GwentPro
{
    public class BoardPoints : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI Textob;
        public GameObject Board; // Referencia al tablero de juego
        public int sum;


        void Start()
        {
            Board = GameObject.Find("Board");
            Textob.text = "0"; // Inicializa el texto con "0"
        }


        void Update()
        {
            sum = 0; // Restablece la suma a cero al principio de cada actualización

            // Recorre todos los hijos del tablero
            for (int i = 0; i < Board.transform.childCount; i++)
            {
                GameObject panel = Board.transform.GetChild(i).gameObject; // Obtiene el i-ésimo hijo

                // Si el tag del panel coincide con el tag del objeto de juego al que está adjunto este script
                if (panel.tag == gameObject.tag)
                {

                    for (int j = 0; j < panel.transform.childCount; j++)
                    {
                        GameObject card = panel.transform.GetChild(j).gameObject; // Obtiene la j-ésima carta
                        CardClass cardClass = card.GetComponent<CardClass>(); // Obtiene el componente CardClass de la carta
                        sum += cardClass.cardpoint; // Suma los puntos de la carta a la suma total
                    }
                }
            }
            Textob.text = sum.ToString(); // Actualiza el texto para mostrar la suma total
        }
    }
}
