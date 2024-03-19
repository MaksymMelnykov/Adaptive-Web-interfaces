using Microsoft.AspNetCore.Mvc;

namespace Lab7.Services.Interfaces
{
    public interface IVersionedService
    {
        public int GetVersion1();
        public string GetVersion2();
        public byte[] GetVersion3();
    }
}
