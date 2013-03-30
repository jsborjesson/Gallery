<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" ViewStateMode="Disabled"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gallery</title>
    <link rel="stylesheet" type="text/css" href="Content/Styles/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="Content/Styles/bootstrap-fileupload.css" />
    <link rel="stylesheet" type="text/css" href="Content/Styles/main.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="Content">
        <%-- Header --%>
        <h1>Gallery of Ballery</h1>

        <%-- View image --%>
        <asp:Panel ID="MainImagePanel" runat="server">
            <asp:Image 
                ID="MainImage" 
                CssClass="img-polaroid"
                ImageUrl="~/Content/Images/Forest Flowers.jpg"
                runat="server" 
                Visible="false"/>
        </asp:Panel>

        <%-- Image thumbs --%>
        <asp:Panel ID="ThumbsPanel" runat="server">
            <asp:ListView ID="ThumbsListView" runat="server" OnItemDataBound="ThumbsListView_ItemDataBound">
               <ItemTemplate>
                   <%-- Construct query-string --%>
                   <asp:HyperLink
                       ID="ThumbHyperLink" 
                       runat="server" 
                       CssClass="thumbnail"
                       NavigateUrl='<%#String.Format("?img={0}", Container.DataItem) %>'>
                       <asp:Image ID="ThumbImage"
                           runat="server" 
                           ImageUrl='<%#String.Format("~/Content/Images/Thumbs/{0}", Container.DataItem) %>'>
                       </asp:Image>
                   </asp:HyperLink>
               </ItemTemplate>

            </asp:ListView>
        </asp:Panel>

        <%-- Uploading --%>
        <asp:Panel ID="ImageUploadPanel" runat="server">
            <fieldset>
                <legend>Upload an image</legend>

                <%-- Bootstrap fileupload plugin --%>
                <div id="ImageUploadPluginDiv" class="fileupload fileupload-new" data-provides="fileupload">
                    <div class="input-append">
                    <div class="uneditable-input span3">
                        <i class="icon-file fileupload-exists"></i> 
                        <span class="fileupload-preview"></span>
                    </div>
                        <span class="btn btn-file">
                            <span class="fileupload-new">Select file</span>
                            <span class="fileupload-exists">Change</span>

                            <%-- Actual fileupload control! --%>
                            <asp:FileUpload ID="ImageFileUpload" runat="server" />
                        </span>
                        <a href="#" class="btn fileupload-exists" data-dismiss="fileupload">Remove</a>
                    </div>
                </div>
        

                <asp:RequiredFieldValidator 
                    ID="ImageUploadRequiredFieldValidator" 
                    runat="server" 
                    ErrorMessage="You must choose a file to upload."
                    ControlToValidate="ImageFileUpload"
                    Display="Dynamic"
                    CssClass="label label-important">
                </asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator 
                    ID="ImageUploadRegularExpressionValidator" 
                    runat="server"
                    ErrorMessage="Only .gif, .jpg and .png files are permitted."
                    ControlToValidate="ImageFileUpload"
                    ValidationExpression="^.*.(gif|jpg|png)$"
                    Display="Dynamic"
                    CssClass="label label-important">
                </asp:RegularExpressionValidator>

                <asp:Button 
                    ID="ImageUploadButton" 
                    OnClick="ImageUploadButton_Click" 
                    runat="server" 
                    Text="Upload"
                    CssClass="btn btn-success" />
            </fieldset>

            <asp:Panel 
                ID="UploadMessagePanel" 
                runat="server"
                Visible="false">
                <asp:Label ID="UploadMessageLabel" runat="server" />
            </asp:Panel>
        </asp:Panel>
    </div>
    </form>

    <%-- Include JavaScript files --%>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script src="JavaScript/bootstrap.js"></script>
    <script src="JavaScript/bootstrap-fileupload.js"></script>
    <script src="JavaScript/main.js"></script>
</body>
</html>
