/*
using UnityEngine;

namespace FATEC.Platformer.Behaviours
{
    /// <summary>
    /// Fires a projectile when an enemy is on sight.
    /// </summary>
    [RequireComponent(typeof(EnemyDetector))]
	public class ProjectileSightableWeapon : ProjectileWeapon {
		/// <summary>Enemy detector component.</summary>
		protected EnemyDetector detector;

		protected override void Awake() {
			base.Awake();
			this.detector = this.GetComponent<EnemyDetector>();
		}

		protected override void Update () {
			if (this.detector.enemyOnSight) {
				this.FireProjectile();
			}
		}
	}
}
*/