<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ListenAndWrite._Default" %>

<%@ Register TagPrefix="asp" Namespace="System.Web.UI.DataVisualization.Charting" Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
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

<%--    <h1>Your Progress:</h1>
    <asp:Chart ID="Chart1" runat="server" Width="1024px" Height="720px" style="width:100%">
        <Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
        </ChartAreas>
    </asp:Chart>--%>

</asp:Content>
