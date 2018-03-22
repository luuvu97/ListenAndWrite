var imported = document.createElement('script');
imported.src = 'TrackAction.js';
document.head.appendChild(imported);

function LessonAction(level, rawScripts, audioPaths, controlElement, currentPart = 0) {
    this.level = level;
    this.rawScripts = rawScripts; //String[]
    this.audioPaths = audioPaths;   //String[]
    this.currentPart = currentPart;
    this.pointTrack = new Array(this.rawScripts.length);
    this.isEnd = false;
    this.controlElement = controlElement;
    this.trackAction = new TrackAction(rawScripts[currentPart],audioPaths[currentPart], controlElement);
    //this.isTesting = false;
    // Để sau này xử lý trực tiếp tại js. Không phải gọi 
    // document.getElementById("txtInput").value = lessonAction.trackAction.currentWord;
    // ở file html

    for(var i = 0; i < this.pointTrack.length; i++){
        this.pointTrack[i] = 0;
    }
}

    LessonAction.prototype.processUserInput = function (userInput) {
        if (!this.isEnd) {
            this.trackAction.processUserInput(userInput);
            this.controlElement.viewText(this.trackAction.rawScript, this.trackAction.markRawScript, this.trackAction.rawScriptIndex, this.trackAction.currentWord);
            if(this.trackAction.isNextTrack == true){
                this.calculatePoint();
                this.controlElement.updateVal(this.pointTrack, this.currentPart, this.trackAction.numOfWrong);
                if(this.currentPart == this.rawScripts.length - 1){
                    this.isEnd = true;
                }
                this.controlElement.chooseDiv.show(this.isEnd);
                var maxPoint = 100 + (this.level - 1) * 10;
                var maxTrackPoint = maxPoint / this.rawScripts.length;
                this.controlElement.showProgress(this.pointTrack[this.currentPart], maxTrackPoint);
            }
        }
    }

    LessonAction.prototype.nextTrack = function () {
        if(this.currentPart < this.rawScripts.length - 1){
            this.currentPart++;
            this.trackAction = new TrackAction(this.rawScripts[this.currentPart], this.audioPaths[this.currentPart], this.controlElement);
            this.controlElement.chooseDiv.hide();
            this.controlElement.progressBar.hide();
            this.controlElement.emptyViewText();
        }
        this.controlElement.lblPart.innerText = (this.currentPart + 1);
    }

    LessonAction.prototype.isNextTrack = function(){
        return this.trackAction.isNextTrack;
    }

    LessonAction.prototype.replay = function(){
        this.trackAction = new TrackAction(this.rawScripts[this.currentPart], this.audioPaths[this.currentPart], this.controlElement);
        this.isEnd = false;
        this.controlElement.chooseDiv.hide();
        this.controlElement.progressBar.hide();
        this.controlElement.emptyViewText();
    }

    LessonAction.prototype.prevTrack = function(){
        if(this.currentPart > 0){
            this.currentPart--;
            this.trackAction = new TrackAction(this.rawScripts[this.currentPart], this.audioPaths[this.currentPart], this.controlElement);
            this.isEnd = false;
            this.controlElement.chooseDiv.hide();
            this.controlElement.progressBar.hide();
            this.controlElement.emptyViewText();
            this.controlElement.lblPart.innerText = (this.currentPart + 1);
        }
    }

    LessonAction.prototype.goTrack = function(trackNumber){
        if(trackNumber >= 0 && trackNumber < this.rawScripts.length){
            this.currentPart = trackNumber;
            this.trackAction = new TrackAction(this.rawScripts[this.currentPart], this.audioPaths[this.currentPart], this.controlElement);
            this.isEnd = false;
            this.controlElement.lblPart.innerText = (this.currentPart + 1);
        }
    }

    LessonAction.prototype.calculatePoint = function(){
        var maxPoint = 100 + (this.level - 1) * 10;
        var maxTrackPoint = maxPoint / this.rawScripts.length;
        var maxPointWord = maxTrackPoint / this.trackAction.script.length;
        var point = 0;
        for(e in this.trackAction.numOfWrong){
            if(this.trackAction.numOfWrong[e] == 0){
                point += maxPointWord;
            }else if(this.trackAction.numOfWrong[e] == 1){
                point +=  maxPointWord * 0.75;
            }else if(this.trackAction.numOfWrong[e] == 2){
                point += maxPointWord * 0.5;
            }
        }
        this.pointTrack[this.currentPart] = point;
    }
