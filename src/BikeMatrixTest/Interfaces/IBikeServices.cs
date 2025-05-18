using BikeMatrixModels;

namespace BikeMatrixTest.Interfaces
{
    public interface IBikeServices
    {
        Task createBikeAsync(Bikes bike);
        Task<Bikes> GetBikeAsync(int BikeId);

        Task<Bikes> UpdateBikeAsync(Bikes newBike);
        Task<bool> DeleteBikeAsync(int bikesID);

        Task<IEnumerable<Bikes>> getAllBikesAsync();
    }
}