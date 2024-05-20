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
    public GameObject lastHitObject;
    public bool nodrag;
    public bool checkselected;
    public GameObject CardTocheck;
    public bool wasselected;
    public bool changedgravity;
    bool gravityeliminated;
    public GameObject selectedcard;

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
        if (cmbtype == CardClass.combatype.Special)
        {
            if (CrTurn)
            {
                gameObject.tag = "Suns";
            }
            else
            {
                gameObject.tag = "Crows";
            }
        }

        if (checkselected)
        {
            wasselected = PeekCardInvoked(CardTocheck);
            if (wasselected)
            {
                selectedcard = CardTocheck;
                string decktofind = "";
                if (gameObject.tag == "Crows")
                {
                    decktofind = "CrowDeck";
                }
                else
                {
                    decktofind = "SunDeck";
                }
                Player player = GameObject.Find(decktofind).GetComponent<Player>();
                player.selectedcard = CardTocheck;
                checkselected = false;
            }
        }
        if (!changedgravity && gravityeliminated)
        {
            RestoreGravity();
            gravityeliminated = false;
        }
    }

    void OnMouseDown()
    {
        if (gameObject.name != "WeatherZone")
        {
            string decktofind = "";
            if (gameObject.tag == "Crows")
            {
                decktofind = "CrowPanel";
            }
            else
            {
                decktofind = "SunPanel";
            }
            {
                /*Si das click en el panel y hay una carta tipo señuelo activa, desactiva la gravedad y comprueba
                si seleccionas alguna carta del panel.*/
                GameObject yourhand = GameObject.Find(decktofind);
                for (int i = 0; i < yourhand.transform.childCount; i++)
                {
                    GameObject card = yourhand.transform.GetChild(i).gameObject;
                    CardClass cardhand = card.GetComponent<CardClass>();
                    if (cardhand.selected && cardhand.cmbtype == CardClass.combatype.Lure)
                    {
                        Rigidbody2D gravity = gameObject.GetComponent<Rigidbody2D>();
                        Destroy(gravity);
                        changedgravity = true;
                        gravityeliminated = true;

                        for (int j = 0; j < gameObject.transform.childCount; j++)
                        {
                            GameObject intopanel = gameObject.transform.GetChild(j).gameObject;

                            CardClass seekselected = intopanel.GetComponent<CardClass>();

                            nodrag = true;
                            seekselected.nodrag = nodrag;
                            checkselected = true;
                            CardTocheck = intopanel;
                        }
                    }
                }
            }
        }
    }

    public static bool PeekCardInvoked(GameObject cardtocheck)
    {
        CardClass cardcomp = cardtocheck.GetComponent<CardClass>();
        if (cardcomp.selected == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void RestoreGravity()
    {
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.simulated = true;
        rb.gravityScale = 0;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.sleepMode = RigidbodySleepMode2D.StartAwake;

        rb.constraints =
            RigidbodyConstraints2D.FreezePositionX
            | RigidbodyConstraints2D.FreezePositionY
            | RigidbodyConstraints2D.FreezeRotation;
    }
}
