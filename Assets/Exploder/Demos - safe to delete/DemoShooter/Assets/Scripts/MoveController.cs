using UnityEngine;
using System.Collections;

namespace Exploder.Demo
{
	public class MoveController : MonoBehaviour
	{
		public float speed = 6.0F;
    	public float jumpSpeed = 8.0F;
    	public float gravity = 20.0F;
		private Vector3 moveDirection = Vector3.zero;

		private CharacterController controller;

		void Start()
		{
			controller = GetComponent<CharacterController>();
		}

		void Update()
		{
			if (controller.isGrounded)
			{
				moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= speed;
				if (Input.GetButton("Jump"))
					moveDirection.y = jumpSpeed;

			}

			moveDirection.y -= gravity * Time.deltaTime;
			controller.Move(moveDirection * Time.deltaTime);
		}
	}
}
