﻿using Microsoft.Extensions.Primitives;

namespace CardStorageService
{
    public interface IConfiguration
    {
        string this[string key] { get; set; }
        IConfigurationSection GetSection(string key);
        IEnumerable<IConfigurationSection> GetChildren();
        IChangeToken GetReloadToken();
    }
}
