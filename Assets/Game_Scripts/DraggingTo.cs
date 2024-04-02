using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingTo : MonoBehaviour
{
    public bool isDragging = false; 
    public bool isOverDropZone;
    public GameObject meleeZone;
    public Vector2 startPosition;

    // Update is called once per frame
    void Update()
    {
        if(isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOverDropZone = true;
        meleeZone = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverDropZone = false;
        meleeZone = null;
    }

    public void StartDrag()
    {
        startPosition = transform.position;
        isDragging = true;
    }

    public void EndDrag()
    {
        isDragging = false;
        if (isOverDropZone)
        {
            transform.SetParent(meleeZone.transform, false);
        }
        else
        {
            transform.position = startPosition;
        }
    }
}

