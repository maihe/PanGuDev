using UnityEngine;
using System.Collections;

namespace FATEC.Platformer.Behaviours
{ 
    /// <summary>
    /// Moves the object towards a given direction.
    /// </summary>
    public class DirectionalMovement : BaseBehaviour {
		[Tooltip("Movement direction")]
		public Vector2 direction = new Vector2(1.0f, 0);
		[Tooltip("Movement speed (u/s)")]
		public float speed = 6.0f;

		protected void Update() {
			this.transform.Translate(
				this.direction * this.speed * Time.deltaTime);
		}
	}
}