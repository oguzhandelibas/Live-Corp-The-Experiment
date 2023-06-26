using System;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

namespace Leadboard
{
    public class PlayfabManager : AbstractSingleton<PlayfabManager>
    {
        [SerializeField] private LeadboardController _leadboardController;
        private void Start()
        {
            Login();
        }
    
        private void Login()
        {
            var request = new LoginWithCustomIDRequest
            {
                CustomId = SystemInfo.deviceUniqueIdentifier,
                CreateAccount = true
            };
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
        }
    
        void OnLoginSuccess(LoginResult result)
        {
            Debug.Log("Successful login!");
            GetLeaderboard();
        }
        
        void OnError(PlayFabError error)
        {
            Debug.Log("Error while logging in account!");
            Debug.Log(error.GenerateErrorReport());
        }
    
        public void SendLeaderboard(int score)
        {
            var request = new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>{
                    new StatisticUpdate {
                        StatisticName = "UserScores",
                        Value = score
                    }
                }
            };
            PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
        }
    
        private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult obj)
        {
            Debug.Log("Success leaderboard send");
        }
    
        public void GetLeaderboard()
        {
            SendLeaderboard(TimeControl.Instance.GetBestTime());
            var request = new GetLeaderboardRequest
            {
                StatisticName = "UserScores",
                StartPosition = 0,
                MaxResultsCount = 10
            };
            PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
            
        }
    
        private void OnLeaderboardGet(GetLeaderboardResult result)
        {
            for (var i = 0; i < result.Leaderboard.Count; i++)
            {//DisplayName
                _leadboardController.SetUsersInformation(
                    result.Leaderboard[i].Position, 
                    result.Leaderboard[i].PlayFabId, 
                    result.Leaderboard[i].StatValue);

                /*if (result.Leaderboard[i].PlayFabId == playfabId)
                {
                    ourPlayer.SetInfo(result.Leaderboard[i].DisplayName, result.Leaderboard[i].StatValue, result.Leaderboard[i].Position, true);
                }*/
                
            }
        }
    }
}

