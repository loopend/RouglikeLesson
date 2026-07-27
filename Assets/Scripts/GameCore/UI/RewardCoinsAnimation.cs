using System.Collections;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.GameCore.UI
{
    [RequireComponent(typeof(AudioSource))]
    public class RewardCoinsAnimation : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        private float _targetTimer;
        private const float AnimationDuration = 2.4f;
        public void ActivateAnimation(float targetValue, float currentValue, TMP_Text text)
        {
            StartCoroutine(routine: Animate(targetValue, currentValue, text));
        }

        private IEnumerator Animate(float targetValue, float currentValue, TMP_Text text)
        {
            StartCoroutine(routine: PichSound());
            float rate = Mathf.Abs(f:targetValue -  currentValue) / AnimationDuration;
            while (Mathf.Abs(f: targetValue - currentValue) > 0.1f)
            {
                currentValue = Mathf.MoveTowards(currentValue, targetValue, maxDelta:rate * Time.deltaTime);
                text.text = Mathf.FloorToInt(currentValue).ToString();
                yield return null;
            }
        }
         private IEnumerator PichSound()
        {
            _targetTimer = 0;
            _audioSource.pitch = 1f;
            while (_targetTimer <= 2.4f)
            {
                _audioSource.Play();
                _audioSource.pitch += 0.1f;
                _targetTimer += Time.deltaTime;
                yield return null;
            }
        }



    }
}