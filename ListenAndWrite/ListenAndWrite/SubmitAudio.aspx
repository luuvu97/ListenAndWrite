<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubmitAudio.aspx.cs" Inherits="ListenAndWrite.SubmitAudio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/MyScript/SubmitAudio.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server">
            <Scripts>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Name="respond" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
            </Scripts>
        </asp:ScriptManager>
        <%-- left --%>
        <div style="width: 50%; float: left;">
            <h1>Prepare</h1>
            <audio controls loop autoplay id="audioControl" style="width: 80%">
                Your browser does not support the audio element.
            </audio>
            <asp:HiddenField ID="audioPath" runat="server" />
            <table>
                <tr>
                    <td>Audio File:</td>
                    <td>
                        <input type="file" id="audioFile" />
                    </td>
                </tr>
                <tr>
                    <td>Transcript File:</td>
                    <td>
                        <input type="file" id="scripFile" />
                    </td>
                </tr>
            </table>

            <h1 id="fileName"></h1>

            <input type="button" id="btnPrev" value="Prev" />
            <asp:TextBox ID="txtCurPart" runat="server" TextMode="Number"></asp:TextBox>
            <input type="button" id="btnNext" value="Next" />
            <br />
            PlayFrom: <asp:TextBox ID="playFrom" runat="server" Text="2" ></asp:TextBox>
            To:<asp:TextBox ID="playTo" runat="server" Text="10" ></asp:TextBox>
            <br />
            <b>Script:</b>
            <br />
            <asp:TextBox ID="txtCurScript" runat="server" TextMode="MultiLine" Rows="5" Width="80%"></asp:TextBox>
            <br />
            <b>Multiple section script</b>
            <br />
            <asp:Label ID="txtMulScript" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Button ID="btnAdd" runat="server" Text="Add this track" OnClick="btnAdd_Click"/>
            <script>
                var scripts;
                var curPart = 0;
                var txtCurPart = document.getElementById("txtCurPart");
                var txtCurScript = document.getElementById("txtCurScript");
                var playFrom = document.getElementById("playFrom");
                var playTo = document.getElementById("playTo");
                var audioControl = document.getElementById("audioControl");
                var audioFile = document.getElementById("audioFile");
                var btnPrev = document.getElementById("btnPrev");
                var btnNext = document.getElementById("btnNext");
                var audioPath = document.getElementById("audioPath");
                var txtMulScript = document.getElementById("txtMulScript");

                function readSingleFile(e) {
                    var file = e.target.files[0];
                    if (!file) {
                        return;
                    }
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        scripts = new Transcript(e.target.result);
                        displayContents();
                    };
                    reader.readAsText(file);
                }
                function displayContents() {
                    
                    if (curPart < 0) {
                        curPart = 0;
                    } else if (curPart >= scripts.transcripts.length) {
                        curPart = scripts.transcripts.length - 1;
                    }

                    playFrom.value = scripts.getCurStart(curPart);
                    playTo.value = scripts.getCurEnd(curPart);
                    txtCurScript.value = scripts.getCurScript(curPart);
                    txtCurPart.value = curPart + 1;
                    txtMulScript.innerText = scripts.getMulScript(curPart);
                    audioControl.onloadstart();
                }

                document.getElementById('scripFile').addEventListener('change', readSingleFile, false);

                audioFile.onchange = function () {
                    audioControl.src = audioFile.value;
                    audioPath.value = audioFile.value;
                }

                audioControl.ontimeupdate = function () {
                    if (audioControl.currentTime >= playTo.value) {
                        audioControl.currentTime = playFrom.value;
                    }
                }

                audioControl.onloadstart = function () {
                    audioControl.currentTime = playFrom.value;
                }

                btnPrev.onclick = function () {
                    curPart -= 1;
                    displayContents();
                }

                btnNext.onclick = function () {
                    curPart += 1;
                    displayContents();
                }

            </script>
        </div>
        <%-- left --%>
        <%-- right --%>
        <div style="width: 50%; float: right;">
            <h1>Add new lesson</h1>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="click" />
                </Triggers>
                <ContentTemplate>
                    Title:
                    <asp:TextBox ID="txtTitle" runat="server" Width="100%"></asp:TextBox>
                    <br />
                    Length:
                    <asp:Label ID="lblLenght" runat="server" ></asp:Label>
                    <br />
                    Level: <asp:TextBox ID="txtLevel" runat="server" TextMode="Number" Text="1"></asp:TextBox>
                    <br />
                    Description:
                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5" Width="100%"></asp:TextBox>
                    <br />

                    <h2>Category list in db:</h2>
                    <asp:ListView ID="_ViewCategory" runat="server" ItemType="ListenAndWrite.ModelIdentify.LessonCategory" SelectMethod="_ViewCategory_GetData">
                        <ItemTemplate>
                            <%# Item.Category.CategoryName %>
                        </ItemTemplate>
                        <ItemSeparatorTemplate> + </ItemSeparatorTemplate>
                    </asp:ListView>
                    <br />
                    <h2>Tracks list:</h2>
                    
                    <asp:ListView ID="_ViewTrack" runat="server" ItemType="ListenAndWrite.ModelIdentify.Track">
                        <ItemTemplate>
                            <b>Script: </b>
                            <br />
                            <asp:Label ID="txtTrackScript" runat="server" Text="<%# Item.ScriptText %>"></asp:Label>
                            <br />
                            <audio controls loop style="width: 80%">
                                <source src="<%# Item.AudioPath %>" type="audio/mp3">
                                Your browser does not support the audio element.
                            </audio>
                        </ItemTemplate>
                        <ItemSeparatorTemplate><br />----------------------<br /></ItemSeparatorTemplate>
                    </asp:ListView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <%-- right --%>
    </form>
</body>
</html>

