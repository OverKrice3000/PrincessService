﻿using PrincessProject.Friend;
using PrincessProject.model;

namespace PrincessProject.Hall;

/// <summary>
/// Defines hall, which is able to generate and give away contenders
/// </summary>
public interface IHall
{
    /// <summary>
    /// Readonly friend property
    /// </summary>
    public IFriend Friend { get; }

    /// <summary>
    /// Method, which returns number of candidates generated by hall
    /// </summary>
    /// <returns>number of candidates generated by hall</returns>
    int GetTotalCandidates();

    /// <summary>
    /// Method, which gives away next contender
    /// </summary>
    /// <returns>next contender</returns>
    VisitingContender GetNextContender();

    /// <summary>
    /// Method, which makes hall regenerate contenders and set initial state
    /// </summary>
    void Reset();

    /// <summary>
    /// Method for princess to choose contender
    /// </summary>
    /// <param name="visitingContender">chosen contender</param>
    /// <returns>value of chosen contender</returns>
    int ChooseContender(VisitingContender visitingContender);

    /// <summary>
    /// Method, which saves an execution record to some source
    /// </summary>
    /// <param name="happiness">happiness of princess</param>
    void SaveAttempt(int happiness);
}