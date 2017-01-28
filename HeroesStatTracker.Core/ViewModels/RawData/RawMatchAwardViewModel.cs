﻿using HeroesStatTracker.Data.Models.Replays;
using HeroesStatTracker.Data.Queries.Replays;

namespace HeroesStatTracker.Core.ViewModels.RawData
{
    public class RawMatchAwardViewModel : RawDataBase<ReplayMatchAward>
    {
        public RawMatchAwardViewModel(IRawDataQueries<ReplayMatchAward> iRawDataQueries)
            : base(iRawDataQueries)
        { }
    }
}
