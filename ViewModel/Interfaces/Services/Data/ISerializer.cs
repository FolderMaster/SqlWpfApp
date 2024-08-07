﻿namespace ViewModel.Interfaces.Services.Data
{
    public interface ISerializer
    {
        byte[] Serialize(object value);

        T? Deserialize<T>(byte[] data);
    }
}
