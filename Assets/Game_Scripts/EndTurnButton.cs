using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GwentPro
{
    public class EndTurnButton : MonoBehaviour
    {
        public bool pressedbutton = false;

        public void ButtonIsPressed()
        {
            pressedbutton = !pressedbutton;
            Debug.Log("Detecta que presionaste el boton");
        }
    }
}
