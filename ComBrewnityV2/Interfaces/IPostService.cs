using ComBrewnityV2.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComBrewnityV2.Interfaces
{
    public interface IPostService
    {
        List<PostDto> GetAll();
    }
}
