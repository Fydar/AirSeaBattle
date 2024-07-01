using UnityEngine;

namespace AirSeaBattleUnity
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private GameObject container;

        public void Show()
        {
            container.SetActive(true);
        }

        public void Hide()
        {
            container.SetActive(false);
        }
    }
}
