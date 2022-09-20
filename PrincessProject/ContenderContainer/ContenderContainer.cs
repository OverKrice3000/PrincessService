﻿using PrincessProject.ContenderGenerator;
using PrincessProject.model;
using PrincessProject.utils;

namespace PrincessProject.ContenderContainer;

public class ContenderContainer : IContenderContainer
{
    private readonly IContenderGenerator _generator;

    public ContenderContainer(IContenderGenerator generator, int initialSize = Constants.DefaultContendersCount)
    {
        _generator = generator;
        var random = new Random();
        Contenders = _generator.Generate(initialSize)
            .OrderBy(item => random.Next())
            .ToArray();
    }

    public Contender[] Contenders { get; private set; }

    public void Reset(in int size = Constants.DefaultContendersCount)
    {
        var random = new Random();
        Contenders = _generator.Generate(size)
            .OrderBy(item => random.Next())
            .ToArray();
    }

    public Contender this[int index] => Contenders[index];
}