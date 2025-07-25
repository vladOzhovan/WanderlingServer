namespace Wanderling.Application.Interfaces
{
    public interface IPlantRecognitionService
    {
        Task<string> RecognizePlantAsync(byte[] plantImage);
    }
}
