using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {

	/*[SerializeField] SpriteRenderer[] renderers;
	[SerializeField] float jumpForce = 300f;
	[SerializeField] float runSpeed = 30f;
	[Range(0, .3f)] [SerializeField] float movementSmoothing = .05f;
	[SerializeField] bool airControl = false;
	[SerializeField] LayerMask groundMask;
	[SerializeField] Transform groundCheck;
	[SerializeField] Transform wallCheck;
	[SerializeField] GameObject bulletPrefab;

	const float groundedRadius = .2f;
	bool grounded;
	Rigidbody2D rb;
	bool facingRight = true;
	Vector3 velocity = Vector3.zero;

	float move;
	bool jump;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	int controllerID;

	Vector2 defaultShoot = Vector2.right;

	float prevAngle;

	public void SetUp(int _controllerID) {
		controllerID = _controllerID;
		for(int i = 0; i < renderers.Length; i++) {
			renderers[i].color = new Color[] { Color.red, Color.green, Color.blue, Color.yellow, Color.magenta, Color.cyan }[_controllerID];
		}
	}

	private void Awake() {
		rb = GetComponent<Rigidbody2D>();

		if(OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	void Update() {
		move = InputManager.GetAxis("Horizontal", controllerID) * runSpeed * Time.fixedDeltaTime;

		if(InputManager.GetButtonDown("A", controllerID)) {
			jump = true;
		}

		Vector2 newShoot = defaultShoot;

		float deadZone = 0.2f;
		bool ignoreAim = true;

		float horizontal = InputManager.GetAxis("Horizontal", controllerID);
		if(Mathf.Abs(horizontal) > deadZone) {
			ignoreAim = false;
		}
		float vertical = InputManager.GetAxis("Vertical", controllerID);
		if(Mathf.Abs(vertical) > deadZone) {
			ignoreAim = false;
		}
		if(!ignoreAim) {
			float angle = RotationAngleInDegrees(Vector3.zero, new Vector3(-horizontal, vertical)) + 90;
			prevAngle = angle;
		}

		if(InputManager.GetButtonDown("B", controllerID)) {
			Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
			bullet.SetUp(controllerID, prevAngle);
		}

		Move();

		if(transform.position.y < -10) {
			Die();
		}
	}

	private float RotationAngleInDegrees(Vector3 centerPt, Vector3 targetPt) {
		double theta = System.Math.Atan2(targetPt.y - centerPt.y, targetPt.x - centerPt.x);
		theta -= System.Math.PI / 2.0;
		double angle = (theta * Mathf.Rad2Deg);
		if(angle < 0)
			angle += 360;

		angle -= 360;
		//Debug.Log("Angle is: " + System.Math.Abs(angle));

		return (float)System.Math.Abs(angle);
	}

	private void FixedUpdate() {
		bool wasGrounded = grounded;
		grounded = false;

		Collider2D[] groundColliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, groundMask);
		for(int i = 0; i < groundColliders.Length; i++) {
			if(groundColliders[i].gameObject != gameObject) {
				grounded = true;
				if(!wasGrounded)
					OnLandEvent.Invoke();
			}
		}

		Collider2D[] wallColliders = Physics2D.OverlapCircleAll(wallCheck.position, groundedRadius, groundMask);
		for(int i = 0; i < wallColliders.Length; i++) {
			if(wallColliders[i].gameObject != gameObject) {
				grounded = true;
				if(!wasGrounded)
					OnLandEvent.Invoke();
			}
		}

		jump = false;
	}

	public bool OnTakeDamage(float damage, int _controllerID) {
		if(controllerID == _controllerID)
			return false;
		Die();
		return true;
	}

	public void Move() {
		if(grounded || airControl) {
			Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
			rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

			if(move > 0 && !facingRight) {
				Flip();
			}
			else if(move < 0 && facingRight) {
				Flip();
			}
		}
		if(grounded && jump) {
			grounded = false;
			if(rb.velocity.y > 0) {
				rb.velocity = new Vector2(rb.velocity.x, jumpForce + rb.velocity.y);
			} else {
				rb.velocity = new Vector2(rb.velocity.x, jumpForce);
			}
		}
	}

	void Die() {
		transform.position = new Vector3(Random.Range(-10, 10), 10, 0);
	}

	void Flip() {
		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnEnable() {
		CameraController.Instance.AddTarget(transform);
	}

	void OnDisable() {
		CameraController.Instance.RemoveTarget(transform);
	}*/
}