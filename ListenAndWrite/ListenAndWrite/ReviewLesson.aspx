<%@ Page Title="Review Lesson" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReviewLesson.aspx.cs" Inherits="ListenAndWrite.ReviewLesson" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="Content/slideshow.css" type="text/css" media="screen" />

    <%-- LESSON INFORMATION --%>
    <asp:FormView ID="_ViewLessionDetail" runat="server" ItemType="ListenAndWrite.ModelIdentify.Lesson" SelectMethod="_ViewLessionDetail_GetItem">
        <EmptyDataTemplate>
            ERROR: CANNOT RETRIEVE THE LESSION INFORMATION
        </EmptyDataTemplate>

        <ItemTemplate>
            <a href="Listening.aspx?LessonID=<%#: Item.LessonID %>"><h1><b><%#: Item.Title %></b></h1></a>
            Provider: <%#: Item.Provider.UserName %>
            <br />
            <%#: Item.Tracks.Count %> Part
            <br />
            Description : <%#: Item.Description %>
        </ItemTemplate>
    </asp:FormView>

    <%-- LESSON INFORMATION --%>
    <%-- BRIEF INFO AUBOUT EACH TEST TYPE --%>
    <asp:ListView ID="_ViewBriefScore" runat="server" ItemType="ListenAndWrite.ModelIdentify.ScoreNode" SelectMethod="_ViewBriefScore_GetData">
        <EmptyDataTemplate>
            Your have never ever completed this lesson.
        </EmptyDataTemplate>
        <ItemTemplate>
            <h3><%# Item.testType %></h3>
            <div class="w3-light-grey">
                <div style="float: left;"><%# Item.testType %></div>
                <div style="float: right;" id="progressText<%# Item.testType %>"></div>
                <br />
                <div style="position: absolute; z-index: 10; width: 100%; text-align: center" id="scorePercent<%# Item.testType %>"></div>
                <div id="myBar<%# Item.testType %>" class="w3-green" style="height: 24px; text-align: center; color: black;"></div>
                <script>
                    var PRENAME = "MainContent_";
                    var width = (<%# Item.totalScore %> / <%# Item.maxScore %>) * 100;
                    document.getElementById("myBar<%# Item.testType %>").style.width = width + "%";
                    if(width < 50){
                        document.getElementById("scorePercent<%# Item.testType %>").style.color = "red";
                    }else if(width < 70){
                        document.getElementById("scorePercent<%# Item.testType %>").style.color = "yellow";
                    }else{
                        document.getElementById("scorePercent<%# Item.testType %>").style.color = "white";
                    }
                document.getElementById("scorePercent<%# Item.testType %>").innerText = width.toFixed(2) + "%";
                    document.getElementById("progressText<%# Item.testType %>").innerText = (<%# Item.totalScore %>).toFixed(2) + " / " + (<%# Item.maxScore %>).toFixed(2);
                </script>
            </div>
        </ItemTemplate>
    </asp:ListView>
    <%-- BRIEF INFO AUBOUT EACH TEST TYPE --%>

    <%-- Detail about each test type. It will hide when the user want --%>
    <div id="divDetail">

        <%-- FULL MODE DETAIL --%>
        <div id="divFullModeDetail">

            <h1 style="text-align: center; font-weight: bold;">Full Mode</h1>

            <asp:ListView ID="_ViewTrackFullMode" runat="server" ItemType="ListenAndWrite.ModelIdentify.TrackNode" SelectMethod="_ViewTrackFullMode_GetData">
                <EmptyDataTemplate>
                    <script>
                        document.getElementById("divFullModeDetail").style.display = "none";
                    </script>
                </EmptyDataTemplate>

                <ItemTemplate>
                    <div class="w3-light-grey">
                        <div style="float: left;" id="trackProgressTitleFullMode<%# Item.seqNumber %>">Track <%# Item.seqNumber %></div>
                        <div style="float: right;" id="trackProgressTextFullMode<%# Item.seqNumber %>"></div>
                        <br />
                        <div style="position: absolute; z-index: 10; width: 100%; text-align: center" id="trackPercentFullMode<%# Item.seqNumber %>"></div>
                        <div id="trackBarFullMode<%# Item.seqNumber %>" class="w3-green" style="height: 24px; text-align: center"></div>
                        <script>
                            var width = (<%# Item.point %> / <%# Item.maxPoint %>) * 100;
                            document.getElementById("trackBarFullMode<%# Item.seqNumber %>").style.width = width + "%";
                            if(width < 50){
                                document.getElementById("trackPercentFullMode<%# Item.seqNumber %>").style.color = "red";
                            }else if(width < 70){
                                document.getElementById("trackPercentFullMode<%# Item.seqNumber %>").style.color = "orange";
                            }else{
                                document.getElementById("trackPercentFullMode<%# Item.seqNumber %>").style.color = "white";
                            }
                            document.getElementById("trackPercentFullMode<%# Item.seqNumber %>").innerText = width.toFixed(2) + "%";
                            document.getElementById("trackProgressTextFullMode<%# Item.seqNumber %>").innerText = (<%# Item.point %>).toFixed(2) + " / " + (<%# Item.maxPoint %>).toFixed(2);
                        </script>
                    </div>
                </ItemTemplate>
                <ItemSeparatorTemplate>
                    <br />
                </ItemSeparatorTemplate>
            </asp:ListView>
        </div>

        <%-- FULL MODE DETAIL --%>

        <%-- NEW MODE DETAIL --%>
        <div id="divNewModeDetail">

            <h1 style="text-align: center; font-weight: bold;">New Mode</h1>

            <asp:ListView ID="_ViewTrackNewMode" runat="server" ItemType="ListenAndWrite.ModelIdentify.TrackNode" SelectMethod="_ViewTrackNewMode_GetData">
                <EmptyDataTemplate>
                    <script>
                        document.getElementById("divNewModeDetail").style.display = "none";
                    </script>
                </EmptyDataTemplate>

                <ItemTemplate>
                    <div class="w3-light-grey">
                        <div style="float: left;" id="trackProgressTitleNewMode<%# Item.seqNumber %>">Track <%# Item.seqNumber %></div>
                        <div style="float: right;" id="trackProgressTextNewMode<%# Item.seqNumber %>"></div>
                        <br />
                        <div style="position: absolute; z-index: 10; width: 100%; text-align: center" id="trackPercentNewMode<%# Item.seqNumber %>"></div>
                        <div id="trackBarNewMode<%# Item.seqNumber %>" class="w3-green" style="height: 24px; text-align: center"></div>
                        <script>
                            var width = (<%# Item.point %> / <%# Item.maxPoint %>) * 100;
                            document.getElementById("trackBarNewMode<%# Item.seqNumber %>").style.width = width + "%";
                            if(width < 50){
                                document.getElementById("trackPercentNewMode<%# Item.seqNumber %>").style.color = "red";
                            }else if(width < 70){
                                document.getElementById("trackPercentNewMode<%# Item.seqNumber %>").style.color = "orange";
                            }else{
                                document.getElementById("trackPercentNewMode<%# Item.seqNumber %>").style.color = "white";
                            }
                            document.getElementById("trackPercentNewMode<%# Item.seqNumber %>").innerText = width.toFixed(2) + "%";
                            document.getElementById("trackProgressTextNewMode<%# Item.seqNumber %>").innerText = (<%# Item.point %>).toFixed(2) + " / " + (<%# Item.maxPoint %>).toFixed(2);
                        </script>
                    </div>
                </ItemTemplate>
                <ItemSeparatorTemplate>
                    <br />
                </ItemSeparatorTemplate>
            </asp:ListView>
        </div>
        <%-- NEW MODE DETAIL --%>
    </div>
    <%-- Detail about each test type. It will hide when the user want --%>


    <h1 style="font-weight: bold; text-align: center">Your progress</h1>

    <div class="slideshow-container" id="slideshow">

        <div class="mySlides">
            <asp:Chart ID="Chart1" runat="server" Width="800px" Height="500px" Style="width: 100%">
                <Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
            <h1>Full Mode</h1>
        </div>

        <div class="mySlides">
            <asp:Chart ID="Chart2" runat="server" Width="800px" Height="500px" Style="width: 100%">
                <Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
            <h1>New Mode</h1>
        </div>

        <a class="prev" onclick="plusSlides(-1)">&#10094;</a>
        <a class="next" onclick="plusSlides(1)">&#10095;</a>

    </div>
    <br>

    <script>
        var index = 1;
        showSlides(index);

        function plusSlides(n) {
            showSlides(index += n);
        }
        function showSlides(n) {
            var slides = document.getElementsByClassName("mySlides");
            if (n > slides.length) {
                index = 1;
            }
            if (n < 1) {
                index = slides.length;
            }
            for (var i = 0; i < slides.length; i++) {
                slides[i].style.display = "none";
            }
            slides[index - 1].style.display = "block";
        }
    </script>
</asp:Content>
