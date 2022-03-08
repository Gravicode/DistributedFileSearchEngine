using Orleans;

namespace FileSearchEngine.Models
{
    public interface IFileSearchGrain : IGrainWithStringKey
    {
        Task<FileSearchInfo[]> SearchFile(string Keyword);
    }

    public class FileSearchInfo
    {
        public string FullName { get; set; }
        public string Name { get; set; }
        public string Directory { get; set; }
    }
}
