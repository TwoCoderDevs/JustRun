using UnityEngine;

namespace DynamicRagdoll.Demo {

	/*
		a simple camera follow script
	*/
	public class CameraHandler : MonoBehaviour
	{
		public float moveSpeed = 1.5f;			
		public float turnSpeed = 7f;			
		public float maxDistance = 4.0f;
		public float minDistance = 0.5f;
		public Transform target;
		public RagdollController r;
		Vector3 startDir;
		public AudioSource hurt;
		public AudioClip hurtClip;
		public LayerMask DetectedLayer;
		
		void Awake ()
		{
			
			startDir = (-transform.position).normalized;	

			//SetTarget(false);
			
		}

		private void Start() {
			SetTarget(false);
		}

		public void GetRagdollController(RagdollController ragdollController)
		{
			r = ragdollController;
		}

		public void SetTarget(bool ragdolled){

			if(ragdolled) hurt.PlayOneShot(hurtClip);

			target = ragdolled ? r.ragdoll.RootBone().transform 
			: GameObject.FindGameObjectWithTag("Player").transform; 

		
		}		
		
		void Update () {
			
			
			Vector3 camPos = transform.position;
			Quaternion camRot = transform.rotation;
			Vector3 targetPos = target.position;

			transform.position = Vector3.Lerp(camPos, targetPos - startDir * GetDistance(), moveSpeed * Time.deltaTime);
			transform.rotation = Quaternion.Slerp(camRot, Quaternion.LookRotation(targetPos - camPos), 
			turnSpeed * Time.deltaTime);
		}	

		float GetDistance()
		{

			Vector3 desiredCameraPos = transform.TransformPoint(-startDir * maxDistance);
			RaycastHit hit;

			if (Physics.Linecast(transform.position, desiredCameraPos, out hit, DetectedLayer))
			{
				return Mathf.Clamp((hit.distance * 0.87f), minDistance, maxDistance);
			}
			else
			{
				return maxDistance;
			}

			//CameraObj.transform.localPosition = Vector3.Lerp(CameraObj.transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
		}
	
	}
}