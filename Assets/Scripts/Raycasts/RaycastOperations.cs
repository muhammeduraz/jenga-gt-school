using UnityEngine;

namespace Assets.Scripts.Raycasts
{
    public class RaycastOperations
    {
        #region Variables

        private Ray _ray;
        private Camera _camera;

        #endregion Variables

        #region Functions

        public RaycastOperations()
        {
            _camera = Camera.main;
        }

        public T GetObjectOfType<T>(Vector3 screenPosition) where T : Component
        {
            T requiredObject = default;

            _ray = _camera.ScreenPointToRay(screenPosition);

            RaycastHit raycastHit;
            bool didHit = Physics.Raycast(_ray, out raycastHit, float.MaxValue);

            if (didHit)
            {
                raycastHit.collider.TryGetComponent<T>(out requiredObject);
            }

            return requiredObject;
        }

        #endregion Functions
    }
}