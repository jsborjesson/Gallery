using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{

    // Display an alert that fades away after a few seconds (handled by JS)
    private void DisplayAlert(string text, string alertClass)
    {
        UploadMessagePanel.Visible = true;
        UploadMessagePanel.CssClass = String.Format("alert alert-{0}", alertClass);
        UploadMessageLabel.Text = text;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // TODO Caching and breakout to DAL?

        // Interpret query-string and set images
        var fileName = Request.QueryString["img"];

        // If image is found
        if (Gallery.ImageExists(fileName))
        {
            // Display that image
            MainImage.Visible = true;
            MainImage.ImageUrl = "~/Content/Images/" + fileName; 

            // Display success-message if the image was just uploaded
            if (Request.QueryString["uploaded"] == "true")
            {
                DisplayAlert(String.Format("The image '{0}' was uploaded successfully!", fileName), "success");
            }  
        }
        // If an upload has failed, display error message
        else if (Request.QueryString["uploaded"] == "false")
        {
            // Display success message
            DisplayAlert(String.Format("The image '{0}' was not uploaded!", fileName), "error");
        }
        // Default action
        else
        {
            // TODO Display first image?

        }



        // Set the thumbnails datasource
        var ga = new Gallery();
        var names = ga.GetImageNames();
        ThumbsListView.DataSource = names;
        ThumbsListView.DataBind();

    }

    /// <summary>
    /// Upload an image to the server.
    /// </summary>
    protected void ImageUploadButton_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            if (ImageFileUpload.HasFile)
            {
                try
                {
                    var fileName = Gallery.SaveImage(ImageFileUpload.FileContent, ImageFileUpload.FileName);
                    Response.Redirect("?img=" + fileName + "&uploaded=true", false); // Second parameter false is to prevent a ThreadAbortException being thrown
                }
                catch (Exception)
                {
                    Response.Redirect("?img=" + ImageFileUpload.FileName + "&uploaded=false", false);
                }
            }
        }
    }

    protected void ThumbsListView_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        // Get the img-attribute
        var fileName = Request.QueryString["img"];
        
        if (fileName == e.Item.DataItem.ToString())
        {
            // Get the hyperlink
            var hyperLink = e.Item.FindControl("ThumbHyperLink") as HyperLink;  

            // Set the css-class
            hyperLink.CssClass = "thumbnail thumbnail_selected";
        }
        
    }
}