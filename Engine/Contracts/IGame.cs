﻿using System;
using System.Collections.Generic;

namespace Engine.Contracts
{
    public interface IGame
    {
        void Start();
        IScene Scene { get; }
        ICalendar Calendar { get; } 
        void _KeyPressed(string key);
        event EventHandler KeyPressed;
        event EventHandler SendMessage;
    }

    public interface ICalendar : IPlayable
    {
        object Today { get; }
        int DayFromStart { get; }
        int Year { get; }
        int DayInYear { get; }
        List<object > Moons { get; }
        void NextDay();
        void AttachScene(IScene scene);
        void DetachScene(IScene scene);
    }
}