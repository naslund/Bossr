﻿using NodaTime;
using System;

namespace BossrApi.Models.Interfaces
{
    public interface IScrape
    {
        int Id { get; set; }
        LocalDate Date { get; set; }
    }
}
