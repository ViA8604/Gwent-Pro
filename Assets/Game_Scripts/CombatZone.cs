using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombatZone : MonoBehaviour
{
    public combatype cmbtype;
    public Transform Panel;
    public bool collide;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        collide = true;
        Debug.Log("ÖHE PASO ALGO");
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        collide = false;
    }

    [System.Serializable]
    public enum combatype
    {
        Melee,
        Range,
        Siege,
        Special
    }
}
