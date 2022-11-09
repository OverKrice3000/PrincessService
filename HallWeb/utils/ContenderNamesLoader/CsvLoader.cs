using Csv;

namespace HallWeb.utils.ContenderNamesLoader;

public class CsvLoader : ITableLoader
{
    private string[]? _columns;

    private string _filepath;
    private char _separator = ',';

    public CsvLoader(string filepath)
    {
        _filepath = filepath;
    }

    public Dictionary<string, List<string>> Load()
    {
        string absoluteCsvPath = Path.Join(Util.GetProjectBaseDirectory(), this._filepath);
        if (!File.Exists(absoluteCsvPath))
        {
            throw new ArgumentException("Bad csv filepath!");
        }

        using (var reader = new StreamReader(File.Open(absoluteCsvPath, FileMode.Open)))
        {
            var lines = CsvReader.Read(reader);
            var csvColumns = lines.First().Headers;
            _columns ??= csvColumns;
            var csvIndices = _columns.Select(
                columnName => Array.FindIndex(csvColumns, csvColumnName => csvColumnName == columnName)).ToArray();
            var csvArrays = new List<string>[csvIndices.Length];
            for (int i = 0; i < csvIndices.Length; i++)
            {
                csvArrays[i] = new List<string>();
            }

            foreach (var line in lines)
            {
                for (int i = 0; i < csvIndices.Length; i++)
                {
                    csvArrays[i].Add(line[csvIndices[i]]);
                }
            }

            Dictionary<string, List<string>> table = new();
            for (int i = 0; i < _columns.Length; i++)
            {
                table.Add(this._columns[i], csvArrays[i]);
            }

            return table;
        }
    }

    public CsvLoader WithFilepath(in string filepath)
    {
        _filepath = filepath;
        return this;
    }

    public CsvLoader WithColumns(in string[] columns)
    {
        _columns = columns;
        return this;
    }

    public CsvLoader WithSeparator(char separator)
    {
        _separator = separator;
        return this;
    }
}