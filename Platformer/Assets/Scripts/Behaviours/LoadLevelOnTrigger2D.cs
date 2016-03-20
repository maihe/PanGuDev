using UnityEngine;
using System.Collections;

namespace FATEC.Platformer.Behaviours
{
    /// <summary>
    /// Loads a level when a trigger occurs.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class LoadLevelOnTrigger2D : BaseBehaviour
    {
        [Tooltip("Name of the level to load.")]
        public string levelName;

        [Tooltip("Name of the tag to allow level loading.")]
        public string tagName;        

        protected void Start()
        {
            this.GetComponent<Collider2D>().isTrigger = true;
        }

        protected void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag(this.tagName))
            {
                Application.LoadLevel(Application.loadedLevel);
            }            
        }
    }
}