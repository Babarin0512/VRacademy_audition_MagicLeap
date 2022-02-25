using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MagicLeap
{
    /// <summary>
    /// This class handles setting the position and rotation of the
    /// transform to match the camera's based on input distance and height
    /// </summary>
    public class LookAtCamera : MonoBehaviour
    {
        public enum LookDirection
        {
            None = 0,
            LookAtCamera = 1,
            LookAwayFromCamera = 2
        }

       

        [SerializeField, Tooltip("The approximate time it will take to reach the current rotation.")]
        private float _rotationSmoothTime = 5f;

        [SerializeField, Tooltip("The direction the transform should face.")]
        private LookDirection _lookDirection = LookDirection.LookAwayFromCamera;

        [SerializeField, Tooltip("Toggle to set position on awake.")]
        private bool _placeOnAwake = false;

        [SerializeField, Tooltip("Toggle to set position on update.")]
        private bool _placeOnUpdate = false;

        [SerializeField, Tooltip("Prevents the object from rotating around the Z axis.")]
        private bool _lockZRotation = false;

        [SerializeField, Tooltip("Prevents the object from rotating around the X axis.")]
        private bool _lockXRotation = false;

        [SerializeField, Tooltip("Places the object in front of and at the same Y position as the camera.")]
        private bool _lockToXZPlane = false;

        [SerializeField, Tooltip("When this is enabled, movement is restricted by threshold conditions.")]
        private bool _useThreshold = false;

        [SerializeField, Tooltip("When [UseThreshold] is enabled the (x, y) euler limits will force an update.")]
        private Vector2 _movementThreshold = new Vector2(10f, 5f);

        

        [SerializeField, Tooltip("When enabled, used local space instead of world space.")]
        private bool _useLocalSpace = true;

        private bool _forceUpdate = false;
        private Camera _mainCamera = null;

        private bool _shouldTryPlacementOnEnable = false;
        private bool _didPlaceOnAwake = false;

        /// <summary>
        /// When enabled automatic placement will occur on each Update cycle.
        /// </summary>
        public bool PlaceOnUpdate
        {
            get { return _placeOnUpdate; }
            set { _placeOnUpdate = value; }
        }

        

        /// <summary>
        /// Set the transform from latest position if flag is checked.
        /// </summary>
        void Awake()
        {
            _mainCamera = Camera.main;
            SceneManager.activeSceneChanged += ChangedActiveScene;

            if (_placeOnAwake)
            {
                StartCoroutine(UpdateTransformEndOfFrame());
            }
        }

        private void OnEnable()
        {
            if (_shouldTryPlacementOnEnable && _placeOnAwake)
            {
                _shouldTryPlacementOnEnable = false;
                StartCoroutine(UpdateTransformEndOfFrame());
            }
        }

        void Update()
        {
            if (!_placeOnAwake && _placeOnUpdate)
            {
                UpdateTransform(_mainCamera);
            }
        }

       

        private void OnDisable()
        {
            // If the game object was initially awake, causing the UpdateTransformEndOfFrame()
            // coroutine to be queued, but before it could be called, the game object was disabled.
            // In that case, retry placement when the game object is enabled again.
            _shouldTryPlacementOnEnable = _placeOnAwake && !_didPlaceOnAwake;
        }

        void OnDestroy()
        {
            SceneManager.activeSceneChanged -= ChangedActiveScene;
        }

        public void ForceUpdate()
        {
            _forceUpdate = true;
        }

        public void ToggleThreshold()
        {
            _useThreshold = !_useThreshold;
        }

        private void ChangedActiveScene(Scene current, Scene next)
        {
            _mainCamera = Camera.main;
        }

        /// <summary>
        /// Reset position and rotation to match current camera values, after the end of frame.
        /// </summary>
        private IEnumerator UpdateTransformEndOfFrame()
        {
            // Wait until the camera has finished the current frame.
            yield return new WaitForEndOfFrame();
            _didPlaceOnAwake = true;
            UpdateTransform(_mainCamera);
        }

        /// <summary>
        /// Reset position and rotation to match current camera values.
        /// </summary>
        private void UpdateTransform(Camera camera)
        {
            

            Quaternion targetRotation = Quaternion.identity;

            // Rotate the object to face the camera.
            if (_lookDirection == LookDirection.LookAwayFromCamera)
            {
                targetRotation = Quaternion.LookRotation((_useLocalSpace ? transform.localPosition : transform.position) - (_useLocalSpace ? camera.transform.localPosition : camera.transform.position), camera.transform.up);
            }
            else if (_lookDirection == LookDirection.LookAtCamera)
            {
                targetRotation = Quaternion.LookRotation((_useLocalSpace ? camera.transform.localPosition : camera.transform.position) - (_useLocalSpace ? transform.localPosition : transform.position), camera.transform.up);
            }

            if (_useLocalSpace)
            {
                transform.localRotation = _placeOnAwake ? targetRotation : Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime / _rotationSmoothTime);
            }
            else
            {
                transform.rotation = _placeOnAwake ? targetRotation : Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime / _rotationSmoothTime);
            }

            if (_placeOnAwake)
            {
                _placeOnAwake = false;
            }

            if (_lockZRotation)
            {
                if (_useLocalSpace)
                {
                    transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
                }
            }

            if (_lockXRotation)
            {
                if (_useLocalSpace)
                {
                    transform.localRotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                }
            }
        }
    }
}