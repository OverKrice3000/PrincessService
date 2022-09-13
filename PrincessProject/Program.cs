using PrincessProject.ContenderGenerator;
using PrincessProject.Friend;
using PrincessProject.Hall;
using PrincessProject.Princess.Strategy;
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
var contenderGenerator = new FromNamesSurnamesContenderGenerator()
    .WithNamesLoader(namesLoader)
    .WithSurnamesLoader(surnamesLoader);
var contenders = Constants.DefaultContendersCount;
var friend = new FriendImpl();
var hall = new HallImpl(contenderGenerator, friend, contenders)
    .WithAttemptSaver(new FileAttemptSaver());
IStrategy strategy = contenders < Constants.ManyCandidatesStrategyCandidatesLowerBorder ?
    new CandidatePositionAnalysisStrategy(hall, contenders) : new LargeNumbersLawStrategy(hall, contenders);
var princess = new Princess(hall).WithStrategy(strategy);

var chosen = princess.MakeAssessment();
hall.ChooseContenderAndCalculateHappiness(chosen);
    