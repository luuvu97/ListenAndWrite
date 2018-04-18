<%@ Page Title="Provider" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Provider.aspx.cs" Inherits="ListenAndWrite.Provider" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
    <audio controls hidden loop id="audioPreviewControl" runat="server">
        Your browser does not support the audio element.
    </audio>

    <script>
        var PRENAME = "MainContent_";
        var prevSrcBtn = null;

        function previewAudioOnClick(src) {
            var audioControl = document.getElementById(PRENAME + "audioPreviewControl");

            if (prevSrcBtn != null) {
                prevSrcBtn.value = "Preview";
            }

            if (prevSrcBtn == src) {
                src.value = "Preview";
                prevSrcBtn = null;
                audioControl.pause();
                return;
            } else {
                prevSrcBtn = src;
            }
            var audioPath = src.getAttribute("audioPath");
            audioControl.src = audioPath;
            src.value = "Stop";
            audioControl.play();
        }

    </script>

    <h1 style="text-align: center; font-weight:bold;"><%: this.provider.UserName %></h1>
    <h3 style="text-align: center;"">Total <%: this.provider.Lessons.Count %> lesson(s)</h3>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="_DataPager" EventName="PreRender" />
        </Triggers>
        <ContentTemplate>

            <div class="audio-list">
                <ul>
                    <asp:ListView ID="_ViewAudios" runat="server"
                        ItemType="ListenAndWrite.ModelIdentify.Lesson" SelectMethod="_ViewAudios_GetData">
                        <ItemTemplate>
                            <li>
                                <table>
                                    <tr>
                                        <td style="width: 10%; min-width: 100px; padding-right: 10px">
                                            <input type="button" id="btnPreviewAudio" value="Preview" class="preview_audio" audiopath="<%#:Item.Tracks[0].AudioPath %>" onclick="previewAudioOnClick(this);" />
                                        </td>
                                        <td>
                                            <div class="content">
                                                <a href="Listening.aspx?LessonID=<%#: Item.LessonID %>" class="name"><%#: Item.Title %> - <%# String.Format("{0:00}",Convert.ToInt64(Item.Length) / 60) %>:<%# String.Format("{0:00}",Convert.ToInt64(Item.Length) % 60) %></a>
                                                <br />
                                                Provider by: <a href="Provider?Provider=<%#: Item.Provider.UserName %>" class="link"><%#: Item.Provider.UserName %></a>
                                                <font class="highlight">Level: <%#: Item.Level %>, Views: <%#: Item.ViewCount %></font>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </ul>
            </div>


            <div class="pagination">
                <asp:DataPager ID="_DataPager" runat="server"
                    PagedControlID="_ViewAudios" OnPreRender="_DataPager_PreRender">
                    <Fields>
                        <asp:NextPreviousPagerField ShowFirstPageButton="true" ShowLastPageButton="false" ShowNextPageButton="false" ShowPreviousPageButton="false"
                            RenderNonBreakingSpacesBetweenControls="true" FirstPageText="«" />
                        <asp:NumericPagerField CurrentPageLabelCssClass="black" ButtonCount="5"
                            RenderNonBreakingSpacesBetweenControls="true" />
                        <asp:NextPreviousPagerField ShowFirstPageButton="false" ShowLastPageButton="true" ShowNextPageButton="false" ShowPreviousPageButton="false"
                            RenderNonBreakingSpacesBetweenControls="true" LastPageText="»" />
                    </Fields>
                </asp:DataPager>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
