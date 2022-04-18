using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rbd;
    private Animator anime;
    private SpriteRenderer spriter;
    private BoxCollider2D bcoll;

    [SerializeField] private LayerMask JumpableGround;

    private float diX = 0f;
    [SerializeField] private float mvspeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementStat { idle, running, jumping, falling }
    private MovementStat Stat = MovementStat.idle;

    [SerializeField] private AudioSource jumpsound;


    // Start is called before the first frame update
    private void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        bcoll = GetComponent<BoxCollider2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        diX = Input.GetAxisRaw("Horizontal");
        rbd.velocity = new Vector2(diX * mvspeed, rbd.velocity.y);


        if (Input.GetButtonDown("Jump") && IsGround())
        {
            rbd.velocity = new Vector2(rbd.velocity.x, jumpForce);
            jumpsound.Play();
        }

        UpdateAnimeState();

    }

    private void UpdateAnimeState()
    {

        MovementStat Stat;

        if (diX > 0f)
        {
            Stat = MovementStat.running;
            spriter.flipX = false;
        }
        else if (diX < 0)
        {
            Stat = MovementStat.running;
            spriter.flipX = true;
        }
        else
        {
            Stat = MovementStat.idle;
        }

        if (rbd.velocity.y > .1f)
        {
            Stat = MovementStat.jumping;
        }
        else if (rbd.velocity.y < -.1f)
        {
            Stat = MovementStat.falling;
        }

        anime.SetInteger("Stat", (int)Stat);

    }

    private bool IsGround()
    {
        return Physics2D.BoxCast(bcoll.bounds.center, bcoll.bounds.size, 0f, Vector2.down, .1f, JumpableGround);
    }



}
