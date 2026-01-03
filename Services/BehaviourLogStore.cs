using Azure.Storage.Blobs;

namespace CatBreedingProgram.Services;

public class BehaviourLogStore : IBehaviourLogStore
{
    private readonly BlobServiceClient _blobServiceClient;
    public BehaviourLogStore(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task SaveTextAsync(string blobName, string content)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient("behaviour-logs");
        var blobClient = containerClient.GetBlobClient(blobName);

        using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
        Console.WriteLine($"[BehaviourLogStore] Saving behaviour log to blob: {blobName}");
        await blobClient.UploadAsync(stream, overwrite: true);
    }


}