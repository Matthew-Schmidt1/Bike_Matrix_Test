using BikeMatrixModels;

namespace BikeMatrixTest.Interfaces
{
    public interface IBikeServices
    {
        Task createBikeAsync(Bikes bike);
        Task<Bikes> GetBikeAsync(int BikeId);

        Task<Bikes> UpdateBikeAsync(Bikes newBike);
        Task<bool> DeleteBikeAsync(Bikes bikes);

        Task<IEnumerable<Bikes>> getAllBikesAsync();
    }
}