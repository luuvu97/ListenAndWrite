<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateLesson.aspx.cs" Inherits="ListenAndWrite.CreateLesson" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/MyScript/SubmitAudio.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Add new lesson</h1>
            <asp:Button ID="btnRefresh" runat="server" Text="Refresh for another lesson" OnClick="btnRefresh_Click" />
            <asp:TextBox ID="lblNewLessonID" runat="server" Visible="true"></asp:TextBox>
            <asp:Button ID="btnGetExistLesson" runat="server" OnClick="btnGetExistLesson_Click" Text="Get this lesson" />
            <br />
            Title:
                    <asp:TextBox ID="txtTitle" runat="server" Width="100%"></asp:TextBox>
            <br />
            <asp:Button ID="btnMoveToSubmitAudio" runat="server" OnClick="btnMoveToSubmitAudio_Click" Visible="true" Text="Now insert track for this lesson" />
            <br />
            Level:
            <asp:TextBox ID="txtLevel" runat="server" TextMode="Number" Text="1"></asp:TextBox>
            <br />
            Description:
                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5" Width="100%"></asp:TextBox>
            <br />
            <asp:Button ID="btnNewLesson" runat="server" Text="Create new lesson" OnClick="btnNewLesson_Click" />

            <h2>Category list in db:</h2>
            <asp:ListView ID="_ViewCategory" runat="server" ItemType="ListenAndWrite.ModelIdentify.LessonCategory" SelectMethod="_ViewCategory_GetData">
                <ItemTemplate>
                    <%# Item.Category.CategoryName %>
                </ItemTemplate>
                <ItemSeparatorTemplate>+ </ItemSeparatorTemplate>
            </asp:ListView>
            <br />
            Category:
                    <asp:DropDownList ID="drCategory" runat="server"></asp:DropDownList>
            OR
                    <asp:TextBox ID="txtNewCategory" runat="server" placeholder="Input here and create new"></asp:TextBox>
            <asp:Button ID="btnNewCategory" runat="server" OnClick="btnNewCategory_Click" Text="Create new category" />
            <br />
            <asp:HiddenField ID="txtCategory" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="txtCategoryNameData" runat="server"></asp:HiddenField>
            <br />
            CategoryNameSelected:
                    <asp:Label ID="txtCategoryName" runat="server"></asp:Label>
            <script>
                    var drCategeory = document.getElementById("drCategory");
                    var txtCategory = document.getElementById("txtCategory");
                    var txtCategoryName = document.getElementById("txtCategoryName");
                    var txtCategoryNameData = document.getElementById("txtCategoryNameData");

                    txtCategoryName.innerText = txtCategoryNameData.value;

                    drCategeory.onchange = function () {
                        var val = drCategeory.value;
                        console.log(txtCategory.innerText.indexOf(val));
                        if (txtCategory.value.indexOf(val) === -1) {
                            if (txtCategory.value != "") {
                                txtCategory.value += "+";
                                txtCategoryNameData.value += "+";
                            }
                            txtCategory.value += drCategeory.value;
                            txtCategoryNameData.value += drCategeory.options[drCategeory.selectedIndex].text;
                            txtCategoryName.innerText = txtCategoryNameData.value;
                        }
                    }
                
            </script>
            <br />
            <asp:Button ID="btnAddLessonCategory" runat="server" Text="Add category into this lesson" OnClick="btnAddLessonCategory_Click" />
        </div>
    </form>
</body>
</html>
