﻿using HeroesStatTracker.Data.Models.Replays;
using HeroesStatTracker.Data.Queries.Replays;

namespace HeroesStatTracker.Core.ViewModels.RawData
{
    public class RawMatchTeamExperienceViewModel : RawDataBase<ReplayMatchTeamExperience>
    {
        public RawMatchTeamExperienceViewModel(IRawDataQueries<ReplayMatchTeamExperience> iRawDataQueries)
            : base(iRawDataQueries)
        { }
    }
}
