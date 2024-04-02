using System.Collections;
using System.Collections.Generic;
using GwentPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

namespace GwentPro
{
    public class ChooseButton : MonoBehaviour
    {

        public List<GameObject> mazo11;
        public List<GameObject> inHand11;
        public Dictionary<GameObject, int> toDestroy11;
        public GameObject inicialDeckObject;
        public int[] indexcard = new int[10];

        // Start se llama antes del primer frame
        void Start()
        {
            //Asigna todos los elementos del script del Deck
            inicialDeckObject = GameObject.Find("InicialDeck");
            if (inicialDeckObject != null)
            {
                Deck deckInstance = inicialDeckObject.GetComponent<Deck>();
                mazo11 = deckInstance.mazo1;
                inHand11 = deckInstance.inHand1;
                toDestroy11 = deckInstance.toDestroy;
            }
        }
        // Update is called once per frame
        void Update()
        {

        }

        public void UpData()
        {
            UpdateCards(toDestroy11, inHand11, mazo11, indexcard);
        }
        public void UpdateCards(Dictionary<GameObject, int> toDestroy11, List<GameObject> inHand11, List<GameObject> mazo11, int[] indexcard)
        {
            /* Hace el cambio de cartas a partir del diccionario
            Hay que ver bien lo de seleccionar y deseleccionar después*/
            int plus = 0;
            foreach (GameObject item in toDestroy11.Keys)
            {
                foreach (int value in toDestroy11.Values)
                {
                    if (item != null && Deck.PeekCardInvoked(item))
                    {
                        Vector3 getpos = inHand11[value].transform.position;
                        Destroy(inHand11[value]);
                        plus++;
                        FillIndex(indexcard, toDestroy11);
                        inHand11[value] = Instantiate(mazo11[9 + plus], getpos, Quaternion.identity);
                        Deck.ResizeInstance(inHand11[value]);
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }



        public void FillIndex(int[] indexcard, Dictionary<GameObject, int> todestroy)
        {
            /*Lleva un array con las posiciones directas al mazo de las cartas
            que invocaste y que destruiste*/
            for (int i = 0; i < indexcard.Length; i++)
            {
                indexcard[i] = i;
            }

            if (todestroy != null)
            {
                int count = todestroy.Count;
                if (count > 0 && count <= 2)
                {
                    var values = todestroy.Values.ToList();
                    indexcard[values[0]] = 10;
                    if (count == 2)
                    {
                        indexcard[values[1]] = 11;
                    }
                }
            }
        }

        public void NextScene()
        {
            StartCoroutine(WaitAndPrint(40.0f));
            FillIndex(indexcard,toDestroy11);
            SceneManager.LoadScene("GameTable");
            DontDestroyOnLoad(gameObject);
        }

        IEnumerator WaitAndPrint(float waitime)
        {
            yield return new WaitForSeconds(waitime);
        }
    }
}