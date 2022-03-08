using Orleans;

namespace SearchModels.Models
{
    public enum Folders
    {
        Documents,Musics,Videos,Desktop,Pictures
    }
    public interface IFileSearchGrain : IGrainWithStringKey
    {
        Task<FileSearchInfo[]> SearchFile(string Keyword);
        Task<FileSearchInfo[]> SearchFile(string Keyword,Folders Folder);
    }

    public class FileSearchInfo
    {
        public string FullName { get; set; }
        public string Name { get; set; }
        public string Directory { get; set; }
    }
}
