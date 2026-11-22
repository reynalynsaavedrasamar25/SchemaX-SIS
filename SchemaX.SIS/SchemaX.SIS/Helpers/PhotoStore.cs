using System;
using System.Collections.Generic;
using System.Text;

namespace SchemaX.SIS.Helpers
{
    public static class PhotoStore
    {
        public const long MaxBytes = 5 * 1024 * 1024;   // 5 MB

        private static readonly string Folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "SchemaX", "Photos");

        // Copies the chosen file into our folder and returns the saved path.
        public static string Save(string sourceFile, string studentId)
        {
            Directory.CreateDirectory(Folder);
            string dest = Path.Combine(Folder, studentId + Path.GetExtension(sourceFile).ToLowerInvariant());
            File.Copy(sourceFile, dest, true);

            // remove an older photo of this student that has another extension
            foreach (string old in Directory.GetFiles(Folder, studentId + ".*"))
                if (!string.Equals(old, dest, StringComparison.OrdinalIgnoreCase))
                    File.Delete(old);
            return dest;
        }

        // Loads a copy (does not lock the file). Null if there is no usable photo.
        public static Image? Load(string? path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path)) return null;
            try
            {
                using var fs = File.OpenRead(path);
                using var img = Image.FromStream(fs);
                return new Bitmap(img);
            }
            catch { return null; }
        }
    }
}

