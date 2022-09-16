namespace PrincessProject.utils.ContenderNamesLoader;

public class CsvLoader : ITableLoader
{
    
    private string? _filepath;
    private string[]? _columns;
    private char _separator = ',';

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
    
    public Dictionary<string, List<string>> Load()
    {
        if (this._filepath is null)
        {
            throw new ArgumentException("Csv filepath is not set!");
        }
        string absoluteCsvPath = Path.Join(Util.GetProjectBaseDirectory(), this._filepath);
        if (!File.Exists(absoluteCsvPath))
        {
            throw new ArgumentException("Bad csv filepath!");
        }

        using (var reader = new StreamReader(File.Open(absoluteCsvPath, FileMode.Open)))
        {
            var csvColumns = reader.ReadLine()?.Split(this._separator);
            if (csvColumns is null)
                throw new ArgumentException("Could not read csv file!");
            _columns ??= csvColumns;
            var csvIndices = _columns.Select(
                columnName => Array.FindIndex(csvColumns, csvColumnName => csvColumnName == columnName)).ToArray();
            var csvArrays = new List<string>[csvIndices.Length];
            for (int i = 0; i < csvIndices.Length; i++)
            {
                csvArrays[i] = new List<string>();
            }
            while (!reader.EndOfStream)
            {
                var nextStr = reader.ReadLine()?.Split(this._separator);
                if (nextStr is null)
                    break;
                for (int i = 0; i < _columns.Length; i++)
                {
                    csvArrays[i].Add(nextStr[csvIndices[i]]);
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
}