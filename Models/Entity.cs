﻿namespace ColorPaletteGeneratorApi.Models
{
    public abstract class Entity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}