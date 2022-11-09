using PrincessProject.Data.model;

namespace HallWeb.utils.AttemptSaver;

public class FileAttemptSaver : IAttemptSaver
{
    public Task Save(Attempt attempt)
    {
        string projectPath = Util.GetProjectBaseDirectory();
        var outputDirectory = new DirectoryInfo(Path.Join(projectPath, Constants.FromProjectRootOutputFolderPath));
        if (!outputDirectory.Exists)
        {
            outputDirectory.Create();
        }

        int outputFilesExists = new DirectoryInfo(Path.Join(projectPath, Constants.FromProjectRootOutputFolderPath))
            .EnumerateFiles().Count();
        var nextFile = new FileInfo(Path.Join(projectPath, Constants.FromProjectRootOutputFolderPath,
            Util.DeriveOutputFileName(outputFilesExists)));
        using (var writer = new StreamWriter(nextFile.Create()))
        {
            writer.WriteLine(attempt.ContendersCount);
            Util.WriteSectionSeparator(writer);
            foreach (var candidate in attempt.Contenders)
            {
                writer.WriteLine(candidate.ToString());
            }

            if (attempt.ChosenContenderValue is null)
            {
                return Task.CompletedTask;
            }

            Util.WriteSectionSeparator(writer);
            writer.Write(attempt.ChosenContenderValue);

            return Task.CompletedTask;
        }
    }
}