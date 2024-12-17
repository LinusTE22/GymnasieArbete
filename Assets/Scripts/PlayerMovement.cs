using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpheight = 50f;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxcoll;
    [SerializeField] private LayerMask GroundLayer;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxcoll = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");

        // Running 
        body.velocity = new Vector2(HorizontalInput * speed, body.velocity.y);


        // jumping
        if (Input.GetKey(KeyCode.Space) && Grounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpheight);
        }

        //flip player
        if (HorizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (HorizontalInput <  -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Running anim
        anim.SetBool("Running", HorizontalInput != 0);

        anim.SetBool("Grounded", Grounded());

        // raycast/boxcast for detecting ground
        bool Grounded()
        {
            RaycastHit2D raycastHit = Physics2D.BoxCast(boxcoll.bounds.center, boxcoll.bounds.size, 0, Vector2.down, 0.1f, GroundLayer);
            return raycastHit.collider != null;
        }
    }


}
