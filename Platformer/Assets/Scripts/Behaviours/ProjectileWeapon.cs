using UnityEngine;

namespace FATEC.Platformer.Behaviours
{
    /// <summary>
    /// Fires a projectile.
    /// </summary>
    public class ProjectileWeapon : BaseBehaviour {
		[Tooltip("Projectile prefab to fire.")]
		public Transform projectilePrefab;
		[Tooltip("The amount of space to translate the projectile after instantiation.")]
		public Vector3 offset = new Vector3(0.3f, 0, 0f);

        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                this.FireProjectile(Vector2.up);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                this.FireProjectile(Vector2.down);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                this.FireProjectile(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                this.FireProjectile(Vector2.right);
            }
        }

        /// <summary>
        /// Fires a projectile.
        /// </summary>
        protected void FireProjectile(Vector3 pDirection) {
			var projectile = Instantiate(this.projectilePrefab);
			var projectileTransform = projectile.GetComponent<Transform>();

            projectile.GetComponent<DirectionalMovement>().direction = pDirection;

			projectileTransform.position = 
				this.transform.position + new Vector3(offset.x * pDirection.x, offset.y * pDirection.y);
		}

        protected void FireProjectile2()
        {
            var projectile = Instantiate(this.projectilePrefab);
            var projectileTransform = projectile.GetComponent<Transform>();

            var directionX = this.transform.localScale.x;
            projectile.GetComponent<DirectionalMovement>().direction = new Vector2(directionX, 0);

            projectileTransform.position =
                this.transform.position + this.offset * directionX;
        }
    }
}