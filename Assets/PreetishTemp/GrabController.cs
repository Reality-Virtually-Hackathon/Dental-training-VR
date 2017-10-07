using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ViveController
{
	public class GrabController : MonoBehaviour 
	{
		private GameObject _grabbedObject;
		private PickupType _pickupType;
		private bool _hideController;
		private Vector3 _position;
		private Quaternion _rotation;
		private float _duration = 0.1f;
		private bool isGrabbing = false;
        private bool _stopGravity = true;
        private bool _kinematic;
		public bool dismiss = false;

		///<summary>
		///Grabs object passed in.
		///</summary>
		public void Grab(GameObject grabbedObject, PickupType pickupType = PickupType.OriginLerp, bool hideController = true, bool stopGravity = true)
		{
			grab(grabbedObject, pickupType, hideController, stopGravity);
		}

		///<summary>
		///Grabs object passed in. Position and rotation used for custom pickupType.
		///</summary>
		public void Grab(GameObject grabbedObject, Vector3 position, Quaternion rotation, bool hideController = true, bool stopGravity = true)
		{
			grab(grabbedObject, PickupType.Custom, hideController, stopGravity);
			_position = position;
			_rotation = rotation;
		}

		///<summary>
		///Returns current held object. Returns null if not holding.
		///</summary>
		public GameObject GetGrabbedObject()
		{
			return isGrabbing ? _grabbedObject : null;
		}

		///<summary>
		///Releases current held object.
		///</summary>
		public void DropObject()
		{
			isGrabbing = false;
			transform.Find("Model").gameObject.SetActive(true);
			_grabbedObject.transform.parent = null;
            Rigidbody rb = _grabbedObject.GetComponent<Rigidbody>();
            if (_stopGravity && rb != null) { }
                rb.isKinematic = _kinematic;
        }

		public GameObject grabbedObject
		{
			get { return _grabbedObject; }
			set { _grabbedObject = value; }
		}

		public PickupType pickupType
		{
			get { return _pickupType; }
			set { _pickupType = value; }
		}

		public bool hideController
		{
			get { return _hideController; }
			set { _hideController = value; }
		}

		public Vector3 position
		{
			get { return _position; }
			set { _position = value; }
		}

		public Quaternion rotation
		{
			get { return _rotation; }
			set { _rotation = value; }
		}

		public float duration
		{
			get { return _duration; }
			set { _duration = value; }
		}

        public bool stopGravity
        {
            get { return _stopGravity; }
            set { _stopGravity = value; }
        }

        private void grab(GameObject grabbedObject, PickupType pickupType, bool hideController, bool stopGravity)
		{
			_grabbedObject = grabbedObject;
			_pickupType = pickupType;
			_hideController = hideController;
            _stopGravity = stopGravity;
			isGrabbing = true;
			_grabbedObject.transform.parent = this.gameObject.transform;
            Rigidbody rb = _grabbedObject.GetComponent<Rigidbody>();
            if (_stopGravity && rb != null)
            {
                _kinematic = rb.isKinematic;
                rb.isKinematic = true;
            }
		}

		private void Update()
		{
			if (isGrabbing)
			{
				switch (_pickupType)
				{
					case PickupType.Origin:
						_grabbedObject.transform.localPosition = Vector3.zero;
						_grabbedObject.transform.localRotation = Quaternion.identity;
						break;
					case PickupType.Natural:
						break;
					case PickupType.OriginLerp:
						_grabbedObject.transform.localPosition = Vector3.Lerp(_grabbedObject.transform.localPosition, Vector3.zero, _duration);
						_grabbedObject.transform.localRotation = Quaternion.Lerp(_grabbedObject.transform.localRotation, Quaternion.identity, _duration);
						break;
					case PickupType.Custom:
						_grabbedObject.transform.localPosition = Vector3.Lerp(_grabbedObject.transform.localPosition, _position, _duration);
						_grabbedObject.transform.localRotation = Quaternion.Lerp(_grabbedObject.transform.localRotation, _rotation, _duration);
						break;
				}
				if (_hideController)
				{
					transform.Find("Model").gameObject.SetActive(false);
				}
			}
		}
	}
}
