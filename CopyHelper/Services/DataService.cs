using System.IO;
using System.Text.Json;
using CopyHelper.Models;

namespace CopyHelper.Services;

public class DataService
{
    private readonly string _dataFilePath;

    public DataService()
    {
        var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var appFolder = Path.Combine(appDataFolder, "CopyHelper");
        Directory.CreateDirectory(appFolder);
        _dataFilePath = Path.Combine(appFolder, "entries.json");
    }

    public List<CopyEntry> LoadEntries()
    {
        if (!File.Exists(_dataFilePath))
        {
            return new List<CopyEntry>();
        }

        try
        {
            var json = File.ReadAllText(_dataFilePath);
            return JsonSerializer.Deserialize<List<CopyEntry>>(json) ?? new List<CopyEntry>();
        }
        catch
        {
            return new List<CopyEntry>();
        }
    }

    public void SaveEntries(List<CopyEntry> entries)
    {
        var json = JsonSerializer.Serialize(entries, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_dataFilePath, json);
    }
}
