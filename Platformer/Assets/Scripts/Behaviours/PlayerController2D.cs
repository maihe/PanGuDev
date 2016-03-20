using UnityEngine;
using System.Collections;

namespace FATEC.Platformer.Behaviours
{

    /// <summary>
    /// Controls the movement of the player.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerController2D : BaseBehaviour
    {

        [Tooltip("Speed of the player´s movement.")]
        public float movementSpeed = 3.0f;

        [Tooltip("Speed of the player´s jump.")]
        public float jumpSpeed = 5.0f;

        [Tooltip("Name of the ground tag.")]
        public string groundTagName = "Ground";

        /// <summary>Reference to the Rigibody component.</summary>
        protected Rigidbody2D rigibody;

        /// <summary>Reference to the Animator component.</summary>
        protected Animator animator;

        /// <summary>Indicates whether the player can jump.</summary>
        protected bool isJumping = false;

        protected override void Awake()
        {
            base.Awake();
            this.rigibody = this.GetComponent<Rigidbody2D>();
            this.animator = this.GetComponent<Animator>();            
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag(this.groundTagName))
            {
                for (var collisionIndex = 0; collisionIndex < collision.contacts.Length; collisionIndex++)
                {
                    if (collision.contacts[collisionIndex].normal == Vector2.up)
                    {
                        this.isJumping = false;
                        break;
                    }
                }
            }
        }

        protected void OnCollisionExit2D(Collision2D collision)
        {
            this.isJumping = (collision.collider.CompareTag(this.groundTagName));
        }

        // Update is called once per frame
        protected void Update()
        {

            var velocity = this.rigibody.velocity;
            var scale = this.transform.localScale;

            //Horizontal movement.
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                velocity.x = -this.movementSpeed;
                scale.x = -Mathf.Abs(scale.x);
                //this.transform.localScale = new Vector2();
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                velocity.x = this.movementSpeed;
                scale.x = Mathf.Abs(scale.x);
            }
            else
            {
                velocity.x = 0;
            }

            //Vertical movement.
            if (!this.isJumping &&
                (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
            {
                velocity.y = this.jumpSpeed;
            }

            this.animator.SetBool("walk", (velocity.x != 0));
            this.animator.SetBool("jump", isJumping);
            this.rigibody.velocity = velocity;
            this.transform.localScale = scale;
        }
    }
}