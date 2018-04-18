function ControlElement(audioControl, txtInput, txtScriptCorrect, txtPoint, txtCurrentPart, btnForEventWireUp, lblPart, chooseDiv, progressBar) {
    this.audioControl =  document.getElementById(audioControl);
    this.txtInput = document.getElementById(txtInput);
    this.txtScriptCorrect = document.getElementById(txtScriptCorrect);
    this.txtPoint = document.getElementById(txtPoint);
    this.txtCurrentPart = document.getElementById(txtCurrentPart);
    this.btnForEventWireUp = document.getElementById(btnForEventWireUp);
    this.lblPart = document.getElementById(lblPart);
    this.chooseDiv = chooseDiv;
    this.progressBar = progressBar;
}

ControlElement.prototype.viewText = function(rawScript, markRawScript, rawScriptIndex, currentWord){
    
    var str = "";
    for (var i = 0; i < rawScriptIndex; i++) {
        if (markRawScript[i] == 0) {
            str += rawScript[i];
        } else if (markRawScript[i] == 1) {
            str += "<font style='color: blue'>" + rawScript[i] + "</font>";
        } else {
            str += "<font style='color: red'>" + rawScript[i] + "</font>";
        }
        str += " ";
    }
    this.txtScriptCorrect.innerHTML = str;
    this.txtInput.value = currentWord;
}

ControlElement.prototype.viewTextNewMode = function (correctText) {
    this.txtScriptCorrect.innerHTML = correctText;
}

ControlElement.prototype.emptyViewText = function () {
    this.txtScriptCorrect.innerHTML = "";
    this.txtInput.value = "";
    this.txtInput.focus();
}

ControlElement.prototype.processInput = function (currentWord) {
    this.txtInput.value = currentWord;
}

ControlElement.prototype.updateVal = function (point, part) {
    this.txtPoint.value = point;
    this.txtCurrentPart.value = part;
    this.btnForEventWireUp.click();
}

ControlElement.prototype.setSourceAudio = function(audioPath){
    this.audioControl.src = audioPath;
}

ControlElement.prototype.showChooseNext = function () {

}

ControlElement.prototype.showProgress = function (curPoint, maxPoint) {
    this.progressBar.showProgress(curPoint, maxPoint);
}