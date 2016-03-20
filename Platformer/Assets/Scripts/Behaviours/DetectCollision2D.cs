using UnityEngine;
using System.Collections;

namespace FATEC.Platformer.Behaviours
{
	/// <summary>
	/// Detects collision based on tags.
	/// If collided with the tag, checks whether the collision
	/// is on top and, even being on top, destroys the other object.
	/// 
	/// If not on top, restarts the level.
	/// </summary>
	[RequireComponent(typeof(Collider2D))]
	public class DetectCollision2D : BaseBehaviour {

		[Tooltip("The target tag to check collision.")]
		public string targetTag;

		protected void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.collider.CompareTag(this.targetTag))
			{
				var isCollisionUp = false;

				for (var collisionIndex = 0; collisionIndex < collision.contacts.Length; collisionIndex++)
				{
					if (collision.contacts[collisionIndex].normal == Vector2.up)
					{
						isCollisionUp = true;
						break;
					}
				}

				if(isCollisionUp) {
					Destroy(collision.collider.gameObject);
				} else{
					Application.LoadLevel(Application.loadedLevel);
				}
			}
		}

	}
}