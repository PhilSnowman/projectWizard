using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public static CameraController Instance;

	[SerializeField] List<Transform> targets = new List<Transform>();
	[SerializeField] float smoothTime = 0.5f;
	[SerializeField] float minY;
	[SerializeField] float minZoom = 40f;
	[SerializeField] float maxZoom = 10f;
	[SerializeField] float zoomLimiter = 50f;

	new Camera camera;

	Vector3 offset;

	Vector3 velocity;

	void Awake() {
		Instance = this;
		offset = transform.position;
		camera = GetComponent<Camera>();
	}

	public void AddTarget(Transform target) {
		targets.Add(target);
	}

	public void RemoveTarget(Transform target) {
		targets.Remove(target);
	}

	void FixedUpdate() {
		Vector3 centerPoint = Vector3.zero;

		if(targets.Count == 1) {
			centerPoint = targets[0].position;
		} else if(targets.Count > 1) {
			Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
			for(int i = 0; i < targets.Count; i++) {
				bounds.Encapsulate(targets[i].position);
			}

			centerPoint = bounds.center;

			camera.orthographicSize = Mathf.Lerp(maxZoom, minZoom, bounds.size.x / zoomLimiter);
		}

		Vector3 newPosition = centerPoint + offset;
		newPosition.y = Mathf.Clamp(newPosition.y, Mathf.NegativeInfinity, Mathf.Infinity);

		transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
	}
}