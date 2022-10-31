using IdleBattler_Common.Enums.Arena;
using IdleBattler_Common.Models.Fighter;
using IdleBattler_Common.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleBattler_Common.Models.Arena
{
    public class ArenaFighterModel : ArenaItemLocation
    {
        public FighterModel Fighter { get; private set; }
        public bool InBattle { get; private set; }
        public Guid InBattleWith { get; private set; }

        public ArenaFighterModel(FighterModel fighter)
        {
            Fighter = fighter;
            this.XLocation = 0;
            this.YLocation = 0;
            this.VerticalMovementDirection = VerticalMovementDirection.Stationary;
            this.HorizontalMovementDirection = HorizontalMovementDirection.Stationary;
            this.InBattle = false;
            this.InBattleWith = Guid.Empty;
            SetHasSpawned(false);
        }

        public void SetLocation(ArenaItemLocation location)
        {
            this.XLocation = location.XLocation;
            this.YLocation = location.YLocation;
            this.VerticalMovementDirection = location.VerticalMovementDirection;
            this.HorizontalMovementDirection = location.HorizontalMovementDirection;
        }

        public void SetInBattle(bool inBattle, Guid battleWith)
        {
            this.InBattle = inBattle;
            this.InBattleWith = battleWith;
        }

        public static ArenaFighterModel Copy(ArenaFighterModel model)
        {
            var copy = new ArenaFighterModel(FighterModel.Copy(model.Fighter));
            copy.SetLocation(model);
            copy.InBattle = model.InBattle;
            copy.InBattleWith = model.InBattleWith;
            copy.SetHasSpawned(model.HasSpawned);
            return copy;
        }
    }
}
