using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Leadboard
{
    public class User : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI indexText;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI scoreText;

        public void SetInformation(int index, string userName, int score)
        {
            indexText.text = "#" + index.ToString();
            nameText.text = userName;
            scoreText.text = score.ToString();
        }
    }
}
