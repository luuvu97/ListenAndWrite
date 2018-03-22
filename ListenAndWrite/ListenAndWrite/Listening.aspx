﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Listening.aspx.cs" Inherits="ListenAndWrite.Listening" EnableEventValidation="false" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%
        if (this.scores != null && this.scores.Count != 0 && this.isContinue == false)
        {
    %>
    Your have already completed this lesson. You can <b><i><a href="ReviewLesson.aspx?LessonID=<%: this.lesson.LessonID %>">review of your test [HERE]</a></i></b>
    <br />
    Do your want to listen again
    <br />
    <asp:Button ID="btnListenAgain" runat="server" OnClick="btnListenAgain_Click" Text="Continue" />
    <%
        }
        else
        {
            if (this.testType == ListenAndWrite.ModelIdentify.TestType.Unset)
            { %>

    <asp:Button ID="btnTestTypeFullMode" runat="server" OnClick="btnTestTypeFullMode_Click" Text="Full Mode" />
    <asp:Button ID="btnTestTypeNewMode" runat="server" OnClick="btnTestTypeNewMode_Click" Text="New Mode" />

    <% }
            else
            { %>

    <asp:FormView ID="_ViewLessionDetail" runat="server" ItemType="ListenAndWrite.ModelIdentify.Lesson" SelectMethod="_ViewLessionDetail_GetItem">
        <EmptyDataTemplate>
            ERROR: CANNOT RETRIEVE THE LESSION INFORMATION
        </EmptyDataTemplate>

        <ItemTemplate>
            <h1><%#: Item.Title %></h1>
            Provider: <%#: Item.Provider.UserName %>
            <br />
            <%#: Item.Tracks.Count %> Part
            <br />
            Description : <%#: Item.Description %>
        </ItemTemplate>
    </asp:FormView>
    <input type="button" value="PrevTrack" id="btnPrev" />
    <label id="lblPart">1</label>
    <input type="button" value="NextTrack" id="btnNext" />

    <div id="txtResult" style="width: 100%; background: #808080; min-height: 300px; font-size: 2em"></div>
    <br />

    <asp:UpdatePanel ID="ListeningUpdatePanel" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnForEventFireUp" EventName="Click" />
        </Triggers>
        <ContentTemplate>

            <script>
                function pageLoad() {
                    
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Button ID="btnForEventFireUp" runat="server" Text="Button for event fire up" OnClick="btnForEventFireUp_Click" Style="display: none" />

    <asp:HiddenField ID="txtPoint" runat="server" />
    <asp:HiddenField ID="txtCurrentPart" runat="server" />
    <asp:HiddenField ID="txtScripts" runat="server" />
    <asp:HiddenField ID="txtAudioPaths" runat="server" />
    <asp:HiddenField ID="txtTestType" runat="server" />

    <input type="button" id="btnAudioControl" value="Pause" />
    <br />
    <div>
        <asp:TextBox ID="txtInput" runat="server" TextMode="MultiLine" Width="100%" Rows="2" Style="z-index: 1; position: absolute;"></asp:TextBox>
        <div style="position: absolute; left: 50%;">
            <div id="divchooseBtn" style="z-index: 10; position: relative; left: -50%; width: 100%; vertical-align: central; align-items: center">
                <input type="button" value="Complete Lesson" id="btnComplete" />
                <input type="button" value="Next" id="btnChooseNext" />
                <input type="button" value="Listen Again" id="btnChooseListenAgain" />
            </div>
        </div>
    </div>
    <br />
    <br />
    <br />
    <input type="button" id="btnHint" style="background: none; color: green;" value="HINT" />
    <br />
    <div id="hint" style="display: none">
        You need to input:
        <div id="txtHint"></div>
    </div>
    <audio controls autoplay hidden loop id="audioControl" runat="server">
        Your browser does not support the audio element.
    </audio>


    <div class="w3-light-grey">
        <div id="myBar" class="w3-green" style="height: 24px; width: 1%"></div>
        <div style="float: right;" id="progressText"></div>
    </div>
    <br>

    <script>
        var PRENAME = "MainContent_";
        var scripts = document.getElementById(PRENAME + "txtScripts").value;
        var audioPaths = document.getElementById(PRENAME + "txtAudioPaths").value;
        scripts = scripts.split(";");
        audioPaths = audioPaths.split(";");

        var chooseDiv = new ChooseDiv("btnComplete", "btnChooseNext", "btnChooseListenAgain")
        var progressBar = new ProgressBar("myBar", "progressText");
        var controlElement = new ControlElement(PRENAME + "audioControl", PRENAME + "txtInput","txtResult", PRENAME + "txtPoint", PRENAME + "txtCurrentPart", PRENAME + "btnForEventFireUp", "lblPart", chooseDiv, progressBar);
        var lessonAction = new LessonAction(<%= this.lesson.Level %>, scripts, audioPaths, controlElement);

        chooseDiv.hide();
        progressBar.hide();

        document.getElementById(PRENAME + "txtInput").onkeyup = function(){
            setTimeout(function(){
                lessonAction.processUserInput(document.getElementById(PRENAME + "txtInput").value);
                document.getElementById("txtHint").innerText = lessonAction.trackAction.script[lessonAction.trackAction.currentIndex];
            },100)
        }

        document.getElementById("btnPrev").onclick = function(){
            lessonAction.prevTrack();
        }
        document.getElementById("btnNext").onclick = function(){
            lessonAction.nextTrack();
        }
        document.getElementById("btnChooseNext").onclick = function(){
            lessonAction.nextTrack();
        }
        document.getElementById("btnChooseListenAgain").onclick = function(){
            lessonAction.replay();
        }
        document.getElementById("btnComplete").onclick = function(){
            window.location = "ReviewLesson.aspx?LessonID=" + <%: this.lesson.LessonID %>;
        }

        var showHint = false;
        document.getElementById("btnHint").onclick = function(){
            if(showHint == false){
                showHint = true;
                document.getElementById("hint").style.display = "Block";
                document.getElementById("btnHint").value = "Hide hint";
                document.getElementById("txtHint").innerText = lessonAction.trackAction.script[lessonAction.trackAction.currentIndex];
            }else{
                showHint = false;
                document.getElementById("hint").style.display = "None";
                document.getElementById("btnHint").value = "HINT";
            }
        }

        var btnAudioControl = document.getElementById("btnAudioControl");
        btnAudioControl.onclick = function(){
            var audioControl = document.getElementById(PRENAME + "audioControl");
            if(audioControl.paused){
                btnAudioControl.value = "Pause";
                audioControl.play();
            }else{
                btnAudioControl.value = "Play";
                audioControl.pause();
            }
        }

    </script>

    <% }
        }
    %>
</asp:Content>