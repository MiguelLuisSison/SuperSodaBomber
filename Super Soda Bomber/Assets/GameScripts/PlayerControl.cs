using UnityEngine;
using UnityEngine.Events;

/*
PlayerControl
	Used to get the physics and the overall movement of the player
	such as walking, jumping and perks
*/
public class PlayerControl : MonoBehaviour
{

	//Gameplay Script
	public GameObject gameplayDebug;

	[Header("Variables")]
	[Space]

	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .75f;	// How much to smooth out the movement
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    const float gracePeriod = .16f; // Time when player can jump regardless of groundcheck

    private bool m_hangJump = false;
    private float hangTime = 0f;
	private bool m_Grounded;            // Whether or not the player is grounded.
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	private CheckpointScript checkScript;

	[Header("Events")]
	[Space]

	//this will be used on sounds, traps, etc.
	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> {}

	//private classes
	GameplayScript gameplayScript;
	PublicScripts publicScripts;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		// Gets GameplayScript component
		gameplayScript = gameplayDebug.GetComponent<GameplayScript>();
		publicScripts = gameplayDebug.GetComponent<PublicScripts>();

	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

        //add hangtime to jump
        if (colliders.Length == 0 && Time.time < hangTime && !m_hangJump){

            m_hangJump = true;
            m_Grounded = true;
            if (!wasGrounded)
                OnLandEvent.Invoke();
        }

		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject) 
			{
                m_hangJump = false;
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
                hangTime = gracePeriod + Time.time;
			}
		}
	}

	public void Move(float move, bool jump)
	{

		// Move the character by finding the target velocity
		Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
		// And then smoothing it out and applying it to the character
		m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

		// If the input is moving the player right and the player is facing left...
		if (move > 0 && !m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (move < 0 && m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			Debug.Log(m_hangJump? "Hangjumped" : "Jumped");
			// Add a vertical force to the player.
			m_Rigidbody2D.AddForce(new Vector2(0f, m_hangJump? m_JumpForce*1.25f : m_JumpForce));
			m_Grounded = false;
            m_hangJump = false;

			// Add score
			gameplayScript.AddScore(publicScripts.scores["jump"]);

		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	// triggers when player touches the checkpoint
    private void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.layer == 10){
			//gets SpriteRenderer and changes the image
			checkScript = col.GetComponent<CheckpointScript>();

			//activate these scripts if the checkpoint was not saved yet
			if(!checkScript.isTouched){
				checkScript.ChangeState();
				gameplayScript.AddScore(publicScripts.scores["checkpoint"]);
				gameplayScript.SetCheckpoint(col.transform.position, col.name);
				StartCoroutine(checkScript.Notify());
				Debug.Log("Checkpoint Saved!");
			}
		}
    }

}
