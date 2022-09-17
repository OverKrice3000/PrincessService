using PrincessProject.ContenderGenerator;
using PrincessProject.Friend;
using PrincessProject.Hall;
using PrincessProject.model;
using PrincessProject.PrincessClasses;
using PrincessProject.utils;
using PrincessProject.utils.AttemptLoader;
using PrincessProject.utils.ContenderNamesLoader;

var namesLoader = new CsvLoader()
    .WithSeparator(';')
    .WithColumns(new string[1] { Constants.CsvNamesColumn })
    .WithFilepath(Constants.FromProjectRootCsvNamesFilepath);
var surnamesLoader = new CsvLoader()
    .WithSeparator(';')
    .WithColumns(new string[1] { Constants.CsvSurnamesColumn })
    .WithFilepath(Constants.FromProjectRootCsvSurnamesFilepath);
var contenderGenerator = new ContenderGenerator(namesLoader, surnamesLoader);
var friend = new Friend();
var attemptSaver = new FileAttemptSaver();
var hall = new Hall(contenderGenerator, friend, attemptSaver);
var princess = new Princess(hall);

var happiness = princess.ChooseHusband();
hall.SaveAttempt(happiness);
    