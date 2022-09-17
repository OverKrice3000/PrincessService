using PrincessProject.model;

namespace PrincessProject.utils.AttemptLoader;

public class FileAttemptSaver : IAttemptSaver
{
    public void Save(Attempt attempt)
    {
        string projectPath = Util.GetProjectBaseDirectory();
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
            Util.WriteSectionSeparator(writer);
            writer.Write(attempt.Happiness);
        }
    }
}