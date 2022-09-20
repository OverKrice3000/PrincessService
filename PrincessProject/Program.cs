using PrincessProject.ContenderContainer;
using PrincessProject.ContenderGenerator;
using PrincessProject.Friend;
using PrincessProject.Hall;
using PrincessProject.PrincessClasses;
using PrincessProject.utils;
using PrincessProject.utils.AttemptLoader;
using PrincessProject.utils.ContenderNamesLoader;

var namesLoader = new CsvLoader(Constants.FromProjectRootCsvNamesFilepath)
    .WithSeparator(Constants.CsvNamesSurnamesSeparator)
    .WithColumns(new string[1] { Constants.CsvNamesColumn });
var surnamesLoader = new CsvLoader(Constants.FromProjectRootCsvSurnamesFilepath)
    .WithSeparator(Constants.CsvNamesSurnamesSeparator)
    .WithColumns(new string[1] { Constants.CsvSurnamesColumn });
var contenderGenerator = new ContenderGenerator(namesLoader, surnamesLoader);
var contenderContainer = new ContenderContainer(contenderGenerator, Constants.DefaultContendersCount);
var friend = new Friend(contenderContainer);
var attemptSaver = new FileAttemptSaver();
var hall = new Hall(contenderGenerator, friend, attemptSaver, contenderContainer);
var princess = new Princess(hall);

var happiness = princess.ChooseHusband();
hall.SaveAttempt(happiness);