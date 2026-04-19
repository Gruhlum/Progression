using HexTecGames.Basics.UI;

namespace HexTecGames.Progression.Stats.UI
{
    public class StatDisplayController : DisplayController<StatDisplay, Stat>
    {
        private void OnEnable()
        {
            SetItems(StatManager.Stats);
        }
    }
}