using UnityEngine;
using DynamicRagdoll;

namespace NWH.VehiclePhysics
{
    /// <summary>
    /// Camera that follows behind the vehicle.
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        /// <summary>
        /// Target transform that the camera will follow.
        /// </summary>
        [Tooltip("Target transform that the camera will follow.")]
        public Transform target;

        /// <summary>
        /// Distance at which camera will follow.
        /// </summary>
        [Tooltip("Distance at which camera will follow.")]
        [Range(0, 30f)]
        public float distance = 6f;

        /// <summary>
        /// Height in relation to the target at which the camera will follow.
        /// </summary>
        [Tooltip("Height in relation to the target at which the camera will follow.")]
        [Range(0, 10f)]
        public float height = 2.5f;

        /// <summary>
        /// Offset in the up direction from the target. Use this if you do not want to use camera baits.
        /// </summary>
        [Tooltip("Offset in the up direction from the target. Use this if you do not want to use camera baits.")]
        [Range(-5, 5f)]
        public float targetUpOffset = 1.25f;

        /// <summary>
        /// Offset in the forward direction from the target. Use this if you do not want to use camera baits.
        /// </summary>
        [Range(-10f, 10f)]
        public float targetForwardOffset = 0;

        /// <summary>
        /// Positional and rotational smoothing of the camera.
        /// </summary>
        [Tooltip("Positional and rotational smoothing of the camera.")]
        [Range(0, 1f)]
        public float smoothing = 0.2f;

        /// <summary>
        /// Allows camera to match target's angle to some extent.
        /// </summary>
        [Tooltip("Allows camera to match target's angle to some extent.")]
        [Range(0, 1f)]
        public float angleFollowStrength = 0;

        private float angle;
        private Vector3 targetForward;
        private Vector3 targetForwardVelocity;
        private bool firstFrame = true;
        private RagdollController r;

        public void GetRagdollController(RagdollController ragdollController)
		{
			r = ragdollController;
		}

		public void SetTarget(bool ragdolled){

			target = ragdolled ? r.ragdoll.RootBone().transform 
			: GameObject.FindGameObjectWithTag("Player").transform; 

		}

        

        void LateUpdate()
        {

            Vector3 prevTargetForward = targetForward;
            targetForward = Vector3.SmoothDamp(prevTargetForward, target.forward, ref targetForwardVelocity, smoothing);
            
            angle = AngleSigned(target.forward, (target.position - transform.position), Vector3.up);

            Vector3 desiredPosition = target.position + targetForward * -(distance) + Vector3.up * height;

            RaycastHit hit;
            if (Physics.Raycast(desiredPosition, -Vector3.up, out hit, 0.8f))
            {
                desiredPosition = hit.point + Vector3.up * 0.8f;
            }

            transform.position = desiredPosition;
            transform.LookAt(target.position + Vector3.up * targetUpOffset + target.forward * targetForwardOffset);
            transform.rotation = Quaternion.AngleAxis(-angle * angleFollowStrength, Vector3.up) * transform.rotation;
        }

        public static float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
        {
            return Mathf.Atan2(Vector3.Dot(n, Vector3.Cross(v1, v2)), Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
        }
    }
}
