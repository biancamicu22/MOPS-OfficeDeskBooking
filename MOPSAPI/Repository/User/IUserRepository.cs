using DataLibrary.DTO;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MOPSAPI.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
    }
}
