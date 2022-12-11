using DataLibrary;

namespace MOPSAPI.Repository.Desk
{
    public class DeskRepository : BaseRepository<DataLibrary.Models.Desk>, IDeskRepository
    {
        public DeskRepository(MOPSContext context) : base(context)
        {
        }
    }
}
