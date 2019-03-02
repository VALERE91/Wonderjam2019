using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Network.Models
{
    public enum MessageType
    {
        MatchmakingResult = 0,
        TextChatMessage = 1,
        ReplicationMessage = 2,
        EventMessage = 3
    }

    [Serializable]
    public class JamMessage
    {
        public MessageType Type { get; set; }
    }
}
