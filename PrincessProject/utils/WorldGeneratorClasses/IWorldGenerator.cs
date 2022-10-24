﻿namespace PrincessProject.utils.WorldGeneratorClasses;

/// <summary>
/// Defines method, which generates set of attempts
/// </summary>
public interface IWorldGenerator
{
    /// <summary>
    /// Method, which generates set of attempts
    /// </summary>
    /// <param name="attempts">Amount of attempts to generate</param>
    void GenerateWorld(int attempts);
}