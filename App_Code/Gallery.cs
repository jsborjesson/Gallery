using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Summary description for Gallery
/// </summary>
public class Gallery
{
    /// <summary>
    /// The approved extensions for an image to have.
    /// </summary>
    private static readonly Regex ApprovedExtensions;

    /// <summary>
    /// Path to the application
    /// </summary>
    public static string PhysicalApplicationPath { get; set; }

    /// <summary>
    /// The filenames of images sorted in alphabetical order.
    /// </summary>
    /// <returns>A list of the names.</returns>
    public List<string> GetImageNames() 
    {
        // Collect filenames
        // TODO Property for the entire path?
        var di = new DirectoryInfo(Gallery.PhysicalApplicationPath + @"Content\Images\");

        // Select the filenames and sort them in alphabetical order
        var files = di.GetFiles()
            .Select(fi => fi.Name)
            .OrderBy(fi => fi)
            .ToList();

        return files;
    }

    /// <summary>
    /// Checks if an image with the specified name exists in the uploads.
    /// </summary>
    /// <param name="name">Filename of the image to be checked for.</param>
    /// <returns>True if the image exists, else false.</returns>
    public static bool ImageExists(string name) 
    {
        return File.Exists(PhysicalApplicationPath + @"Content\Images\" + name);
    }

    /// <summary>
    /// Checks if contents of a file really is an image.
    /// </summary>
    /// <param name="image">The image to check.</param>
    /// <returns>True if the image is valid, else false.</returns>
    private static bool IsValidImage(Image image) 
    {
        // Compare the image's MIME-type to the allowed ones.
        if (image.RawFormat.Guid == ImageFormat.Jpeg.Guid ||
            image.RawFormat.Guid == ImageFormat.Gif.Guid ||
            image.RawFormat.Guid == ImageFormat.Png.Guid)
        {
            return true;
        }

        return false;
            
    }

    /// <summary>
    /// Uploads an image to the server.
    /// </summary>
    /// <param name="stream">The stream with the image.</param>
    /// <param name="fileName">The image's filename.</param>
    /// <returns>The image's new filename.</returns>
    public static string SaveImage(Stream stream, string fileName) 
    {

        // Read image from stream.
        var image = System.Drawing.Image.FromStream(stream);

        // Check file-ending
        if (!ApprovedExtensions.IsMatch(fileName)) 
        {
            throw new ArgumentException("Invalid filename.");
        }

        // Check if the image is of a valid format
        if (!IsValidImage(image)) 
        {
            throw new ArgumentException("Invalid MIME-type of image.");
        }
        

        // Check unique filename, else fileName (index)
        if (ImageExists(fileName)) 
        {
            int counter = 2;
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);
            
            while (ImageExists(fileName))
            {
                // Create new filename
                fileName = String.Format("{0}({1}){2}", fileNameWithoutExtension, counter++, extension);
            }
        }
        
        // Save file
        image.Save(PhysicalApplicationPath + @"Content\Images\" + fileName);

        // Create and save thumbnail
        var thumb = image.GetThumbnailImage(50, 50, null, System.IntPtr.Zero);
        thumb.Save(PhysicalApplicationPath + @"Content\Images\Thumbs\" + fileName);

        // returnera nya filnamnet
        return fileName;
    }

	static Gallery()
	{
        ApprovedExtensions = new Regex("^.*.(gif|jpg|png)$");
	}
}