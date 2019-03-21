﻿using System;

namespace GF.DillyDally.Wpf.Client.Core.Navigator
{
    public interface INavigationTargetProvider
    {
        INavigationTarget FindNavigationTargetWithName(string navigationTargetName);

        INavigationTarget FindNavigationTargetWithKey(Guid navigationTargetId);

        void RegisterTarget(INavigationTarget target);
    }
}