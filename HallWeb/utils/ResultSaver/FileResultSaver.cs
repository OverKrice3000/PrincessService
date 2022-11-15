using PrincessProject.Data.model.data;

namespace HallWeb.utils.ResultSaver;

public class FileResultSaver : IResultSaver
{
    public Task Save(Result result)
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
            writer.WriteLine(result.ContendersCount);
            Util.WriteSectionSeparator(writer);
            foreach (var candidate in result.Contenders)
            {
                writer.WriteLine(candidate.ToString());
            }

            Util.WriteSectionSeparator(writer);
            writer.Write(result.ChosenContenderValue);

            return Task.CompletedTask;
        }
    }
}