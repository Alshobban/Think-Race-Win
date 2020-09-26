using Photon.Realtime;
using Utilities;

namespace SceneSpecific.RoomSetup
{
    public class PlayerListController : ScrollListController<Player>
    {
        protected override string GetLineText(Player sourceObject)
        {
            return sourceObject.NickName;
        }
    }
}