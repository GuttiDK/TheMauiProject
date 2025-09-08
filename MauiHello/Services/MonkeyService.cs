using System.Text.Json;
using MauiHello.Models;

namespace MauiHello.Services
{
    public class MonkeyService
    {
        public async Task<List<Monkey>> GetMonkeys()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            List<Monkey> monkeyList = JsonSerializer.Deserialize<List<Monkey>>(contents) ?? new List<Monkey>();
            return monkeyList;
        }
    }
}