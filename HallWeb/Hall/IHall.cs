﻿using PrincessProject.Data.model;

namespace HallWeb.Hall;

/// <summary>
/// Defines hall, which is able to generate and give away contenders
/// </summary>
public interface IHall
{
    /// <summary>
    /// Method, which returns number of candidates generated by hall
    /// </summary>
    /// <returns>number of candidates generated by hall</returns>
    int GetTotalCandidates();

    /// <summary>
    /// Method, which gives away next contender for an attempt id
    /// </summary>
    /// <returns>next contender</returns>
    VisitingContender GetNextContender(int attemptId);

    /// <summary>
    /// Method, which makes hall regenerate contenders for an attempt id and set initial state
    /// </summary>
    void Reset(int attemptId);

    /// <summary>
    /// Method for princess to choose contender for an attempt id
    /// </summary>
    /// <param name="attemptId">attempt id</param>
    /// <returns>value of chosen contender</returns>
    int ChooseContender(int attemptId);
}