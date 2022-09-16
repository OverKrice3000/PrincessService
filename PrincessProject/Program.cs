using PrincessProject.ContenderGenerator;
using PrincessProject.Friend;
using PrincessProject.Hall;
using PrincessProject.Princess.Strategy;
using PrincessProject.PrincessClasses;
using PrincessProject.PrincessClasses.Strategy;
using PrincessProject.PrincessClasses.Strategy.LargeNumbersLawStrategy;
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
var contenders = Constants.DefaultContendersCount;
var friend = new Friend();
var hall = new HallImpl(contenderGenerator, friend, contenders)
    .WithAttemptSaver(new FileAttemptSaver());
var princess = new Princess(hall);

var chosen = princess.ChooseHusband();
hall.ChooseContenderAndCalculateHappiness(chosen);
    