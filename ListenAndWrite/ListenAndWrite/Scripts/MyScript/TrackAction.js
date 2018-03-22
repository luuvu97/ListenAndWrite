var imported = document.createElement('script');
imported.src = 'CompactString.js';
document.head.appendChild(imported);

function TrackAction(rawScript, audioPath, controlElement){
    this.rawScript = rawScript.split(" ");
    this.cs = new CompactString();
    this.script = this.cs.parseScript(rawScript);
    this.numOfWrong = new Array(this.script.length);
    this.markRawScript = new Array(this.rawScript.length);
    //this.controlElement = controlElement;
    controlElement.setSourceAudio(audioPath);
    this.currentIndex = 0;
    this.currentWord = ""; //Từ đang cần nhập
    this.correctText = "";  //Tất cả những từ đã nhập đúng
    this.rawScriptIndex = 0;    //Index cho rawScript có thể k giống curentIndex của script do có từ viết tắt
    this.isCompactText = false; //Có phải từ đang xét là từ có khả năng viết tắt hay không
    //this.isNextWord = false;
    this.isNextTrack = false;

    this.initNewWord();

    for(var i = 0; i < this.numOfWrong.length; i++){
        this.numOfWrong[i] = 0;
    }
    for (var i = 0; i < this.rawScript.length; i++) {
        //markRawScript 0: correct. 1: semi correct .>=2: Wrong
        this.markRawScript[i] = 0;
    }
}

TrackAction.prototype.updateMarkRawSrcipt = function () {
    var offset = 1;
    if (this.isCompactText == true) {
        var offset = 2;
    }
    for (var i = this.rawScriptIndex; i < this.rawScriptIndex + offset; i++) {
        this.markRawScript[i] = this.numOfWrong[this.currentIndex];
    }
}


TrackAction.prototype.initNewWord = function () {
    if(this.script[this.currentIndex].length == 2 && (this.script[this.currentIndex] != this.rawScript[this.rawScriptIndex])){
        this.isCompactText = true;
    }else{
        this.isCompactText = false;
    }
}

TrackAction.prototype.getCorrectText = function(){
    this.correctText = "";
    //console.log(this.currentIndex + " - " + this.rawScriptIndex);
    for(var i = 0; i < this.rawScriptIndex; i++){
        this.correctText += this.rawScript[i] + " ";
    }
}

TrackAction.prototype.nextWord = function(){
    //this.isNextWord = true;
    // this.correctText += " " + this.currentWord;
    this.currentWord = "";
    this.updateMarkRawSrcipt();
    //console.log("Next word: " + this.script[this.currentIndex]);
    if(this.isCompactText == true){
        this.rawScriptIndex += 2;
        this.currentIndex++;
    }else{
        this.rawScriptIndex++;
        this.currentIndex++;
    }
    if(this.currentIndex == this.script.length){
        this.isNextTrack = true;
    }else{
        this.initNewWord();
    }   
    this.getCorrectText();
}

TrackAction.prototype.processUserInput = function (strInp) {
    var correctIndex = -1;
    var tmp;
    var isCorrect;
    var str = strInp.toLowerCase();
    for(var i = 1; i <= str.length; i++){
        tmp = str.substring(0, i);
        isCorrect = false;
        for(text in this.script[this.currentIndex]){
            if(this.script[this.currentIndex][text].indexOf(tmp) == 0){
                correctIndex = i;
                isCorrect =  true;
            }
        }
        if(isCorrect == false){
            this.numOfWrong[this.currentIndex]++;
            break;
        }
    }
    this.currentWord = strInp.substring(0, correctIndex);
    //console.log(this.currentWord);
    if(this.script[this.currentIndex].includes(str.substring(0, this.correctIndex))){
        this.nextWord();
    }
    //this.controlElement.processInput(this.currentWord);
}