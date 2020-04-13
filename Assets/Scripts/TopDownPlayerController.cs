using UnityEngine;
using UnityEngine.Events;

public class TopDownPlayerController : MonoBehaviour
{

	[SerializeField] SpriteRenderer[] renderers;
	[SerializeField] float moveSpeed = 5f;

	Rigidbody2D rb;
	Animator animator;

	Vector2 move;

	int controllerID;


	public void SetUp(int _controllerID)
	{
		controllerID = _controllerID;
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].color = new Color[] { Color.red, Color.green, Color.blue, Color.yellow, Color.magenta, Color.cyan }[_controllerID];
		}
	}

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		//Get and Set the Input values on the X and Y axis
		move = new Vector2(InputManager.GetAxis("Horizontal", controllerID), InputManager.GetAxis("Vertical", controllerID));

		//Animate the player
		animator.SetFloat("Horizontal", this.move.x);
		animator.SetFloat("Vertical", this.move.y);
		animator.SetFloat("Speed", this.move.sqrMagnitude);

		if (InputManager.GetButtonDown("A", controllerID))
		{
			//Do Something with the first button
			Debug.Log("Player " + controllerID + " Pressed SELECT");
		}

		
		if (InputManager.GetButtonDown("B", controllerID))
		{
			//Do something with the Second Button
			Debug.Log("Player " + controllerID + " Pressed ACTION");
		}

		Move();

	}


	private void FixedUpdate()
	{
		Move();
		getDirection();
	}

	public void Move()
	{
		Vector2 movement = new Vector2(move.x, move.y).normalized;
		rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

	}

	void getDirection()
	{
		if (move.y < 0 && Mathf.Abs(move.y) > Mathf.Abs(move.x))
			animator.SetFloat("facingDirection", 0); //Down

		if (move.y > 0 && Mathf.Abs(move.y) > Mathf.Abs(move.x))
			animator.SetFloat("facingDirection", 1); //Up

		if (move.x < 0 && Mathf.Abs(move.y) < Mathf.Abs(move.x))
			animator.SetFloat("facingDirection", 2); //Left

		if (move.x > 0 && Mathf.Abs(move.y) < Mathf.Abs(move.x))
			animator.SetFloat("facingDirection", 3); //Right
	}

	/*void Die()
	{
		transform.position = new Vector3(Random.Range(-10, 10), 10, 0);
	}*/

	void OnEnable()
	{
		CameraController.Instance.AddTarget(transform);
	}

	void OnDisable()
	{
		CameraController.Instance.RemoveTarget(transform);
	}
}