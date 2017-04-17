﻿using BossrApi.Models.Interfaces;
using NodaTime;
using System;

namespace BossrApi.Models.Entities
{
    public class Scrape : IScrape
    {
        public int Id { get; set; }
        public LocalDate Date { get; set; }
    }
}
