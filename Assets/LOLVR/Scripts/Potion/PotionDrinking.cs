using UnityEngine;

namespace LOLVR
{
    public class PotionDrinking : MonoBehaviour
    {
        private const float TRIGGER_ELAPSED_SECONDS = 0.15f;

        private Potion triggeredPotion;
        private float playerCollisionElapsedTime;

        private void Update()
        {
            if (triggeredPotion)
            {
                playerCollisionElapsedTime += Time.deltaTime;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Potion") && other.TryGetComponent(out Potion potion))
            {
                playerCollisionElapsedTime = 0;
                triggeredPotion = potion;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (triggeredPotion && triggeredPotion.IsFresh && playerCollisionElapsedTime >= TRIGGER_ELAPSED_SECONDS)
            {
                triggeredPotion.DrinkPotion();
                playerCollisionElapsedTime = 0;
                triggeredPotion = null;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (triggeredPotion && other.CompareTag("Potion"))
            {
                playerCollisionElapsedTime = 0;
                triggeredPotion = null;
            }
        }
    }
}
