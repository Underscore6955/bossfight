using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class characterControl : MonoBehaviour
{
    public Animator animBottom;
    public Animator animTop;
    public float speed;
    Rigidbody2D rb;
    public float jumpHeight;
    public float doubleJumpHeight;
    public SpriteRenderer srTop;
    public SpriteRenderer srBottom;
    [SerializeField] Sprite standingSpriteBottom;
    [SerializeField] Sprite crouchingSprite;
    [SerializeField] BoxCollider2D standingCollider;
    [SerializeField] BoxCollider2D crouchingCollider;
    public BoxCollider2D curCollider;
    public float dashTime;
    public bool crouching;
    [SerializeField] float dashTimeC;
    [SerializeField] float dashSpeed;
    Vector2 dashDir;
    [SerializeField] AnimationClip doubleJumpAnim;
    public int dir;
    public bool lookingUp;
    void Start()
    {
        curCollider = standingCollider;
        rb = GetComponent<Rigidbody2D>();
        dir = -1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) && rb.velocity.y == 0) { crouch(); }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow) && crouching) {unCrouch(); }
        if (Input.GetButtonDown("Jump") && rb.velocity.y == 0) { Jump(); }
        spriteColliderManager();
        if (rb.velocity.y != 0 && Input.GetButtonDown("Jump") && GetComponent<bloodOrb>().hasOrb) { GetComponent<bloodOrb>().useOrb(doubleJumpAnim); doubleJump(); }
        if (Input.GetKeyDown(KeyCode.LeftShift)) { dash(); }
    }
    private void FixedUpdate()
    {
        LeftRight();
        dashTime -= Time.deltaTime;
    }
    private void LeftRight()
    {
        rb.velocity = new Vector2 (0, rb.velocity.y);
        if (dashTime > 0) { rb.velocity = new Vector2(dashDir.x * Time.deltaTime * dashSpeed, 0); return; }
        if (crouching) { return; }
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, rb.velocity.y);
    }
    private void Jump()
    {
        if(crouching) { unCrouch(); }
        rb.AddForce(new Vector2(0, jumpHeight));
    }
    private void spriteColliderManager()
    {
        if (Input.GetAxisRaw("Horizontal") < 0) { srTop.flipX = false; srBottom.flipX = false; dir = -1; }
        if (Input.GetAxisRaw("Horizontal") > 0) { srTop.flipX = true; srBottom.flipX = true; dir = 1; }
        if (rb.velocity.x != 0) { srBottom.GetComponent<Animator>().SetBool("running", true); } else { srBottom.GetComponent<Animator>().SetBool("running", false); }
        if (Input.GetAxisRaw("Vertical") > 0) { lookingUp = true; } else { lookingUp = false; }
        if (crouching) { srBottom.sprite = crouchingSprite; srTop.enabled = false; standingCollider.enabled = false; crouchingCollider.enabled = true; curCollider = crouchingCollider; } else { srBottom.sprite = standingSpriteBottom; standingCollider.enabled = true; crouchingCollider.enabled = false; curCollider = standingCollider; srTop.enabled = true; }
        if (dashTime > 0) { srTop.enabled = false; Debug.Log("fiji"); } else { srTop.enabled = true; }
    }
    public void crouch()
    {
        rb.transform.localPosition = new Vector2(rb.transform.localPosition.x, rb.transform.localPosition.y - (standingCollider.size.y - crouchingCollider.size.y) * (rb.transform.localScale.y / 2));
        crouching = true;
        srBottom.transform.localPosition = new Vector2(srBottom.transform.localPosition.x, srBottom.transform.localPosition.y + (standingCollider.size.y - crouchingCollider.size.y) / 2); rb.velocity = new Vector2(0,rb.velocity.y);
    }   
    public void unCrouch()
    {
            rb.transform.localPosition = new Vector2(rb.transform.localPosition.x, rb.transform.localPosition.y + (standingCollider.size.y - crouchingCollider.size.y) * (rb.transform.localScale.y / 2));
            crouching = false;
            srBottom.transform.localPosition = new Vector2(srBottom.transform.localPosition.x, srBottom.transform.localPosition.y - (standingCollider.size.y - crouchingCollider.size.y) / 2);
    }
    public void dash()
    {
        if (dashTime < -0.2f) { dashTime = dashTimeC; animBottom.Play("dashAnim"); }
        dashDir = new Vector2(Input.GetAxisRaw("Horizontal"),0);
    }
    public void doubleJump()
    {
        rb.AddForce(new Vector2(0,doubleJumpHeight));
    }
    public void changeAnim(Animation newAnim, bool top)
    {

    }
    public void changeSprite(Sprite newSprite, bool top)
    {

    }
}
