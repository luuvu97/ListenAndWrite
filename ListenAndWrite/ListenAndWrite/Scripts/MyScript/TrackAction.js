var imported = document.createElement('script');
imported.src = 'CompactString.js';
document.head.appendChild(imported);

//NOTE: ALL user input and CompactString process string with lowercase
function TrackAction(rawScript, audioPath, controlElement, testType) {
    //script be processed in lower case
    this.rawScript = rawScript.split(" "); //rawScript dua ve mang cac word [ "i" , "am", "vu"]
    this.cs = new CompactString();
    this.script = this.cs.parseScript(rawScript); //rawSau khi duoc rut gon dua ve mang cac array: [ ["i am", "i'm"], ["vu"]]
    this.numOfWrong = new Array(this.script.length);    //so lan nhap sai ung voi script. su dung voi FULL MODE ex[ 2, 0]
    this.markRawScript = new Array(this.rawScript.length);  //so lan nhap sai ung voi rawScript => [2, 2, 0]
    controlElement.setSourceAudio(audioPath);
    this.currentIndex = 0;
    this.currentWord = ""; //Từ đang cần nhập
    this.correctText = "";  //Tất cả những từ đã nhập đúng
    this.rawScriptIndex = 0;    //Index cho rawScript có thể k giống curentIndex của script do có từ viết tắt
    this.isCompactText = false; //Có phải từ đang xét là từ có khả năng viết tắt hay không
    this.isNextTrack = false;
    this.tetsType = testType;

    this.initNewWord(); //use for fullmode: kiem tra tu can nhap co phai tu co the viet tat hay khong

    for (var i = 0; i < this.numOfWrong.length; i++) {
        this.numOfWrong[i] = 0;
    }
    for (var i = 0; i < this.rawScript.length; i++) {
        //markRawScript 0: correct. 1: semi correct .>=2: Wrong
        this.markRawScript[i] = 0;
    }
}

//same update rawScript index
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
    //[i'm, i am]
    
    if (this.script[this.currentIndex].length == 2 && (this.script[this.currentIndex][0] != this.rawScript[this.rawScriptIndex].toLowerCase())) {
        this.isCompactText = true;
    } else {
        this.isCompactText = false;
    }
}

//get current corrected text
TrackAction.prototype.getCorrectText = function () {
    this.correctText = "";
    for (var i = 0; i < this.rawScriptIndex; i++) {
        this.correctText += this.rawScript[i] + " ";
    }
}

//thiet lap trang thai cho tu can nhap tiep theo
TrackAction.prototype.nextWord = function () {
    //this.isNextWord = true;
    // this.correctText += " " + this.currentWord;
    this.currentWord = "";
    this.updateMarkRawSrcipt();
    if (this.isCompactText == true) {
        this.rawScriptIndex += 2;
        this.currentIndex++;
    } else {
        this.rawScriptIndex++;
        this.currentIndex++;
    }
    if (this.currentIndex == this.script.length) {
        this.isNextTrack = true;
    } else {
        this.initNewWord();
    }
    this.getCorrectText();
}

TrackAction.prototype.processUserInput = function (strInp) {
    var correctIndex = -1;
    var tmp;
    var isCorrect;
    strInp = strInp.toLowerCase();
    var str = strInp.toLowerCase();
    if (this.tetsType == "FULLMODE") {
        //1. Kiem tra tu vua nhap chinh xac den vi tri nao. Sai thi cat bo
        for (var i = 1; i <= str.length; i++) {
            tmp = str.substring(0, i);
            isCorrect = false;
            for (text in this.script[this.currentIndex]) {
                if (this.script[this.currentIndex][text].indexOf(tmp) == 0) {
                    correctIndex = i;
                    isCorrect = true;
                }
            }
            if (isCorrect == false) {
                this.numOfWrong[this.currentIndex]++;
                break;
            }
        }
        this.currentWord = strInp.substring(0, correctIndex);
        if (this.script[this.currentIndex].includes(str.substring(0, correctIndex))) {  //dung tu can nhap => can nhap tu moi
            this.nextWord();
        }
    } else {
        //this.currentWord = strInp;
        strInp = this.standardized(strInp);
        //console.log(strInp);
        var maxSubstring = new MaxSubString(this.rawScript, this.script);
        this.correctText = maxSubstring.getMaxSubString(strInp);
        if (maxSubstring.numOfCorrectText == this.script.length) {
            this.isNextTrack = true;
        }
    }

}

//chuan hoa: loai bo ky tu dac biet, whitespace 2 dau sau do dua no ve dang viet tat nhat (neu co the)
TrackAction.prototype.standardized = function (strInp) {
    for (var i = 0; i < strInp.length; i++) {
        var ch = strInp.charAt(i);
        if (!((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || ch == ' ' || ch == '\'')) {
            strInp = strInp.replace(ch, ' ');
        }
    }
    strInp = strInp.trim();
    while (true) {
        var prev = strInp;
        strInp = strInp.replace("  ", " ");
        if (prev == strInp) {
            break;
        }
    }

    strInp = this.cs.compactString(strInp);
    strInp = strInp.split(" ");
    return strInp;
}