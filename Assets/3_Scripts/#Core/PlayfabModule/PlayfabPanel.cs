using UnityEngine;

namespace Leadboard
{
    public class PlayfabPanel : View
    {
        [SerializeField] private PlayfabManager playfabManager;

        public void _SendLeaderboard()
        {
            playfabManager.SendLeaderboard(2);
        }

        public void _MainMenu()
        {
            UIManager.Instance.Show<MainMenuPanel>();
        }

        public void _GetLeaderboard()
        {
            playfabManager.GetLeaderboard();
        }
    }
}
