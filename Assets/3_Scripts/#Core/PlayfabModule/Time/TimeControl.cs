using UnityEngine;

namespace Leadboard
{
    public class TimeControl : AbstractSingleton<TimeControl>
    {
        [SerializeField] private TimeData _timeData;
        private float startTime;
        public float ElapsedTime;

        private void Start()
        {
            startTime = Time.time;
        }

        private void Update()
        {
            ElapsedTime = Time.time - startTime;
        }
        
        private void OnDisable()
        {
            if (_timeData.BestTime < ElapsedTime)
            {
                _timeData.BestTime = ElapsedTime;
                PlayfabManager.Instance.SendLeaderboard((int)ElapsedTime);
            }
        }

        public int GetBestTime()
        {
            return (int)_timeData.BestTime;
        }
    }
}
