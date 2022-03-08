using Orleans;

namespace SearchModels.Models
{
    public class FileSearchGrain : Grain, IFileSearchGrain
    {


        public Task<FileSearchInfo[]> SearchFile(string Keyword)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return Task.FromResult(search(Keyword, path));
        }

        public Task<FileSearchInfo[]> SearchFile(string Keyword, Folders Folder)
        {
            string path = String.Empty;
            switch (Folder)
            {
                case Folders.Documents:
                    path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    break;   
                case Folders.Musics:
                    path = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                    break;
                case Folders.Desktop:
                    path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    break;
                case Folders.Videos:
                    path = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
                    break;
                case Folders.Pictures:
                    path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                    break;
            }
            return Task.FromResult(search(Keyword, path));
        }

        FileSearchInfo[] search(string Keyword, string PathFolder)
        {
            DirectoryInfo dir = new DirectoryInfo(PathFolder);
            if (dir.Exists)
            {
                var files = dir.GetFiles(Keyword);
                var datas = new List<FileSearchInfo>();
                foreach (var file in files)
                {
                    datas.Add(new FileSearchInfo() { Directory = file.DirectoryName, FullName = file.FullName, Name = file.Name });
                }
                return datas.ToArray();
            }
            return default;
        }
    }
}
