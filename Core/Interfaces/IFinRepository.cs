﻿using Core.DTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IFinRepository
    {
        Task<IEnumerable<UserDto>> GetUser(int id);
        Task<UserDto> GetUserById(int id);
        Task<string> AddUser(UserDto user);
        Task<string> JournalAddUpdateDeleteDto(JournalAddDto dd);
        Task<IEnumerable<JournalAddDto>> GetallJournalInfo();
    }
}
