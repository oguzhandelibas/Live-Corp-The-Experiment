using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leadboard
{
    public class LeadboardController : MonoBehaviour
    {
        public User[] users;

        public void SetUsersInformation(int index, string userName, int score)
        {
            users[index].gameObject.SetActive(true);
            users[index].SetInformation(index+1, userName, score);
        }
    }
}
