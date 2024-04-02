using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GwentPro;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace GwentPro
{
    public class Deck : MonoBehaviour
    {
        public List<GameObject> mazo1 = new List<GameObject>();
        public List<GameObject> inHand1 = new List<GameObject>();
        public Dictionary<GameObject, int> toDestroy = new Dictionary<GameObject, int>();

        bool needtoRenew = false;

        // Start is called before the first frame update
        void Start()
        {
            needtoRenew = BeginRound(mazo1, inHand1); //esta talla devuelve siempre true
        }

        // Update is called once per frame
        void Update()
        {
            if (needtoRenew == true)
            {
                needtoRenew = RenewInicialCards(inHand1, toDestroy); //esta talla devuelve siempre false
            }
            foreach (GameObject item in toDestroy.Keys)
            {
                foreach (int valor in toDestroy.Values)
                {
                    Debug.Log(item + "," + valor);
                }
            }
        }

        static bool BeginRound(List<GameObject> mazo1, List<GameObject> inHand1)
        {
            //Empieza la ronda para el mazo actual
            Shuffle(mazo1);
            PositionateHand(mazo1, "CrowRedraw", inHand1);
            Debug.Log("necesidad de renovar en true");
            return true;
        }

        static void Shuffle(List<GameObject> mazo1)
        {
            System.Random alt = new System.Random();
            int n = mazo1.Count;
            while (n > 1)
            {
                n--;
                int chngednum = alt.Next(n + 1);
                GameObject valor = mazo1[chngednum];
                mazo1[chngednum] = mazo1[n];
                mazo1[n] = valor;
            }
        }

        public static void PositionateHand(List<GameObject> mazo, string tag, List<GameObject> inHand1)
        {
            GameObject[] posHand = new GameObject[10];
            posHand = GameObject.FindGameObjectsWithTag(tag);
            for (int i = 0; i < posHand.Length; i++)
            {
                Vector3 prueba = posHand[i].transform.position;
                GameObject instance = Instantiate(mazo[i], prueba, Quaternion.identity);
                inHand1.Insert(i, instance);
                ResizeInstance(instance);

            }
        }

        public static bool RenewInicialCards(List<GameObject> inHand1, Dictionary<GameObject, int> toDestroy)
        {
            int counter = 0;
            int handLe = inHand1.Count;
            for (int i = 0; i < handLe; i++)
            {
                if (PeekCardInvoked(inHand1[i]) && counter < 3)
                {
                    if (!toDestroy.ContainsKey(inHand1[i]))
                    {
                        AddToDestroy(toDestroy, inHand1[i], i);

                        counter++;
                    }
                    else break;
                }

            }

            Debug.Log("Carta no seleccionada");
            return true;
        }

        public static bool PeekCardInvoked(GameObject cardtocheck)
        {
            Debug.Log("Entra a comprobar si hay carta con selected");
            CardClass cardcomp = cardtocheck.GetComponent<CardClass>(); //OHE
            if (cardcomp.selected == true)
            {
                Debug.Log("selected en true");
                return true;
            }
            else
            {
                Debug.Log("selected en false");
                return false;
            }
        }

        public static void AddToDestroy(Dictionary<GameObject, int> toDestroy, GameObject item, int value)
        {
            if (toDestroy.Count == 2)
            {
                var oldestKey = GetOldestKey(toDestroy);
                toDestroy.Remove(oldestKey);

            }

            // Agregar el nuevo elemento
            toDestroy[item] = value;

        }

        private static GameObject GetOldestKey(Dictionary<GameObject, int> dict)
        {
            foreach (var key in dict.Keys)
            {
                return key;
            }
            return null;
        }

        public static void ResizeInstance(GameObject instance)
        {
            CardClass checktype = instance.GetComponent<CardClass>();
            if (checktype.cmbtype == CardClass.combatype.Special)
            {
                // Reduce la escala a la mitad para el tipo especial
                instance.transform.localScale = new Vector3(0.12f, 0.2f, 0.2f);
            }
            else
            {
                // Reduce la escala a la mitad para los demás tipos
                instance.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            }
        }

    }
}