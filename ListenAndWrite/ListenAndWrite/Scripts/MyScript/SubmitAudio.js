function Transcript(fileContent) {
    debugger
    this.fileContent = fileContent;
    this.transcripts = new Array();
    this.readData();
}

function TranscriptNode(seqNumber,start, end, script) {
    this.seqNumber = seqNumber;
    this.start = start;
    this.end = end;
    this.script = script;
}

//ex str: 00:00:04,450
function MyTime(str){
    str = str.replace(",", ":");
    var arr = str.split(":");
    this.hour = parseInt(arr[0]);
    this.min = parseInt(arr[1]);
    this.sec = parseInt(arr[2]);
    this.ms = parseInt(arr[3]);

    //console.log(arr + "\n" + this.hour + ";" + this.min + ":" + this.sec + ":" + this.ms + "\n");
}

MyTime.prototype.toDouble = function () {
    return (this.hour * 3600 + this.min * 60 + this.sec) + "." + this.ms;
}

Transcript.prototype.readData = function () {
    var arr = this.fileContent.split("\r\n");
    if (arr[0] == this.fileContent) {
        arr = this.fileContent.split("\n");
    }
    var i = 0;
    var myTime, seqNumber, start, end, script;
    while (i < arr.length) {
        seqNumber = arr[i++];
        var tmp = arr[i++].split(" --> ");
        myTime = new MyTime(tmp[0]);
        start = myTime.toDouble();
        myTime = new MyTime(tmp[tmp.length - 1]);
        end = myTime.toDouble();
        script = "";
        while (i < arr.length) {
            if (!arr[i] || arr[i].length === 0) {
                i++;
                break;
            } else {
                if (script != "") {
                    script += "\n";
                }
                script += arr[i++];
            }
        }
        while (i < arr.length) {
            if (!arr[i] || arr[i].length === 0) {
                i++;
            } else {
                break;
            }
        }
        //console.log(seqNumber + "\n" + start + " --> " + end + "\n" + script + "\n");
        this.transcripts.push(new TranscriptNode(seqNumber, start, end, script));
    }
}

Transcript.prototype.getCurStart = function(n){
    return this.transcripts[n].start;
}

Transcript.prototype.getCurEnd = function (n) {
    return this.transcripts[n].end;
}

Transcript.prototype.getCurScript = function (n) {
    return this.transcripts[n].script;
}

//Display seq number, playFrom -> playTo
Transcript.prototype.getBriefInfo = function (n) {
    return this.transcripts[n].seqNumber + " Play " + this.transcripts[n].start + " --> " + this.transcripts[n].end;
}

Transcript.prototype.getMulScript = function (n) {
    var ret = "";
    if (n == 0) {
        ret += this.getBriefInfo(n) + "\n";
        ret += this.getCurScript(n) + "\n\n";
        ret += this.getBriefInfo(n + 1) + "\n";
        ret += this.getCurScript(n + 1) + "\n\n";
        ret += this.getBriefInfo(n + 2) + "\n";
        ret += this.getCurScript(n + 2);
    } else if (n == this.transcripts.length - 1) {
        ret += this.getBriefInfo(n - 2) + "\n";
        ret += this.getCurScript(n - 2) + "\n\n";
        ret += this.getBriefInfo(n - 2) + "\n";
        ret += this.getCurScript(n - 1) + "\n\n";
        ret += this.getBriefInfo(n) + "\n";
        ret += this.getCurScript(n);
    } else {
        ret += this.getBriefInfo(n - 1) + "\n";
        ret += this.getCurScript(n - 1) + "\n\n";
        ret += this.getBriefInfo(n) + "\n";
        ret += this.getCurScript(n) + "\n\n";
        ret += this.getBriefInfo(n + 1) + "\n";
        ret += this.getCurScript(n + 1);
    }
    return ret;
}