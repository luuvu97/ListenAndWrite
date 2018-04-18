function MaxSubString(rawScript, script) {
    this.rawScript = rawScript;
    this.script = script;
    this.mark = new Array();
    this.numOfCorrectText = 0;
}

MaxSubString.prototype.getMaxSubString = function (strInp) {
    
    //strInp is standardized
    var M = new Array();
    //init data
    for (var i = 0; i < this.script.length + 1; i++) {
        M.push(new Array(strInp.length + 1));
    }
    for (var i = 0; i < this.script.length + 1; i++) {
        for (var j = 0; j < strInp.length + 1; j++) {
            M[i][j] = new Node(this.script.length + 1);
        }
    }
    
    //process
    for (var i = 1; i < this.script.length + 1; i++) {
        for (var j = 1; j < strInp.length + 1; j++) {
            debugger
            if (this.script[i - 1].includes(strInp[j - 1])) {
                M[i][j].val = M[i - 1][j - 1].val + 1;
                for (var t = 1; t < this.script.length + 1; t++) {
                    M[i][j].mark[t] = M[i - 1][j - 1].mark[t];
                }
                M[i][j].mark[i] = true;
            } else {
                if (M[i][j - 1].val > M[i - 1][j].val) {
                    M[i][j].val = M[i][j - 1].val;
                    for (var t = 1; t < this.script.length + 1; t++) {
                        M[i][j].mark[t] = M[i][j - 1].mark[t];
                    }
                } else {
                    M[i][j].val = M[i - 1][j].val;
                    for (var t = 1; t < this.script.length + 1; t++) {
                        M[i][j].mark[t] = M[i - 1][j].mark[t];
                    }
                }
            }
        }
    }
    
    this.mark = M[this.script.length][strInp.length].mark;
    this.numOfCorrectText = M[this.script.length][strInp.length].val;
    //console.log(M[this.script.length][strInp.length].val);
    //console.log(this.getResult());
    return this.getResult();
}

MaxSubString.prototype.getResult = function () {
    var ret = "";
    var rawScriptIndex = 0;
    for (var i = 0; i < this.script.length; i++) {
        
        //if (this.script[this.currentIndex].length == 2 && (this.script[this.currentIndex] != this.rawScript[this.rawScriptIndex])) 
        if (this.script[i].length == 2 && (this.script[i][0] != this.rawScript[rawScriptIndex].toLowerCase())) {
            if (this.mark[i + 1]) {
                ret += this.rawScript[rawScriptIndex] + " ";
                ret += this.rawScript[rawScriptIndex + 1] + " ";
            }
            rawScriptIndex += 2;
        } else {
            if (this.mark[i + 1]) {
                ret += this.rawScript[rawScriptIndex] + " ";
            }
            rawScriptIndex += 1;
        }

        //if (this.script[i].length == 1 &&  this.script[i][0] == this.rawScript[rawScriptIndex]) {
        //    if (this.mark[i + 1]) {
        //        ret += this.rawScript[rawScriptIndex] + " ";
        //    }
        //    rawScriptIndex += 1;
        //} else {    //compact string
        //    if (this.mark[i + 1]) {
        //        ret += this.rawScript[rawScriptIndex] + " ";
        //        ret += this.rawScript[rawScriptIndex + 1] + " ";
        //    }
        //    rawScriptIndex += 2;
        //}
    }
    return ret;
}

function Node(n) {
    this.val = 0;
    this.mark = new Array(n);
    for (var i = 0; i < n; i++) {
        this.mark[i] = false;
    }
}