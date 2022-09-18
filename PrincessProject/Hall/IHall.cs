﻿using PrincessProject.model;

namespace PrincessProject.Hall;

/*
 * Defines hall, which is able to generate and give away contenders
 */
public interface IHall
{
    /*
     * Method, which returns number of candidates generated by hall
     */
    int GetTotalCandidates();

    /*
     * Method, which gives away next contender
     */
    VisitingContender GetNextContender();

    /*
     * Method, which compares contenders
     */
    VisitingContender AskFriendToCompareContenders(VisitingContender first, VisitingContender second);

    /*
     * Method, which makes hall regenerate contenders and set initial state
     */
    void Reset();

    /*
     * Method for princess to choose contender
     */
    int ChooseContender(VisitingContender visitingContender);

    void SaveAttempt(int happiness);
}