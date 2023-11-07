using LOLVR.Config;
using LOLVR.Input;
using UnityEngine;

namespace LOLVR
{
    public class Potion : MonoBehaviour
    {
        private static readonly int isFresh = Animator.StringToHash("IsFresh");

        [SerializeField]
        private AudioSource drinkSound;

        [SerializeField]
        private Animator drinkingAnimator;

        public bool IsFresh { get; private set; } = true;


        public void DrinkPotion()
        {
            Debug.Log("Drinking potion");
            drinkSound.Play();
            drinkingAnimator.SetBool(isFresh, false);
            IsFresh = false;

            SendAction.SimulateKey(ConfigManager.PotionKey);

            Invoke(nameof(RestockPotion), 5f);
        }


        public void RestockPotion()
        {
            drinkingAnimator.SetBool(isFresh, true);
            IsFresh = true;
        }
    }
}
