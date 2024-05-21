using UnityEngine;

namespace GD.Managers
{
    public class AudioManager : MonoBehaviour
    {
        #region Self Variables

        #region Public  Variables

        //

        #endregion

        #region Serialized Variables

        //

        #endregion

        #region Private Variables

        //

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {

        }

        private void UnsubscribeEvents()
        {

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}