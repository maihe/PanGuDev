using UnityEngine;
using System.Collections;

namespace FATEC.Platformer.Behaviours
{
    /// <summary>
    /// Detects whether the game object is off the level.
    /// </summary>
    public class DetectOffLevel : BaseBehaviour
    {
        [Tooltip("The minimum level height the game object can move to.")]
        public float minimumHeight;

        // Update is called once per frame
        private void Update()
        {
            if(this.transform.position.y < this.minimumHeight)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }
}