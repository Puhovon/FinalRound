using System;
using TMPro;
using Units.Player;
using UnityEngine;

namespace UI
{
    public class UIGame : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text hpText;
        [SerializeField] private TMP_Text ammoText;

        private int _currentScoreText;
        private int _currentHpText;
        private int _currentAmmoText;

        public void Instantiate(int maxAmmo)
        {
            _currentAmmoText = maxAmmo;
            _currentScoreText = Convert.ToInt32(scoreText.text);
            ammoText.text = _currentAmmoText.ToString();
            hpText.text = _currentHpText.ToString();
            
            UIEvents.onHealthChanged.AddListener(HealthChanged);
            UIEvents.onScoreChanged.AddListener(ScoreChanged);
            UIEvents.onAmmoChanged.AddListener(AmmoChanged);
        }

        private void ScoreChanged()
        {
            _currentScoreText += 1;
            scoreText.text = _currentScoreText.ToString();
        }

        private void HealthChanged(int health)
        {
            _currentHpText = health;
            hpText.text = _currentHpText.ToString();
        }

        private void AmmoChanged()
        {
            _currentAmmoText = PlayerAttack.currentAmmo;
            ammoText.text = _currentAmmoText.ToString();
        }
    }
}