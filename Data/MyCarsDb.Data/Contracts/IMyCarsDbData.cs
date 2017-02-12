namespace MyCarsDb.Data.Contracts
{
    using System.Threading.Tasks;

    using MyCarsDb.Data.Contracts.Repositories;    

    public interface IMyCarsDbData
    {
        IUsersDbRepository UsersRepository { get; }
        IRolesDbRepository RolesRepository { get; }
        IVehiclesDbRepository VehiclesRepository { get; }

        Task SaveChangesAsync();
    }
}
