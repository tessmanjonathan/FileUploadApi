using System.IO;
using System.Threading.Tasks;

public class FileUploadService : IFileUploadService
{
    public async Task<string> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return "No file uploaded.";

        using (var stream = new FileStream(GetUploadPath(file.FileName), FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return "File uploaded successfully.";
    }

    private string GetUploadPath(string fileName)
    {
        var uploadFolder = "uploads";
        if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), uploadFolder)))
        {
            Directory.CreateDirectory(uploadFolder);
        }

        return Path.Combine(Directory.GetCurrentDirectory(), uploadFolder, fileName);
    }
}
