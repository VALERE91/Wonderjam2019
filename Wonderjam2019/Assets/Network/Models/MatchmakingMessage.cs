using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Network.Models
{
    [Serializable]
    public class MatchmakingPlayer
    {
        public uint Team { get; set; }

        public string Pseudo { get; set; }

        public PlayerRole Role { get; set; }
    }

    public enum PlayerRole
    {
        Cooker = 0,
        Waiter = 1,
        NumberOfRole = 2
    }
    [Serializable]
    public class MatchmakingResult : JamMessage
    {
        public string PlayerID { get; set; }

        public string SessionID { get; set; }

        public uint TeamID { get; set; }

        public PlayerRole Role { get; set; }

        public Dictionary<string, MatchmakingPlayer> Players { get; set; }
    }
}
