﻿using HeroesStatTracker.Data.Models.Replays;
using HeroesStatTracker.Data.Queries.Replays;

namespace HeroesStatTracker.Core.ViewModels.RawData
{
    public class RawMatchPlayerTalentViewModel : RawDataBase<ReplayMatchPlayerTalent>
    {
        public RawMatchPlayerTalentViewModel(IRawDataQueries<ReplayMatchPlayerTalent> iRawDataQueries)
            : base(iRawDataQueries)
        { }
    }
}
