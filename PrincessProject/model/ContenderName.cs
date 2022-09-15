﻿namespace PrincessProject.model;

public record ContenderName(string Name, string Surname)
{
    public override string ToString()
    {
        return Name + " " + Surname;
    }
}