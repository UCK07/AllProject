namespace AllProject.Server.Helper
{
    public class AppStateManager
    {
        public Task DirectoryControl(string Root)
        {
            var directoryPath = Path.Combine(Root);
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
            return Task.CompletedTask;
        }

        public Task<string> PrepareUniqueImageName(string fileName)
        {
            var randomName = Path.GetRandomFileName();
            var extension = Path.GetExtension(fileName);
            var newFileName = Path.ChangeExtension(randomName, extension);
            return Task.FromResult(newFileName);
        }

        //seknron kalabilir
        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
    }
}
