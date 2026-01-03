using System.Threading.Tasks;
using CatBreedingProgram.Models;
using CatBreedingProgram.Services;

public class BehaviourService : IBehaviourService
{
    private readonly IBehaviourLogStore _behaviourLogStore;
    public BehaviourService(IBehaviourLogStore behaviourLogStore)
    {
        _behaviourLogStore = behaviourLogStore;
    }
    public async Task LogBehaviour(BehaviourLogRequest behaviourLogRequest)
    {
        var todayDate = DateTime.UtcNow.ToString("dd-MM-yyyy");
        var behaviourLog = new BehaviourLog
        {
            CatName = behaviourLogRequest.CatName,
            BehaviourScore = behaviourLogRequest.BehaviourScore,
            BehaviourDescription = behaviourLogRequest.BehaviourDescription,
            DateLogged = todayDate
        };

        var blobStoragePath = $"behaviour-logs/{todayDate}/{behaviourLog.CatName}.txt";

        var content = $"Cat Name: {behaviourLog.CatName}\n" +
                      $"Behaviour Score: {behaviourLog.BehaviourScore}\n" +
                      $"Behaviour Description: {behaviourLog.BehaviourDescription}\n" +
                      $"Date Logged: {behaviourLog.DateLogged}\n";

        await _behaviourLogStore.SaveTextAsync(blobStoragePath, content);

        Console.WriteLine($"[BehaviourService] Behaviour logged for cat {behaviourLog.CatName} on {behaviourLog.DateLogged}: Score {behaviourLog.BehaviourScore}, Description: {behaviourLog.BehaviourDescription} to path {blobStoragePath}");
    }
}