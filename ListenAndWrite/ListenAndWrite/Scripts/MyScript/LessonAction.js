var imported = document.createElement('script');
imported.src = 'TrackAction.js';
document.head.appendChild(imported);

function LessonAction(level, rawScripts, audioPaths, controlElement, testType) {
    this.level = level;
    this.rawScripts = rawScripts; //String[] //mang cac rawScript
    this.audioPaths = audioPaths;   //String[]  //mang cac audioPath
    this.currentPart = 0;
    this.pointTrack = new Array(this.rawScripts.length);
    this.isEnd = false;
    this.controlElement = controlElement;
    this.testType = testType.toUpperCase();
    this.hint = "";
    this.trackAction = new TrackAction(rawScripts[this.currentPart], audioPaths[this.currentPart], controlElement, this.testType);

    if (this.testType == "FULLMODE") {
        this.maxPoint = 100 + (this.level - 1) * 10;
        this.maxTrackPoint = this.maxPoint / this.rawScripts.length;
    } else {
        this.maxPoint = 200 + (this.level - 1) * 10;
        this.maxTrackPoint = this.maxPoint / this.rawScripts.length;
    }

    for (var i = 0; i < this.pointTrack.length; i++) {
        this.pointTrack[i] = 0;
    }
}

LessonAction.prototype.showHint = function () {
    this.getHint();
    document.getElementById("txtHint").innerText = this.hint;
}

LessonAction.prototype.getHint = function () {
    if (this.testType == "FULLMODE") {
        this.hint = this.trackAction.script[this.trackAction.currentIndex];
    } else {
        this.hint = this.rawScripts[this.currentPart];
    }
}

LessonAction.prototype.processUserInput = function (userInput) {
    if (!this.isEnd) {
        this.trackAction.processUserInput(userInput);
        
        if (this.testType == "FULLMODE") {
            this.controlElement.viewText(this.trackAction.rawScript, this.trackAction.markRawScript, this.trackAction.rawScriptIndex, this.trackAction.currentWord);
        } else {
            this.controlElement.viewTextNewMode(this.trackAction.correctText);
        }
        if (this.trackAction.isNextTrack == true) {
            this.calculatePoint();
            this.controlElement.updateVal(this.pointTrack, this.currentPart, this.trackAction.numOfWrong);
            if (this.currentPart == this.rawScripts.length - 1) {
                this.isEnd = true;
            }
            this.controlElement.chooseDiv.show(this.isEnd);
            this.controlElement.showProgress(this.pointTrack[this.currentPart], this.maxTrackPoint);
        }
        this.showHint();
    }
}

LessonAction.prototype.nextTrack = function () {
    if (this.currentPart < this.rawScripts.length - 1) {
        this.currentPart++;
        this.trackAction = new TrackAction(this.rawScripts[this.currentPart], this.audioPaths[this.currentPart], this.controlElement, this.testType);
        this.controlElement.chooseDiv.hide();
        this.controlElement.progressBar.hide();
        this.controlElement.emptyViewText();
    }
    this.controlElement.lblPart.innerText = (this.currentPart + 1);
    this.showHint();
}

LessonAction.prototype.isNextTrack = function () {
    return this.trackAction.isNextTrack;
}

LessonAction.prototype.replay = function () {
    this.trackAction = new TrackAction(this.rawScripts[this.currentPart], this.audioPaths[this.currentPart], this.controlElement, this.testType);
    this.isEnd = false;
    this.controlElement.chooseDiv.hide();
    this.controlElement.progressBar.hide();
    this.controlElement.emptyViewText();
}

LessonAction.prototype.prevTrack = function () {
    if (this.currentPart > 0) {
        this.currentPart--;
        this.trackAction = new TrackAction(this.rawScripts[this.currentPart], this.audioPaths[this.currentPart], this.controlElement, this.testType);
        this.isEnd = false;
        this.controlElement.chooseDiv.hide();
        this.controlElement.progressBar.hide();
        this.controlElement.emptyViewText();
        this.controlElement.lblPart.innerText = (this.currentPart + 1);
        this.showHint();
    }
}

LessonAction.prototype.goTrack = function (trackNumber) {
    if (trackNumber >= 0 && trackNumber < this.rawScripts.length) {
        this.currentPart = trackNumber;
        this.trackAction = new TrackAction(this.rawScripts[this.currentPart], this.audioPaths[this.currentPart], this.controlElement, this.testType);
        this.isEnd = false;
        this.controlElement.lblPart.innerText = (this.currentPart + 1);
    }
}

LessonAction.prototype.calculatePoint = function () {
    var point = 0;
    if (this.testType == "FULLMODE") {
        var maxPointWord = this.maxTrackPoint / this.trackAction.script.length;
        for (e in this.trackAction.numOfWrong) {
            if (this.trackAction.numOfWrong[e] == 0) {
                point += maxPointWord;
            } else if (this.trackAction.numOfWrong[e] == 1) {
                point += maxPointWord * 0.75;
            } else if (this.trackAction.numOfWrong[e] == 2) {
                point += maxPointWord * 0.5;
            }
        }
    }
    else {   //new mode
        point = this.maxTrackPoint;
    }
    this.pointTrack[this.currentPart] = point;
}
