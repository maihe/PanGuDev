using UnityEngine;
using System.Collections;

namespace FATEC.Platformer.Behaviours
{
	/// <summary>
	/// Performs a horizontal movement.
	/// </summary>
	[RequireComponent(typeof(Collider2D))]
	public class HorizontalMovement2D : BaseBehaviour {
			
		/// <summary>
		/// Represents the direction on a movement.
		/// </summary>
		public enum MovementDirection {
			Left,
			Right
		}

		[Tooltip("Movement velocity.")]
		public float movementVelocity = 1.0f;

		[Tooltip("Initial movement direction.")]
		public MovementDirection direction = MovementDirection.Left;

		[Tooltip("Duration of the movement in a given direction.")]
		public float duration = 5.0f;

		/// <summary>Reference to the rigibody component.</summary>
		protected Rigidbody2D rigibody;

		protected override void Awake() {
			base.Awake();
			this.rigibody = this.GetComponent<Rigidbody2D>();
		}

		protected void Start() {
			this.StartCoroutine (this.ChangeDirection());
		}

		protected void Update() {
			var velocity = this.rigibody.velocity;
			velocity.x = (this.direction == MovementDirection.Left ? -this.movementVelocity : this.movementVelocity);
			this.rigibody.velocity = velocity;
		}

		/// <summary>
		/// Changes the direction.
		/// </summary>
		/// <returns>The coroutine enumerator.</returns>
		protected IEnumerator ChangeDirection() {
			while (true) {

				var scale = this.transform.localScale;
				scale.x = (this.direction == MovementDirection.Left ? 1.0f : -1.0f);
				this.transform.localScale = scale;

				yield return new WaitForSeconds(this.duration);

				this.direction = 
					(this.direction == MovementDirection.Left ? MovementDirection.Right : MovementDirection.Left);

			}
		}
	
	}
}