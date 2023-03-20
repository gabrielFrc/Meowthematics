using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public Rigidbody2D itemRB;
    public SpriteRenderer itemSprite;

    void Start()
    {
        itemSprite = GetComponent<SpriteRenderer>();
        itemRB = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "InvisibleFloor")
            Invoke("ResetItemPosition",2f);
    }
    public void ResetItemPosition()
    {
        itemSprite.sprite = null;
        itemRB.gravityScale = 0;

        itemSprite.transform.position = new Vector3(0,2);

        itemSprite.sortingOrder = 1;
    }
}
