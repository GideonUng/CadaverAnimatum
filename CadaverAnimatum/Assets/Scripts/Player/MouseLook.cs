using UnityEngine;
using System.Collections;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour
{

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	public float maxTurnSpeed;
	public bool mouseAcceleration;

	public float rotationX = 0f;
	public float rotationY = 0F;

	void Update()
	{
		if (axes == RotationAxes.MouseXAndY)
		{
			if (mouseAcceleration)
			{
				rotationX += Mathf.Clamp(Mathf.Abs(Input.GetAxis("Mouse X") * 0.4f) * Input.GetAxis("Mouse X") * sensitivityX * 2.5f, -maxTurnSpeed, maxTurnSpeed);
				rotationY += Mathf.Clamp(Mathf.Abs(Input.GetAxis("Mouse Y") * 0.4f) * Input.GetAxis("Mouse Y") * sensitivityY * 2.5f, -maxTurnSpeed, maxTurnSpeed);
			}
			else
			{
				rotationX += Mathf.Clamp(Input.GetAxis("Mouse X") * sensitivityX, -maxTurnSpeed, maxTurnSpeed);
				rotationY += Mathf.Clamp(Input.GetAxis("Mouse Y") * sensitivityY, -maxTurnSpeed, maxTurnSpeed);
			}
			rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
		else if (axes == RotationAxes.MouseX)
		{
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
	}

	void Start()
	{
		rotationY = transform.rotation.eulerAngles.x;
		rotationX = transform.rotation.eulerAngles.y;

		// Make the rigid body not change rotation
		if (rigidbody)
			rigidbody.freezeRotation = true;
	}
}