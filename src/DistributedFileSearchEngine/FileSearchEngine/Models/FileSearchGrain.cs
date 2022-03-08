using Orleans;

namespace FileSearchEngine.Models
{
    public class FileSearchGrain : Grain, IFileSearchGrain
    {
       

        public Task<FileSearchInfo[]> SearchFile(string Keyword)
        {
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            DirectoryInfo dir = new DirectoryInfo (path);
            if (dir.Exists)
            {
                var files = dir.GetFiles(Keyword);
                var datas = new List<FileSearchInfo>();
                foreach(var file in files)
                {
                    datas.Add(new FileSearchInfo() { Directory = file.DirectoryName, FullName = file.FullName, Name = file.Name });
                }
                return Task.FromResult( datas.ToArray());
            }
            return default;
        }
    }
}
