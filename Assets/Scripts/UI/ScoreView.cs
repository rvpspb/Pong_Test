using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace pong.ui
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        public void SetScore(int value)
        {
            _scoreText.SetText(value.ToString("00"));
        }
    }
}
