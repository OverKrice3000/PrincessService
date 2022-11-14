﻿using PrincessProject.Data.model;

namespace HallWeb.utils.AttemptSaver;

/*
 * Defines method, which saves program execution data to some source.
 */
public interface IAttemptSaver
{
    /*
     * Method, which saves program execution data to some source.
     */
    Task Save(Attempt attempt);
}