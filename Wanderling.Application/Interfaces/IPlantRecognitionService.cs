namespace Wanderling.Application.Interfaces
{
    public interface IPlantRecognitionService
    {
        Task<string> IdentifyPlantAsync(byte[] plantImage);
    }
}
