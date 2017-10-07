using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ViveController
{
    public class HapticObject : MonoBehaviour
    {
        [SerializeField]
        private int _strength = 3999;
        [SerializeField]
        private float _duration = 0.1f;
        [SerializeField]
        private HapticForm _hapticForm = HapticForm.OnEnter;
        [SerializeField]
        private HapticStyle _hapticStyle = HapticStyle.Default;
        [SerializeField]
        private ControllerEvent _hapticEvent = ControllerEvent.Both;
        [SerializeField]
        private bool _overwrite = true;
        private bool duringCollision = false;
        private ControllerObject controllerObject;
        public bool dismiss = false;

        private void Start()
        {
            controllerObject = new ControllerObject();
        }

        private void Update()
        {
            if (duringCollision && _hapticForm == HapticForm.DuringCollision)
                controllerObject.hapticController.Haptic(_strength, _overwrite);
        }

        private void OnEnter()
        {
            switch (_hapticForm)
            {
                case HapticForm.OnEnter:
                    controllerObject.hapticController.Haptic(_duration, _strength, _hapticStyle, _overwrite);
                    break;
                case HapticForm.DuringCollision:
                    duringCollision = true;
                    break;
            }
        }

        private void OnExit()
        {
            switch (_hapticForm)
            {
                case HapticForm.OnExit:
                    controllerObject.hapticController.Haptic(_duration, _strength, _hapticStyle, _overwrite);
                    break;
                case HapticForm.DuringCollision:
                    duringCollision = false;
                    break;
            }
        }

        private bool ControllerCheck(GameObject go)
        {
            return go.name.Contains("Controller");
        }

        private void OnCollisionEnter(Collision col)
        {
            if (ControllerCheck(col.gameObject))
            {
                if (_hapticEvent != ControllerEvent.Trigger)
                {
                    controllerObject.controller = col.gameObject;
                    OnEnter();
                }
            }
        }

        private void OnTriggerEnter(Collider col)
        {
            if (ControllerCheck(col.gameObject))
            {
                if (_hapticEvent != ControllerEvent.Collision)
                {
                    controllerObject.controller = col.gameObject;
                    OnEnter();
                }
            }
        }

        private void OnCollisionExit(Collision col)
        {
            if (ControllerCheck(col.gameObject))
            {
                if (_hapticEvent != ControllerEvent.Trigger)
                    OnExit();
            }
        }

        private void OnTriggerExit(Collider col)
        {
            if (ControllerCheck(col.gameObject))
            {
                if (_hapticEvent != ControllerEvent.Collision)
                    OnExit();
            }
        }

        public int strength
        {
            get { return _strength; }
            set { _strength = value; }
        }

        public float duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        public HapticForm hapticForm
        {
            get { return _hapticForm; }
            set { _hapticForm = value; }
        }

        public HapticStyle hapticStyle
        {
            get { return _hapticStyle; }
            set { _hapticStyle = value; }
        }

        public ControllerEvent hapticEvent
        {
            get { return _hapticEvent; }
            set { _hapticEvent = value; }
        }

        public bool overwrite
        {
            get { return _overwrite; }
            set { _overwrite = value; }
        }
    }
}