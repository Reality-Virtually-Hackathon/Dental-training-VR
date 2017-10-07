using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ViveController
{
    public class HapticController : MonoBehaviour
    {
        private SteamVR_Controller.Device device;
        private SteamVR_TrackedObject sto;
        private int[] _strengths;
        private int _strength;
        private int _currentStrength;
        private float _duration;
        private float _currentDuration;
        private HapticStyle _hapticStyle;
        private bool useArray = false;
        public bool dismiss = false;

        ///<summary>
		///Haptic Feedback.
		///</summary>
        public void Haptic(float duration, int strength = 3999, HapticStyle hapticStyle = HapticStyle.Default, bool overwrite = true)
        {
            if (!(!overwrite && _currentDuration < _duration))
            {
                _currentDuration = 0;
                _strength = strength;
                _duration = duration;
                _hapticStyle = hapticStyle;
                useArray = false;
            }
        }

        ///<summary>
		///Haptic Feedback. Custom hapticStyle using strengths array spread evenly over duration.
		///</summary>
        public void Haptic(float duration, int[] strengths, bool overwrite = true)
        {
            if (!(!overwrite && _currentDuration < _duration))
            {
                _currentDuration = 0;
                _strengths = strengths;
                _duration = duration;
                useArray = true;
            }
        }

        ///<summary>
		///One frame of haptic feedback.
		///</summary>
        public void Haptic(int strength = 3999, bool overwrite = true)
        {
            device = SteamVR_Controller.Input((int)sto.index);
            device.TriggerHapticPulse((ushort)strength);
        }

        private void Start()
        {
            sto = GetComponent<SteamVR_TrackedObject>();
            device = SteamVR_Controller.Input((int)sto.index);
        }

        private void Update()
        {
            if (_currentDuration < _duration)
            {
                _currentDuration += Time.deltaTime;
                device = SteamVR_Controller.Input((int)sto.index);
                device.TriggerHapticPulse((ushort)currentStrength());
            }
        }

        private int currentStrength()
        {
            if (useArray)
            {
                return _strengths[(int)(_currentDuration / _duration) * _strengths.Length];
            }
            else
            {
                switch (_hapticStyle)
                {
                    case HapticStyle.Crescendo:
                        return (int)((_currentDuration / _duration) * 3999);
                    case HapticStyle.Diminuendo:
                        return 3999 - (int)((_currentDuration / _duration) * 3999);
                    default:
                        return _strength;
                }
            }
        }

        public float duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        public int strength
        {
            get { return _strength; }
            set { _strength = value; }
        }

        public int[] strengths
        {
            get { return _strengths; }
            set { _strengths = value; }
        }

        public HapticStyle hapticStyle
        {
            get { return _hapticStyle; }
            set { _hapticStyle = value; }
        }
    }
}